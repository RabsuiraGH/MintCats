using System;
using Core.Character.Player;
using R3;
using UnityEngine;
using Zenject;

namespace Core.Character.Player.Camera
{
    public class PlayerCameraManager : MonoBehaviour
    {
        [field: SerializeField] public PlayerCamera ActivePlayerCamera { get; private set; }

        [SerializeField] private FirstPersonCamera _firstPersonCamera;
        [SerializeField] private ThirdPersonPlayerCamera _thirdPersonPlayerCamera;
        [field: SerializeField] public PlayerViewMode PlayerViewMode { get; private set; }

        [SerializeField] private PlayerInputManager _playerInputManager;

        [SerializeField] private readonly ReactiveProperty<float> _cameraCurrentZoom = new();
        [SerializeField] private float _cameraTargetZoom;
        [SerializeField] private float _zoomSensitivity;
        [SerializeField] private float _zoomSpeed;

        [SerializeField] private float _firstThirdViewEdgeDistance = 1;

        public event Action<PlayerCamera, PlayerViewMode> OnPlayerViewChanged;

        private void Start()
        {
            // TODO: Maybe unsubscribe by composite disp
            _cameraCurrentZoom.Subscribe(SwitchView);
            _cameraCurrentZoom.Subscribe(UpdateThirdPersonCameraLocation);
            _playerInputManager.OnSwitchCameraViewRequested += SwitchCamera;
        }

        private void OnDestroy()
        {
            _playerInputManager.OnSwitchCameraViewRequested -= SwitchCamera;
        }

        /// <summary>
        /// Updates third person camera height and Z distance via current zoom
        /// </summary>
        private void UpdateThirdPersonCameraLocation(float currentZoom)
        {
            if (ActivePlayerCamera != _thirdPersonPlayerCamera) return;

            _thirdPersonPlayerCamera.UpdateCameraLocationViaZoom(currentZoom);
        }

        /// <summary>
        /// Checks when to switch view
        /// </summary>
        private void SwitchView(float currentZoom)
        {
            // switches view if conditions are met
            if (ActivePlayerCamera != _firstPersonCamera && currentZoom <= _firstThirdViewEdgeDistance)
            {
                SetCamera(_firstPersonCamera);
            }
            else if (ActivePlayerCamera != _thirdPersonPlayerCamera && currentZoom > _firstThirdViewEdgeDistance)
            {
                SetCamera(_thirdPersonPlayerCamera);
            }
        }

        /// <summary>
        /// Sets new camera as current active
        /// </summary>
        private void SetCamera(PlayerCamera newCamera)
        {
            if (newCamera is FirstPersonCamera)
            {
                // TODO: UNCOMMENT IF WE WANNA ROTATE PLAYER TO LOOK AT NEEDED POINT
                //_firstPersonCamera.SyncCamera(_thirdPersonPlayerCamera);
                PlayerViewMode = PlayerViewMode.FirstPerson;
                _firstPersonCamera.SyncRotationWithPlayer();
                _firstPersonCamera.ToggleCamera(true);
                _thirdPersonPlayerCamera.ToggleCamera(false);
            }
            else if (newCamera is ThirdPersonPlayerCamera)
            {
                if (_cameraCurrentZoom.Value <= _firstThirdViewEdgeDistance)
                    _cameraTargetZoom = _firstThirdViewEdgeDistance * 1.05f;

                _thirdPersonPlayerCamera.SyncCamera(_firstPersonCamera);
                PlayerViewMode = PlayerViewMode.ThirdPerson;
                _firstPersonCamera.ToggleCamera(false);
                _thirdPersonPlayerCamera.ToggleCamera(true);
            }

            ActivePlayerCamera = newCamera;
            OnPlayerViewChanged?.Invoke(ActivePlayerCamera, PlayerViewMode);
        }
        /// <summary>
        /// Switches cameras sequentially
        /// </summary>
        private void SwitchCamera()
        {
            if(ActivePlayerCamera is FirstPersonCamera)
            {
                SetCamera(_thirdPersonPlayerCamera);
            }
            else if(ActivePlayerCamera is ThirdPersonPlayerCamera)
            {
                SetCamera(_firstPersonCamera);
            }
        }


#if UNITY_EDITOR
        [EasyButtons.Button]
        private void SwitchToFirstPerson() => SetCamera(_firstPersonCamera);

        [EasyButtons.Button]
        private void SwitchToThirdPerson() => SetCamera(_thirdPersonPlayerCamera);
#endif

        /// <summary>
        /// Handles zoom input
        /// </summary>
        private void HandleZoom()
        {
            // read scroll input
            _cameraTargetZoom -= _playerInputManager.ZoomInput * _zoomSensitivity;

            // smooth zoom
            _cameraCurrentZoom.Value = Mathf.Lerp(_cameraCurrentZoom.CurrentValue, _cameraTargetZoom, Time.deltaTime * _zoomSpeed);
            _cameraTargetZoom = Mathf.Clamp(_cameraTargetZoom, _thirdPersonPlayerCamera.CameraMinDistance, _thirdPersonPlayerCamera.CameraMaxDistance);

            // push to target if difference too small
            if (Mathf.Abs(_cameraCurrentZoom.CurrentValue - _cameraTargetZoom) <= 0.05)
            {
                _cameraCurrentZoom.Value = _cameraTargetZoom;
            }
        }

        private void LateUpdate()
        {
            HandleZoom();
            ActivePlayerCamera.HandleAllCameraActions();
        }
    }
}