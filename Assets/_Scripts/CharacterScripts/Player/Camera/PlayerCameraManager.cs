using System;
using Core.Character.Player;
using UnityEngine;
using Zenject;

namespace Core
{
    public class PlayerCameraManager : MonoBehaviour
    {
        [field: SerializeField] public PlayerCamera ActivePlayerCamera { get; private set; }
        [field: SerializeField] public FirstPersonCamera FirstPersonCamera { get; private set; }
        [field: SerializeField] public ThirdPersonPlayerCamera ThirdPersonPlayerCamera { get; private set; }
        [field: SerializeField] public PlayerViewMode PlayerViewMode { get; private set; }

        [SerializeField] private PlayerInputManager _playerInputManager;

        [SerializeField] private float _cameraZoom;
        [SerializeField] private float _cameraTargetZoom;
        [SerializeField] private float _zoomSensitivity;
        [SerializeField] private float _zoomSpeed;

        //Inject]
        public void Construct(PlayerInputManager playerInputManager)
        {
            _playerInputManager = playerInputManager;
        }


        public void SetCamera(PlayerCamera camera)
        {
            ActivePlayerCamera = camera;

            if(ActivePlayerCamera is FirstPersonCamera)
            {
                PlayerViewMode = PlayerViewMode.FirstPerson;
                FirstPersonCamera.ToggleCamera(true);
                ThirdPersonPlayerCamera.ToggleCamera(false);
            }
            else if(ActivePlayerCamera is ThirdPersonPlayerCamera)
            {
                PlayerViewMode = PlayerViewMode.ThirdPerson;
                FirstPersonCamera.ToggleCamera(false);
                ThirdPersonPlayerCamera.ToggleCamera(true);
            }
        }


        private void HandleZoom()
        {
            _cameraTargetZoom -= _playerInputManager.ZoomInput * _zoomSensitivity;

            _cameraZoom = Mathf.Lerp(_cameraZoom, _cameraTargetZoom, Time.deltaTime * _zoomSpeed);

            if(_cameraZoom - _cameraTargetZoom <= 0.05)
            {
                _cameraZoom = _cameraTargetZoom;
            }


            if(_cameraZoom <= 1 && ActivePlayerCamera != FirstPersonCamera)
            {
                SetCamera(FirstPersonCamera);
                FirstPersonCamera.SyncCamera(ThirdPersonPlayerCamera);
            }
            else if(_cameraZoom > 1 &&ActivePlayerCamera != ThirdPersonPlayerCamera)
            {
                SetCamera(ThirdPersonPlayerCamera);
                ThirdPersonPlayerCamera.SyncCamera(FirstPersonCamera);
            }
            if(ActivePlayerCamera == ThirdPersonPlayerCamera)
            {
                ThirdPersonPlayerCamera.DefaultCameraZPosition = -_cameraZoom;
            }

        }

        private void Update()
        {
            HandleZoom();
        }

        private void LateUpdate()
        {
            ActivePlayerCamera.HandleAllCameraActions();
        }
    }
}