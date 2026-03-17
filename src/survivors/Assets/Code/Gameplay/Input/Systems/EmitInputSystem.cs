using Code.Gameplay.Input.Service;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Input.Systems
{
    public class EmitInputSystem : IExecuteSystem
    {
        private readonly IInputService _inputService;
        private readonly IGroup<InputEntity> _inputs;

        public EmitInputSystem(InputContext input, IInputService inputService)
        {
            _inputService = inputService;
            _inputs = input.GetGroup(InputMatcher.Input);
        }

        public void Execute()
        {
            _inputService.ResetFrameState();

            foreach (InputEntity input in _inputs)
            {
                // Axis
                if (_inputService.HasAxisInput())
                    input.ReplaceAxisInput(new Vector2(
                        _inputService.GetHorizontalAxis(),
                        _inputService.GetVerticalAxis()));
                else if (input.hasAxisInput)
                    input.RemoveAxisInput();

                // Pointer
                if (_inputService.GetPointerHeld())
                    input.ReplacePointerWorldPosition(_inputService.GetWorldPointerPosition());
                else if (input.hasPointerWorldPosition)
                    input.RemovePointerWorldPosition();

                if (_inputService.GetPointerDown())
                    input.isPointerDown = true;
                else
                    input.isPointerDown = false;

                if (_inputService.GetPointerUp())
                    input.isPointerUp = true;
                else
                    input.isPointerUp = false;
            }
        }
    }
}