# «Выжить на районе» — Architecture Cheat Sheet

## Meta
Vampire Survivors-клон, isometric 3D, советский двор 80-х, Yandex Games WebGL.
Solo dev: Вик (Meateater Games). Обращаться на "ты", отвечать на русском.

## Stack
Unity 6 URP WebGL, Entitas ECS (k-syndicate fork + Jenny codegen), Zenject DI, New Input System, PluginYG2.

## Contexts
Game, Input, Meta

## Codegen Rules
- Single field → `Value` → shortcut `entity.Speed`
- Builder chaining: Add/Replace/Remove return entity
- Component ≠ context name → suffix `Component`
- Enum ≠ component class name → `EnemyTypeIdComponent` + `EnemyTypeId` enum
- `EnemyTypeId` enum starts at 1 (Unknown = 0)

## Flags
`isHero`, `isMoving`, `isTurnAlongDirection`, `isEnemy`, `isDead`, `isProcessingDeath`, `isMovementAvailable`, `isArmament`, `isEffect`, `isProcessed`, `isCooldownUp`, `isReadyToCollectTargets`, `isCollectTargetsContinuously`, `isDestructed`, `isStatus`, `isPoison`, `isFreeze`, `isApplied`, `isUnapplied`, `isAffected`, `isActive`, `isFollowingProducer`, `isPoisonEnchant`, `isDudeWordAbility`, `isProjectileAbility`, `isOrbitingBrickAbility`, `isHealEffect`, `isDamageEffect`

## View Pattern
```
GameObject
├── EntityBehaviour           ← SetEntity(), ReleaseEntity(), CollisionRegistry
├── TransformRegistrar        ← Common
├── HeroAnimatorRegistrar     ← Hero
├── EnemyAnimatorRegistrar    ← Enemy
├── EnchantVisualsRegistrar   ← Enchant visuals
└── StatusVisuals             ← Poison/Freeze color via shader
```
Entity создаётся в фабрике → ViewPath/ViewPrefab → BindEntityViewFromPathSystem/BindEntityViewFromPrefabSystem → EntityViewFactory → EntityBehaviour.SetEntity() → регистраторы.

## Feature Hierarchy
```
BattleFeature
├── InputFeature
│   ├── InitializeInputSystem
│   └── EmitInputSystem
├── BindViewFeature
│   ├── BindEntityViewFromPathSystem
│   └── BindEntityViewFromPrefabSystem
├── HeroFeature
│   ├── InitializeHeroSystem          ← creates Hero + all abilities
│   ├── SetHeroDirectionByInputSystem
│   ├── AnimateHeroMovementSystem
│   └── CameraFollowHeroSystem
├── MovementFeature
│   ├── DirectionalDeltaMoveSystem
│   ├── OrbitCenterFollowSystem
│   ├── OrbitalDeltaMoveSystem
│   ├── TurnAlongDirectionSystem
│   └── UpdateTransformPositionSystem
├── AbilityFeature
│   ├── CooldownSystem
│   ├── ProjectileAbilitySystem
│   ├── OrbitingBrickAbilitySystem
│   └── DudeWordAuraAbilitySystem
├── EnchantFeature
│   ├── PoisonEnchantSystem
│   └── ApplyPoisonEnchantVisualsSystem
├── EnemiesFeature
│   ├── InitializeSpawnTimerSystem
│   ├── EnemySpawnSystem
│   └── ChaseHeroSystem
├── TargetCollectionFeature
│   ├── CollectTargetsIntervalSystem
│   ├── CastForTargetsNoLimitSystem    ← enemies (no TargetLimit)
│   ├── CastForTargetsWithLimitSystem  ← armaments (TargetLimit + ProcessedTargets)
│   └── CleanupTargetBuffersSystem
├── EffectApplicationFeature
│   ├── ApplyEffectsOnTargetsSystem
│   └── ApplyStatusesOnTargetsSystem
├── ArmamentFeature
│   ├── MarkProcessedOnTargetLimitExceededSystem
│   ├── FollowProducerSystem
│   └── FinalizeProcessedArmamentsSystem
├── EffectFeature
│   ├── RemoveEffectsWithoutTargetSystem
│   ├── ProcessDamageEffectSystem
│   ├── ProcessHealEffectSystem
│   └── CleanupProcessedEffects
├── StatusFeature
│   ├── StatusDurationSystem
│   ├── PeriodicDamageStatusSystem
│   ├── ApplyFreezeStatusSystem
│   ├── StatusVisualFeature
│   │   ├── ApplyPoisonVisualsSystem
│   │   └── UnapplyPoisonVisualsSystem
│   └── CleanupUnappliedStatuses
├── StatFeature
│   ├── StatChangeSystem
│   └── ApplySpeedFromStatsSystem
├── DeathFeature
│   ├── MarkDeadSystem
│   └── UnapplyStatusesOfDeadTargetSystem
├── HeroDeathFeature
│   ├── HeroDeathSystem
│   └── FinalizeHeroDeathProcessingSystem
├── EnemyDeathFeature
│   ├── EnemyDeathSystem
│   └── FinalizeEnemyDeathProcessingSystem
└── ProcessDestructedFeature
    ├── SelfDestructTimerSystem
    ├── CleanupGameDestructedViewsSystem
    └── CleanupGameDestructedSystem
```

