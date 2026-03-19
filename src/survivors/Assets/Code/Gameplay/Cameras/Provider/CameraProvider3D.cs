using UnityEngine;

namespace Code.Gameplay.Cameras.Provider
{
    public class CameraProvider3D : ICameraProvider3D
    {
        public Camera MainCamera { get; private set; }

        public float WorldScreenHeight { get; private set; }
        public float WorldScreenWidth { get; private set; }

        public void SetMainCamera(Camera camera)
        {
            MainCamera = camera;

            RefreshBoundaries();
        }

        private void RefreshBoundaries()
        {
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

            Vector3 bottomLeft = ViewportToGround(0, 0, groundPlane);
            Vector3 topRight = ViewportToGround(1, 1, groundPlane);

            WorldScreenWidth = topRight.x - bottomLeft.x;
            WorldScreenHeight = topRight.z - bottomLeft.z;
        }

        private Vector3 ViewportToGround(float viewportX, float viewportY, Plane groundPlane)
        {
            Ray ray = MainCamera.ViewportPointToRay(new Vector3(viewportX, viewportY));

            if (groundPlane.Raycast(ray, out float distance))
                return ray.GetPoint(distance);

            return Vector3.zero;
        }
    }
}