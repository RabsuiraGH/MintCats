using System;
using Unity.XR.OpenVR;
using UnityEngine;

namespace Core.Character
{
    public class CharacterAnimationManager : MonoBehaviour
    {
        [SerializeField] protected Animator _animator;

        public static int MovementXHash { get; private set; }
        public static int MovementZHash { get; private set; }
        public static int RequireToStopHash { get; private set; }
        public static int IsFallingHash { get; private set; }
        public static int IsJumpingHash { get; private set; }

        [SerializeField] private float _targetMovementParameterZ;
        [SerializeField] private float _currentMovementParameterZ;

        [SerializeField] private float _targetMovementParameterX;
        [SerializeField] private float _currentMovementParameterX;
        [SerializeField] private float _movementTransitionCoefficient;

        protected virtual void Awake()
        {
            MovementXHash = Animator.StringToHash("MovementX");
            MovementZHash = Animator.StringToHash("MovementZ");
            RequireToStopHash = Animator.StringToHash("RequireToStop");
            IsFallingHash = Animator.StringToHash("IsFalling");
            IsJumpingHash = Animator.StringToHash("IsJumping");
        }

        public virtual void UpdateMovementParameter(float vertical)
        {
            _targetMovementParameterZ = vertical;
            _currentMovementParameterZ = Mathf.MoveTowards(
                _currentMovementParameterZ,
                _targetMovementParameterZ,
                _movementTransitionCoefficient * (1 + Mathf.Abs(_currentMovementParameterZ - _targetMovementParameterZ)) * Time.deltaTime);

            _animator.SetFloat(MovementZHash, _currentMovementParameterZ);
        }

        public virtual void UpdateSideMovement(float horizontal)
        {
            _targetMovementParameterX = horizontal;
            _currentMovementParameterX = Mathf.MoveTowards(
                _currentMovementParameterX,
                _targetMovementParameterX,
                _movementTransitionCoefficient * (1 + Mathf.Abs(_currentMovementParameterX - _targetMovementParameterX)) * Time.deltaTime);

            _animator.SetFloat(MovementXHash, _currentMovementParameterX);
        }

        public virtual void UpdateFallingParameter(bool isFalling)
        {
            _animator.SetBool(IsFallingHash, isFalling);
        }

        public virtual void TriggerRequireToStop()
        {
            _animator.SetBool(RequireToStopHash, true);
        }

        public virtual void TriggerJump()
        {
            _animator.SetBool(IsJumpingHash, true);
        }
    }
}