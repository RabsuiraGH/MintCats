using UnityEngine;

namespace Core.Character.Player.Camera
{
    public class FirstPersonCamera : PlayerCamera
    {
        public override void HandleAllCameraActions()
        {
            if (PlayerManager != null)
            {
                HandleRotation();
            }
        }

        public void SyncRotationWithPlayer()
        {
            LeftAndRightLookAngle = PlayerManager.GetCharacterTransform().rotation.eulerAngles.y;

        }

        protected override void ApplyRotation()
        {
            Vector3 cameraRotation = Vector3.zero;

            cameraRotation.x = UpAndDownLookAngle;

            Quaternion targetRotation = Quaternion.Euler(cameraRotation);
            CameraTransform.localRotation = targetRotation;
        }

        private void HandleRotation()
        {
            int inverseY = _inverseY ? -1 : 1;
            int inverseX = _inverseX ? -1 : 1;


            LeftAndRightLookAngle += inverseX * PlayerManager.InputManager.CameraInput.x * _leftAndRightRotationSpeed * Time.fixedDeltaTime * _sensitiveX;

            UpAndDownLookAngle += inverseY * PlayerManager.InputManager.CameraInput.y * _upAndDownRotationSpeed * Time.fixedDeltaTime * _sensitiveY;

            UpAndDownLookAngle = Mathf.Clamp(UpAndDownLookAngle, _minimumPivot, _maximumPivot);

            ApplyRotation();
        }
    }
}