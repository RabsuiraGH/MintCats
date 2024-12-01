using System;
using Core.Character.Player;
using CustomInspector;
using UnityEngine;


namespace Core.Character.Player.Camera
{
    public abstract class PlayerCamera : MonoBehaviour
    {
        [field: Header("MAIN OBJECTS")]
        [field: SerializeField] public UnityEngine.Camera CameraObject { get; private set; }
        [field: SerializeField] public Transform CameraTransform { get; private set; }
        [field: SerializeField] public PlayerManager PlayerManager { get; set; }

        [Header("CONTROLS SETTINGS")]
        [SerializeField] protected float _upAndDownRotationSpeed = 150;
        [SerializeField] protected float _leftAndRightRotationSpeed = 220;

        [SerializeField] protected float _minimumPivot = -30;
        [SerializeField] protected float _maximumPivot = 60;

        [SerializeField] protected bool _inverseY = true;
        [SerializeField] protected bool _inverseX = false;
        [SerializeField] protected float _sensitiveY;
        [SerializeField] protected float _sensitiveX;

        [field: Header("ROTATION VALUES")]
        [field: SerializeField, ReadOnly] public float LeftAndRightLookAngle { get; protected set; }
        [field: SerializeField, ReadOnly] public float UpAndDownLookAngle { get; protected set; }

        [Header("CAMERA CONFIG")]
        [SerializeField] private PlayerCameraConfig _currentConfig;

        [EasyButtons.Button]

        protected void Awake()
        {
            if (_currentConfig != null)
            {
                ApplyConfig(_currentConfig);
            }
        }
        public virtual void ApplyConfig(PlayerCameraConfig config)
        {
            _currentConfig = config;

            _upAndDownRotationSpeed = config.UpAndDownRotationSpeed;
            _leftAndRightRotationSpeed = config.LeftAndRightRotationSpeed;
            _minimumPivot = config.MinimumPivot;
            _maximumPivot = config.MaximumPivot;
            _inverseY = config.InverseY;
            _inverseX = config.InverseX;
            _sensitiveY = config.SensitiveY;
            _sensitiveX = config.SensitiveX;
        }

        public void ToggleCamera(bool enabled)
        {
            CameraTransform.gameObject.SetActive(enabled);
        }

        public void SyncCamera(PlayerCamera fromCamera)
        {
            LeftAndRightLookAngle = fromCamera.LeftAndRightLookAngle;
            UpAndDownLookAngle = fromCamera.UpAndDownLookAngle;
            SetRotation();
        }



        public abstract void HandleAllCameraActions();

        protected abstract void SetRotation();
    }
}