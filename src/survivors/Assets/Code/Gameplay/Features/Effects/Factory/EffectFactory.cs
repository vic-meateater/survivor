using System;
using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Infrastructure.Identifiers;

namespace Code.Gameplay.Features.Effects.Factory
{
    public class EffectFactory : IEffectFactory
    {
        private readonly IIdentifierService _identifiers;

        public EffectFactory(IIdentifierService identifiers)
        {
            _identifiers = identifiers;
        }

        public GameEntity CreateEffect(EffectSetup setup, int producerID, int targetID)
        {
            switch (setup.EffectTypeID)
            {
                case EffectTypeID.Unknown:
                    break;
                case EffectTypeID.Damage:
                    return CreateDamage(producerID, targetID, setup.Value);
            }

            throw new Exception($"Effect with type id {setup.EffectTypeID} does not exist");
        }

        private GameEntity CreateDamage(int producerID, int targetID, float value)
        {
           return CreateEntity.Empty()
                .AddId(_identifiers.Next())
                .With(x => x.isEffect = true)
                .With(x => x.isDamageEffect = true)
                .AddEffectValue(value)
                .AddProducerID(producerID)
                .AddTargetID(targetID)
                ;
        }
    }
}