using System;
using UnityEngine;

namespace Core.Character
{
    public class CharacterAnimationManager : MonoBehaviour
    {
        [SerializeField] protected Animator _animator;


        [SerializeField] private int _movementXHash;
        [SerializeField] private int _movementZHash;
        [SerializeField] private int _requireToStopHash;

        [SerializeField] private float _targetMovementParameterZ;
        [SerializeField] private float _currentMovementParameterZ;

        [SerializeField] private float _targetMovementParameterX;
        [SerializeField] private float _currentMovementParameterX;
        [SerializeField] private float _movementTransitionCoefficient;


        protected virtual void Awake()
        {
            _movementXHash = Animator.StringToHash("MovementX");
            _movementZHash = Animator.StringToHash("MovementZ");
            _requireToStopHash = Animator.StringToHash("RequireToStop");
        }

        public virtual void UpdateMovementParameter(float vertical)
        {
            _targetMovementParameterZ = vertical;
            _currentMovementParameterZ = Mathf.MoveTowards(
                _currentMovementParameterZ,
                _targetMovementParameterZ,
                _movementTransitionCoefficient * (1 + Mathf.Abs(_currentMovementParameterZ - _targetMovementParameterZ)) * Time.deltaTime);

            _animator.SetFloat(_movementZHash, _currentMovementParameterZ);
        }
        public virtual void UpdateSideMovement(float horizontal)
        {
            _targetMovementParameterX = horizontal;
            _currentMovementParameterX = Mathf.MoveTowards(
                _currentMovementParameterX,
                _targetMovementParameterX,
                _movementTransitionCoefficient * (1 + Mathf.Abs(_currentMovementParameterX - _targetMovementParameterX)) * Time.deltaTime);

            _animator.SetFloat(_movementXHash, _currentMovementParameterX);
        }


        public virtual void TriggerRequireToStop()
        {
            _animator.SetBool(_requireToStopHash, true);
        }

    }
}