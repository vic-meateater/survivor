using Entitas;
using UnityEngine;

namespace Code.Gameplay.Hero.Systems
{
    public class SetHeroDirectionByInputSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _heroes;
        private readonly IGroup<InputEntity> _inputs;

        public SetHeroDirectionByInputSystem(GameContext game, InputContext input)
        {
            _heroes = game.GetGroup(GameMatcher.Hero);
            _inputs = input.GetGroup(InputMatcher.Input);
        }
        public void Execute()
        {
            foreach(InputEntity input in _inputs)
            foreach (GameEntity hero in _heroes)
            {
                hero.isMoving = input.hasAxisInput;

                if (input.hasAxisInput)
                {
                    var axis = input.AxisInput;
                    hero.ReplaceDirection(new Vector3(axis.x, 0, axis.y).normalized);
                }
            }
        }
    }
}