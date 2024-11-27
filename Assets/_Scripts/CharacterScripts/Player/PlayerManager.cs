using System;
using Core.Character;
using Core.Character.Player.Camera;
using UnityEngine;

namespace Core.Character.Player
{
    public class PlayerManager : CharacterManager
    {
        [field: SerializeField] public PlayerCameraManager PlayerCameraManager { get; private set; }

        [field:SerializeField] public PlayerInputManager InputManager { get; private set; }

        [SerializeField] private PlayerLocomotionManager _playerLocomotionManager;

        protected override void Awake()
        {
            base.Awake();
            /*_playerLocomotionManager = GetComponentInChildren<PlayerLocomotionManager>();
            InputManager = GetComponentInChildren<PlayerInputManager>();
            PlayerCameraManager = GetComponentInChildren<PlayerCameraManager>();*/
        }



        private void HandleAllInput()
        {
            _playerLocomotionManager.GetVerticalAndHorizontalInputs(InputManager.MovementInput);
        }

        protected override void Update()
        {
            base.Update();
            HandleAllInput();
        }

        private void FixedUpdate()
        {
            _playerLocomotionManager.HandleAllMovement();
        }

        protected override void LateUpdate()
        {
            base.LateUpdate();
        }
    }
}