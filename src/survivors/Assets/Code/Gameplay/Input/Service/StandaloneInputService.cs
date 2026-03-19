using Code.Gameplay.Cameras.Provider;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Gameplay.Input.Service
{
    public class StandaloneInputService : IInputService
    {
        private readonly PlayerInput _playerInput;
        private readonly Camera _camera;

        private InputAction _move;
        private InputAction _pointerPosition;
        private InputAction _pointerClick;

        private bool _pointerDownConsumed;
        private bool _pointerUpConsumed;

        public StandaloneInputService(ICameraProvider3D camera)
        {
            _playerInput = new();
            _playerInput.Enable();
            _camera = camera.MainCamera;

            _move = _playerInput.Actions.Move;
            _pointerPosition = _playerInput.Actions.PointerPosition;
            _pointerClick = _playerInput.Actions.PointerClick;
        }

        public bool HasAxisInput() => _move.ReadValue<Vector2>().sqrMagnitude > 0.01f;
        public float GetHorizontalAxis() => _move.ReadValue<Vector2>().x;
        public float GetVerticalAxis() => _move.ReadValue<Vector2>().y;

        public bool GetPointerHeld() => _pointerClick.IsPressed();

        public bool GetPointerDown()
        {
            if (_pointerDownConsumed) return false;
            bool val = _pointerClick.WasPressedThisFrame();
            if (val) _pointerDownConsumed = true;
            return val;
        }

        public bool GetPointerUp()
        {
            if (_pointerUpConsumed) return false;
            bool val = _pointerClick.WasReleasedThisFrame();
            if (val) _pointerUpConsumed = true;
            return val;
        }

        public Vector3 GetWorldPointerPosition()
        {
            if (_camera == null) return Vector3.zero;
            Vector2 screenPos = _pointerPosition.ReadValue<Vector2>();
            Ray ray = _camera.ScreenPointToRay(screenPos);
            var plane = new Plane(Vector3.up, Vector3.zero);
            return plane.Raycast(ray, out float distance)
                ? ray.GetPoint(distance)
                : Vector3.zero;
        }
        
        public void ResetFrameState()
        {
            _pointerDownConsumed = false;
            _pointerUpConsumed = false;
        }
    }
}