## Entity Types

### Hero
Id, WorldPosition, Direction, MaxHP, CurrentHP, Speed, BaseStats, StatModifiers, Hero, TurnAlongDirection, MovementAvailable, ViewPath

### Enemy
Id, WorldPosition, Direction, MaxHP, CurrentHP, Speed, EnemyTypeId, BaseStats, StatModifiers, TargetBuffer, Radius, CollectsTargetInterval, CollectsTargetTimer, LayerMask(Hero), EffectSetups, Enemy, Moving, MovementAvailable, TurnAlongDirection, ViewPath

### Ability
Id, AbilityID, Cooldown, CooldownLeft, CooldownUp, [TypeFlag: ProjectileAbility|OrbitingBrickAbility|DudeWordAbility], Active

### Armament (Projectile/Brick/Aura)
Id, WorldPosition, Direction, Speed, EffectSetups, StatusSetups, Radius, TargetBuffer, ProcessedTargets, TargetLimit, LayerMask, SelfDestructTimer, ViewPrefab, ParentAbility, ProducerID, Armament, Moving, MovementAvailable, ReadyToCollectTargets, CollectTargetsContinuously
- Projectile adds: Direction (straight flight)
- OrbitingBrick adds: OrbitPhase, OrbitRadius, OrbitCenterPosition, OrbitCenterFollowTarget
- Aura adds: CollectsTargetInterval, CollectsTargetTimer, FollowingProducer (no SelfDestructTimer)

### Effect
Id, Effect, [TypeFlag: DamageEffect|HealEffect], EffectValue, ProducerID, TargetID, Processed

### Status
Id, Status, StatusTypeID, [TypeFlag: Poison|Freeze|PoisonEnchant], EffectValue, ProducerID, TargetID, Duration, TimeLeft, Period, TimeSinceLastTick, Applied, Unapplied, Affected

### StatChange
StatChange(Stats enum), TargetID, ProducerID, EffectValue, ApplierStatusLink

### SpawnTimer
SpawnTimer

## Pipelines

### Cooldown
CooldownSystem ticks CooldownLeft → sets CooldownUp=true → AbilitySystem reacts → PutOnCooldown() sets CooldownUp=false + resets CooldownLeft

### Ability → Armament
AbilitySystem (on CooldownUp) → ArmamentFactory.Create*() → armament entity with EffectSetups/StatusSetups

### TargetCollection
CollectTargetsIntervalSystem → CastForTargetsNoLimitSystem (enemies) / CastForTargetsWithLimitSystem (armaments with pierce) → CleanupTargetBuffersSystem

### Effect Application
Armament(EffectSetups) → ApplyEffectsOnTargetsSystem → Effect entity → ProcessDamageEffectSystem/ProcessHealEffectSystem → CleanupProcessedEffects

### Status Application
Armament(StatusSetups) → ApplyStatusesOnTargetsSystem → StatusApplier (refresh or create via EntityIndex) → StatusDurationSystem ticks TimeLeft → PeriodicDamageStatusSystem creates Effect → Unapplied → CleanupUnappliedStatuses + CleanupUnappliedStatusLinkedChanges

### Enchant
Status(PoisonEnchant) on Hero → PoisonEnchantSystem finds new armaments with same ProducerID → adds StatusSetups from EnchantConfig → marks isPoisonEnchant → ApplyPoisonEnchantVisualsSystem colors armament

### Stat System
BaseStats + StatModifiers on entity. StatChangeSystem sums all StatChange entities via EntityIndex(StatKey) into StatModifiers. ApplySpeedFromStatsSystem: Speed = Base + Modifier (min 0). Freeze creates StatChange(Speed, -value) linked to status via ApplierStatusLink.

