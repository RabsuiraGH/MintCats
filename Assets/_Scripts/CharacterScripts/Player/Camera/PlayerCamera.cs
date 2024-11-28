using Core.Character.Player;
using CustomInspector;
using UnityEngine;


namespace Core.Character.Player.Camera
{
    public abstract class PlayerCamera : MonoBehaviour
    {
        [SerializeField] protected PlayerCameraConfig _config;
        [field:Header("MAIN OBJECTS")]
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

        [field:Header("ROTATION VALUES")]
        [field: SerializeField, ReadOnly] public float LeftAndRightLookAngle { get; protected set; }
        [field: SerializeField, ReadOnly] public float UpAndDownLookAngle { get; protected set; }

        public void ApplyConfig(PlayerCameraConfig config)
        {
            _config = config;
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