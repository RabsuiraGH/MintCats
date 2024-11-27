using UnityEngine;

namespace Core
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

        private void HandleRotation()
        {
            int inverseY = _inverseY ? -1 : 1;
            int inverseX = _inverseX ? -1 : 1;


            LeftAndRightLookAngle += inverseX * PlayerManager._inputManager.CameraInput.x * _leftAndRightRotationSpeed * Time.fixedDeltaTime * _sensitiveX;

            UpAndDownLookAngle += inverseY * PlayerManager._inputManager.CameraInput.y * _upAndDownRotationSpeed * Time.fixedDeltaTime * _sensitiveY;

            UpAndDownLookAngle = Mathf.Clamp(UpAndDownLookAngle, _minimumPivot, _maximumPivot);


            Vector3 cameraRotation = Vector3.zero;

            cameraRotation.x = UpAndDownLookAngle;

            Quaternion targetRotation = Quaternion.Euler(cameraRotation);
            CameraTransform.localRotation = targetRotation;
        }
    }
}