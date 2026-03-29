using System;
using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.Features.Enchants;
using Code.Infrastructure.Identifiers;

namespace Code.Gameplay.Features.Statuses.Factory
{
  public class StatusFactory : IStatusFactory
  {
    private readonly IIdentifierService _identifiers;

    public StatusFactory(IIdentifierService identifiers)
    {
      _identifiers = identifiers;
    }

    public GameEntity CreateStatus(StatusSetup setup, int producerID, int targetID)
    {
      GameEntity status = null;

      switch (setup.StatusTypeID)
      {
        case StatusTypeId.Unknown:
          break;
        case StatusTypeId.Poison:
          status = CreatePoisonStatus(setup, producerID, targetID);
          break;
        case StatusTypeId.Freeze:
          status = CreateFreezeStatus(setup, producerID, targetID);
          break;
        case StatusTypeId.PoisonEnchant:
          status = CreatePoisonEnchantStatus(setup, producerID, targetID);
          break;
        default:
          throw new Exception($"Effect with type id {setup.StatusTypeID} does not exist");
      }

      status
        .With(x => x.AddDuration(setup.Duration), when: setup.Duration > 0)
        .With(x => x.AddTimeLeft(setup.Duration), when: setup.Duration > 0)
        .With(x => x.AddPeriod(setup.Period), when: setup.Period > 0)
        ;
      return status;
    }
    
    private GameEntity CreatePoisonStatus(StatusSetup setup, int producerID, int targetID)
    {
      return CreateEntity.Empty()
          .AddId(_identifiers.Next())
          .AddStatusTypeID(StatusTypeId.Poison)
          .AddEffectValue(setup.Value)
          .AddTimeSinceLastTick(0f)
          .AddProducerID(producerID)
          .AddTargetID(targetID)
          .With(x => x.isStatus = true)
          .With(x => x.isPoison = true)
        ;
    }

    private GameEntity CreateFreezeStatus(StatusSetup setup, int producerID, int targetID)
    {
      return CreateEntity.Empty()
          .AddId(_identifiers.Next())
          .AddStatusTypeID(StatusTypeId.Freeze)
          .AddEffectValue(setup.Value)
          .AddTimeSinceLastTick(0f)
          .AddProducerID(producerID)
          .AddTargetID(targetID)
          .With(x => x.isStatus = true)
          .With(x => x.isFreeze = true)
        ;
    }

    private GameEntity CreatePoisonEnchantStatus(StatusSetup setup, int producerID, int targetID)
    {
      return CreateEntity.Empty()
          .AddId(_identifiers.Next())
          .AddStatusTypeID(StatusTypeId.PoisonEnchant)
          .AddEnchantTypeID(EnchantTypeID.PoisonArmaments)
          .AddEffectValue(setup.Value)
          .AddProducerID(producerID)
          .AddTargetID(targetID)
          .With(x => x.isStatus = true)
          .With(x=>x.isPoison = true)
          .With(x => x.isPoisonEnchant = true)
        ;
    }
  }
}