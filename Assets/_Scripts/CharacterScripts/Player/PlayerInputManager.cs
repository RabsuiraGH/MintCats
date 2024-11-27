using System;
using Core.GameInputSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Core.Character.Player
{
    public class PlayerInputManager : MonoBehaviour
    {
        [field: SerializeField] public Vector2 MovementInput { get; private set; }
        [field: SerializeField] public Vector2 CameraInput { get; private set; }

        [field: SerializeField] public float MouseScrollInput { get; private set; }
        [field: SerializeField] public float ZoomInput { get; private set; }

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
            // READ PLAYER MOVEMENT
            _baseControls.Gameplay.Movement.performed += i => MovementInput = i.ReadValue<Vector2>();

            _baseControls.Gameplay.Look.performed += i => CameraInput = i.ReadValue<Vector2>();

            _baseControls.Gameplay.MouseScroll.performed += i => MouseScrollInput = i.ReadValue<float>();
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