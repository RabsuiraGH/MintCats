using UnityEngine;

namespace Core.Character.Player.Camera
{
    public class ThirdPersonPlayerCamera : PlayerCamera
    {
        [Header("THIRD PERSON CAMERA")]

        [Header("CAMERA FOLLOW")]
        [SerializeField] private float _cameraFollowSmoothTime = 0.01f;
        [field: SerializeField] public float CameraZPosition { get; set; } = -2.5f;

        [Header("CAMERA COLLISIONS")]
        [SerializeField] private LayerMask _cameraCollisionLayer;
        [SerializeField] private float _cameraCollisionRadius = 0.2f;

        [Header("CAMERA VALUES")]
        [SerializeField] private float _targetCameraPosition;
        [SerializeField] private Vector3 _cameraObjectPosition;
        [SerializeField] private Vector3 _cameraVelocity;
        [SerializeField] protected Transform _cameraUpAndDownPivot;

        [field: SerializeField] public float CameraMaxDistance{ get; private set; }
        [field: SerializeField] public float CameraMinDistance{ get; private set; }

        [SerializeField] private float _cameraMaxHeight;
        [SerializeField] private float _cameraMinHeight;

        [SerializeField] private float _cameraHeight;
        [SerializeField] private Transform _heightPivot;

        public void ApplyCameraConfig(PlayerCameraConfig config)
        {
            _cameraFollowSmoothTime = config.CameraSmoothTime;
            _upAndDownRotationSpeed = config.UpAndDownRotationSpeed;
            _leftAndRightRotationSpeed = config.LeftAndRightRotationSpeed;
            _minimumPivot = config.MinimumPivot;
            _maximumPivot = config.MaximumPivot;
            _inverseY = config.InverseY;
            _inverseX = config.InverseX;
            _sensitiveY = config.SensitiveY;
            _sensitiveX = config.SensitiveX;
            CameraZPosition = config.DefaultCameraPosition;
        }

        public override void HandleAllCameraActions()
        {
            if (PlayerManager == null) return;

            HandleFollowTarget();
            HandleRotation();
            HandleCollision();
        }

        protected override void SetRotation()
        {
            Vector3 cameraRotation = Vector3.zero;
            Quaternion targetRotation;

            cameraRotation.y = LeftAndRightLookAngle;
            targetRotation = Quaternion.Euler(cameraRotation);

            CameraTransform.rotation = targetRotation;
            cameraRotation = Vector3.zero;
            cameraRotation.x = UpAndDownLookAngle;
            targetRotation = Quaternion.Euler(cameraRotation);
            _cameraUpAndDownPivot.localRotation = targetRotation;
        }

        public void UpdateCameraLocationViaZoom(float currentZoom)
        {
            CameraZPosition = -currentZoom;
            CalculateCameraHeight(currentZoom);
        }

        private void CalculateCameraHeight(float planeDistance)
        {
            _cameraHeight = Mathf.Lerp(_cameraMaxHeight,_cameraMinHeight, (planeDistance - CameraMinDistance)/(CameraMaxDistance - CameraMinDistance));
            _cameraHeight = Mathf.Clamp(_cameraHeight, _cameraMinHeight, _cameraMaxHeight);


            Vector3 cameraTransformPosition = _heightPivot.position;
            cameraTransformPosition.y = _cameraHeight;
            _heightPivot.position = cameraTransformPosition;
        }
        private void HandleCollision()
        {
            _targetCameraPosition = CameraZPosition;

            Vector3 direction = CameraObject.transform.position - _cameraUpAndDownPivot.position;
            direction.Normalize();

            if (Physics.SphereCast(_cameraUpAndDownPivot.position, _cameraCollisionRadius, direction,
                                   out RaycastHit hit, Mathf.Abs(_targetCameraPosition), _cameraCollisionLayer))
            {
                float distanceFromHitObject = Vector3.Distance(_cameraUpAndDownPivot.position, hit.point);

                _targetCameraPosition = -(distanceFromHitObject - _cameraCollisionRadius);
            }

            if (Mathf.Abs(_targetCameraPosition) < _cameraCollisionRadius)
            {
                _targetCameraPosition = -_cameraCollisionRadius;
            }

            _cameraObjectPosition.z = Mathf.Lerp(CameraObject.transform.localPosition.z, _targetCameraPosition, 0.1f);
            CameraObject.transform.localPosition = _cameraObjectPosition;
        }

        private void HandleFollowTarget()
        {
            Vector3 targetCameraPosition = Vector3.SmoothDamp(
                CameraTransform.position,
                PlayerManager.transform.position,
                ref _cameraVelocity,
                _cameraFollowSmoothTime * Time.fixedDeltaTime);

            CameraTransform.position = targetCameraPosition;
        }

        private void HandleRotation()
        {
            int inverseY = _inverseY ? -1 : 1;
            int inverseX = _inverseX ? -1 : 1;


            LeftAndRightLookAngle +=
                inverseX * PlayerManager.InputManager.CameraInput.x * _leftAndRightRotationSpeed * Time.fixedDeltaTime *
                _sensitiveX;

            UpAndDownLookAngle +=
                inverseY * PlayerManager.InputManager.CameraInput.y * _upAndDownRotationSpeed * Time.fixedDeltaTime *
                _sensitiveY;

            UpAndDownLookAngle = Mathf.Clamp(UpAndDownLookAngle, _minimumPivot, _maximumPivot);


            SetRotation();
        }
    }
}