### Death
MarkDeadSystem (isDead + isProcessingDeath, NoneOf Dead) → UnapplyStatusesOfDeadTargetSystem → domain DeathSystems (HeroDeathSystem/EnemyDeathSystem: stop movement, play anim, SelfDestructTimer) → Finalize (remove ProcessingDeath)

### Destruct
SelfDestructTimerSystem → CleanupGameDestructedViewsSystem (ReleaseEntity + Destroy GO) → CleanupGameDestructedSystem (entity.Destroy)

## Factories
- HeroFactory: creates hero entity with BaseStats, ViewPath
- EnemyFactory: creates enemy by EnemyTypeId (Chushpan/Zaletny/Ment), BaseStats
- AbilityFactory: CreateProjectileAbility, CreateOrbitingBrickAbility, CreateDudeWordAuraAbility
- ArmamentFactory: CreateProjectile, CreateOrbitingBrick, CreateEffectAura (shared CreateProjectileEntity base)
- EffectFactory: CreateEffect → switch on EffectTypeID (Damage, Heal)
- StatusFactory: CreateStatus → switch on StatusTypeId (Poison, Freeze, PoisonEnchant)
- EntityViewFactory: CreateViewForEntity (by path), CreateViewForEntityFromPrefab

## Configs
- AbilityConfig (ScriptableObject) → List<AbilityLevel> → ProjectileSetup, AuraSetup, List<EffectSetup>, List<StatusSetup>
- EnchantConfig (ScriptableObject) → List<EffectSetup>, List<StatusSetup>
- Loaded via StaticDataService.LoadAll() from Resources/Gameplay/Configs/

## Entity Indices
- StatusKey(TargetID, StatusTypeId) → O(1) status lookup via StatusKeyEqualityComparer
- StatKey(TargetID, Stats) → O(1) stat change lookup via StatKeyEqualityComparer

## Services
ITimeService, IInputService, ICameraProvider3D (raycast viewport→groundPlane Y=0), IPhysics3DService (OverlapSphereNonAlloc + ICollisionRegistry), IRandomService, IIdentifierService, IStaticDataService, ILevelDataProvider, IAssetProvider, IEntityViewFactory, IHeroFactory, IEnemyFactory, IAbilityFactory, IArmamentFactory, IEffectFactory, IStatusFactory, IStatusApplier

## DI
BootstrapInstaller (MonoInstaller + IInitializable). Initialize(): StaticDataService.LoadAll() → SceneLoader.LoadScene(Game). All bindings via Zenject. ISystemFactory + DiContainer.Instantiate for DI into systems.

## Movement
XZ plane (Y=0). Camera 45° isometric. CameraProvider3D raycasts viewport corners to Plane(Vector3.up, Vector3.zero) for accurate WorldScreenWidth/Height. Enemy spawn at screen edges relative to hero position.

## Enums
- AbilityID: Unknown=0, Stab=1, OrbitingBrick=2, Projectile=999
- EnemyTypeId: Unknown=0, Chushpan=1, Zaletny=2, Ment=3
- EffectTypeID: Unknown=0, Damage=1, Heal=2
- StatusTypeId: Unknown=0, Poison=1, Freeze=2, PoisonEnchant=3
- EnchantTypeID: Unknown=0, PoisonArmaments=1
- Stats: Unknown=0, Speed=1, MaxHp=2, Damage=3
- CollisionLayer: Hero, Enemy (used with .AsMask())

## Weapon Patterns

| Weapon | Pattern |
|---|---|
| Projectile | cooldown → spawn → flies straight → pierce → dies |
| OrbitingBrick | cooldown → spawn N bricks → orbit hero → continuous damage |
| DudeWordAura | spawn once → follow hero → periodic AOE via interval |
| Stab | TODO: cooldown → cone attack toward nearest enemy |

## Principles
- Atomic systems (know components, not entity types)
- Data from factories, not Inspector registrars
- ISystemFactory + DiContainer.Instantiate for DI
- Positive AllOf preferred over NoneOf, but NoneOf(Dead) acceptable
- MVP-first, configs later (currently hardcoded values in factories)
- URP Lit shader: use `_BaseColor` not `_Color`
- WebGL: GC-sensitive, prefer NonAlloc variants
- Movement in XZ, Y=0

## Current Stage
4 weapons implemented (Projectile, OrbitingBrick, DudeWordAura, Stab reserved). Effects: Damage, Heal. Statuses: Poison, Freeze, PoisonEnchant. Stat system with modifiers. Enchant system (poison armaments). Next: implement Stab (cone attack toward nearest enemy, auto-attack on cooldown).
