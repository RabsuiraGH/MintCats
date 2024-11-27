using System;
using Core.Character;
using UnityEngine;

namespace Core.Character.Player
{
    public class PlayerManager : CharacterManager
    {
        [field: SerializeField] public PlayerCameraManager PlayerCameraManager { get; private set; }

        // TODO: DO NOT DO THIS
        [SerializeField] public PlayerInputManager _inputManager;

        [SerializeField] private PlayerLocomotionManager _playerLocomotionPlayer;

        protected override void Awake()
        {
            base.Awake();
            _inputManager = GetComponent<PlayerInputManager>();
            _playerLocomotionPlayer = GetComponent<PlayerLocomotionManager>();
        }



        private void HandleAllInput()
        {
            _playerLocomotionPlayer.GetVerticalAndHorizontalInputs(_inputManager.MovementInput);
        }

        protected override void Update()
        {
            base.Update();
            HandleAllInput();
        }

        private void FixedUpdate()
        {
            _playerLocomotionPlayer.HandleAllMovement();
        }

        protected override void LateUpdate()
        {
            base.LateUpdate();

        }
    }
}