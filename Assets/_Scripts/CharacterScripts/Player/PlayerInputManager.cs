using System;
using Core.GameInputSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Core.Character.Player
{
    public class PlayerInputManager : MonoBehaviour
    {
        [Header("MOVEMENT INPUT")]
        [field: SerializeField] public Vector2 MovementInput { get; private set; }

        [Header("CAMERA INPUT")]
        [field: SerializeField] public Vector2 CameraInput { get; private set; }
        [field: SerializeField] public float MouseScrollInput { get; private set; }
        [field: SerializeField] public float ZoomInput { get; private set; }

        public event Action OnSwitchCameraViewRequested;
        public event Action<bool> OnSprintRequested;


        [SerializeField] private InputMaps _baseControls;

        [Inject]
        public void Construct(InputMaps baseControls)
        {
            _baseControls = baseControls;
            _baseControls.Enable();
        }

        private void Awake()
        {
            ReadPlayerInputOnce();
        }

        private void ReadPlayerInputOnce()
        {
            _baseControls.Gameplay.Movement.performed += i => MovementInput = i.ReadValue<Vector2>();

            _baseControls.Gameplay.Look.performed += i => CameraInput = i.ReadValue<Vector2>();

            _baseControls.Gameplay.MouseScroll.performed += i => MouseScrollInput = i.ReadValue<float>();

            _baseControls.Gameplay.SwitchCameraView.performed += i => OnSwitchCameraViewRequested?.Invoke();

            _baseControls.Gameplay.Sprint.performed += i => OnSprintRequested?.Invoke(true);
            _baseControls.Gameplay.Sprint.canceled += i => OnSprintRequested?.Invoke(false);
        }

        private void ReadPlayerInputRepeatedly()
        {
            ZoomInput = _baseControls.Gameplay.ZoomCondition.IsInProgress() ? MouseScrollInput : 0;
        }

        private void Update()
        {
            ReadPlayerInputRepeatedly();
        }
    }
}