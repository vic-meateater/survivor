using UnityEngine;

namespace Code.Gameplay.Input.Service
{
  public interface IInputService
  {
    bool HasAxisInput();
    float GetVerticalAxis();
    float GetHorizontalAxis();
    
    bool GetPointerHeld();
    bool GetPointerDown();
    bool GetPointerUp();
    Vector3 GetWorldPointerPosition();
    void ResetFrameState();
  }
}