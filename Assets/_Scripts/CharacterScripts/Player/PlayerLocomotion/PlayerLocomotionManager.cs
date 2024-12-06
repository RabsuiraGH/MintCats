using Core.Character;
using Core.Character.Player.Camera;
using UnityEngine;

namespace Core.Character.Player
{
    public class PlayerLocomotionManager : CharacterLocomotionManager
    {
        [Header("PLAYER EXTRAS")]
        [SerializeField] private PlayerManager _playerManager;

        [SerializeField] private float _backwardsMovementSpeed;

        [field: SerializeField] public float VerticalInputMovement { get; set; }
        [field: SerializeField] public float HorizontalInputMovement { get; set; }

        [SerializeField] private PlayerLocomotionConfig _firstPersonConfig;
        [SerializeField] private PlayerLocomotionConfig _thirdPersonConfig;

        protected override void Awake()
        {
            base.Awake();
            _playerManager.PlayerCameraManager.OnPlayerViewChanged += ApplyViewSettings;
            _playerManager.InputManager.OnSprintRequested += SetRunning;
        }

        private void ApplyViewSettings(PlayerCamera _, PlayerViewMode viewMode)
        {
            if (_playerManager.PlayerCameraManager.PlayerViewMode is PlayerViewMode.FirstPerson)
            {
                ApplyConfig(_firstPersonConfig);
            }
            else if (_playerManager.PlayerCameraManager.PlayerViewMode is PlayerViewMode.ThirdPerson)
            {
                ApplyConfig(_thirdPersonConfig);
            }
        }

        protected override void ApplyConfig(CharacterLocomotionConfig config)
        {
            base.ApplyConfig(config);
            if (config is not PlayerLocomotionConfig playerConfig) return;
            _backwardsMovementSpeed = playerConfig.BackwardsMovementSpeed;
        }

        public void GetVerticalAndHorizontalInputs(Vector2 movementInput)
        {
            VerticalInputMovement = movementInput.y;
            HorizontalInputMovement = movementInput.x;
        }

        public override void HandleAllMovement()
        {
            base.HandleAllMovement();
        }

        protected override void HandleGroundedMovement()
        {
            base.HandleGroundedMovement();
            if (_playerManager.PlayerCameraManager.PlayerViewMode is PlayerViewMode.FirstPerson)
            {
                FirstPersonMovement();
            }
            else if (_playerManager.PlayerCameraManager.PlayerViewMode is PlayerViewMode.ThirdPerson)
            {
                ThirdPersonMovement();
            }
        }

        private void FirstPersonMovement()
        {
            _movementDirection = _playerManager.PlayerCameraManager.ActivePlayerCamera.CameraTransform.forward * VerticalInputMovement;
            _movementDirection = _movementDirection + _playerManager.PlayerCameraManager.ActivePlayerCamera.CameraTransform.right * HorizontalInputMovement;

            _movementDirection.y = 0f;
            _movementDirection.Normalize();

            if (_movementDirection == Vector3.zero)
            {
                AccelerateTo(0);
            }
            else if (_movementDirection.z < 0)
            {
                AccelerateTo(_backwardsMovementSpeed);
            }
            else
            {
                AccelerateTo(_movementSpeed);
            }

            Move();
        }

        private void ThirdPersonMovement()
        {
            _movementDirection = _playerManager.PlayerCameraManager.ActivePlayerCamera.CameraTransform.forward * VerticalInputMovement;
            _movementDirection =
                _movementDirection + _playerManager.PlayerCameraManager.ActivePlayerCamera.CameraTransform.right * HorizontalInputMovement;

            _movementDirection.y = 0f;
            _movementDirection.Normalize();

            if (_movementDirection == Vector3.zero)
            {
                AccelerateTo(0);
            }
            else
            {
                AccelerateTo(_movementSpeed);
            }

            Move();
        }

        protected override void HandleRotation()
        {
            if (_playerManager.PlayerCameraManager.PlayerViewMode is PlayerViewMode.FirstPerson)
            {
                FirstPersonRotation();
            }
            else if (_playerManager.PlayerCameraManager.PlayerViewMode is PlayerViewMode.ThirdPerson)
            {
                ThirdPersonRotation();
            }
        }

        private void FirstPersonRotation()
        {
            Quaternion newRotation = Quaternion.Euler(0, _playerManager.PlayerCameraManager.ActivePlayerCamera.LeftAndRightLookAngle, 0);
            Quaternion targetRotation =
                Quaternion.Slerp(_characterTransform.rotation, newRotation, _rotationSpeed * Time.fixedDeltaTime);

            _characterTransform.rotation = targetRotation;
        }

        private void ThirdPersonRotation()
        {
            _targetRotationDirection = Vector3.zero;
            _targetRotationDirection =
                _playerManager.PlayerCameraManager.ActivePlayerCamera.CameraObject.transform.forward * VerticalInputMovement;
            _targetRotationDirection = _targetRotationDirection +
                                       _playerManager.PlayerCameraManager.ActivePlayerCamera.CameraObject.transform.right *
                                       HorizontalInputMovement;

            _targetRotationDirection.y = 0f;
            _targetRotationDirection.Normalize();

            if (_targetRotationDirection == Vector3.zero)
            {
                _targetRotationDirection = _characterTransform.forward;
            }

            Quaternion newRotation = Quaternion.LookRotation(_targetRotationDirection);
            Quaternion targetRotation =
                Quaternion.Slerp(_characterTransform.rotation, newRotation, _rotationSpeed * Time.fixedDeltaTime);
            _characterTransform.rotation = targetRotation;
        }
    }
}