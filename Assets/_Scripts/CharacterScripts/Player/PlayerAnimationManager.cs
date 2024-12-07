using Core.Character.Player.Camera;
using UnityEngine;

namespace Core.Character.Player
{
    public class PlayerAnimationManager : CharacterAnimationManager
    {
        [SerializeField] private int _firstPersonViewHash;

        protected override void Awake()
        {
            base.Awake();
            _firstPersonViewHash = Animator.StringToHash("FirstPersonView");
        }

        public override void UpdateMovementParameter(float vertical)
        {
            base.UpdateMovementParameter(vertical);
        }

        public void UpdateViewParameter(PlayerViewMode viewMode)
        {
            _animator.SetBool(_firstPersonViewHash, viewMode is PlayerViewMode.FirstPerson);
        }
    }
}