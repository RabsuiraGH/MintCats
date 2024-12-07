using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

namespace Core.Character
{
    public class CharacterLocomotionManager : MonoBehaviour
    {
        [field: SerializeField] public Transform CharacterTransform { get; protected set; }
        [SerializeField] private CharacterManager _character;
        [field: SerializeField] public CharacterController CharacterController { get; private set; }

        [field: SerializeField] public bool IsRunning { get; protected set; }

        [Header("MOVEMENT SETTINGS")]
        [SerializeField] protected Vector3 _movementDirection;

        [SerializeField] private float _targetMovementSpeed;
        [SerializeField] protected float _currentMovementSpeed;
        [SerializeField] protected float _movementSpeed;

        [SerializeField] protected float _accelerationSpeed;
        [SerializeField] protected float _decelerationSpeed;
        [SerializeField] protected float _runningAccelerationMultiplier;
        [SerializeField] protected float _runningSpeedMultiplier;

        [SerializeField] protected Vector3 _targetRotationDirection;
        [SerializeField] protected float _rotationSpeed = 50;

        [SerializeField] private CharacterLocomotionConfig _currentConfig;

        protected virtual void Awake()
        {
        }

        [EasyButtons.Button]
        protected virtual void ApplyConfig(CharacterLocomotionConfig config)
        {
            _currentConfig = config;
            _movementSpeed = config.MovementSpeed;
            _rotationSpeed = config.RotationSpeed;
            _accelerationSpeed = config.AccelerationSpeed;
            _decelerationSpeed = config.DecelerationSpeed;
            _runningSpeedMultiplier = config.RunningSpeedMultiplier;
            _runningAccelerationMultiplier = config.RunningAccelerationMultiplier;
        }

        public virtual void SetRunning(bool isRunning)
        {
            IsRunning = isRunning;
        }

        protected virtual void AccelerateTo(float speed)
        {
            float acceleration = IsRunning ? _accelerationSpeed * _runningAccelerationMultiplier : _accelerationSpeed;
            float targetSpeed = IsRunning ? speed * _runningSpeedMultiplier : speed;

            if (_currentMovementSpeed <= targetSpeed)
            {
                _currentMovementSpeed = Mathf.MoveTowards(_currentMovementSpeed, targetSpeed, acceleration * Time.deltaTime);
            }
            else if (_movementDirection == Vector3.zero)
            {
                _currentMovementSpeed = Mathf.MoveTowards(_currentMovementSpeed, 0, _decelerationSpeed * Time.deltaTime);
            }
            else
            {
                _currentMovementSpeed = Mathf.MoveTowards(_currentMovementSpeed, targetSpeed, _decelerationSpeed * Time.deltaTime);
            }

            if (_currentMovementSpeed != 0 && targetSpeed == 0 && _currentMovementSpeed > _movementSpeed)
            {
                _character.CharacterAnimationManager.TriggerRequireToStop();
            }

            float normalizedSpeed = Mathf.Lerp(0, _currentMovementSpeed > _movementSpeed ? 2 : 1, _currentMovementSpeed / _movementSpeed * _runningSpeedMultiplier);

            float forward = Vector3.Dot(_movementDirection, transform.forward);
            float side = Vector3.Dot(_movementDirection, transform.right);

            forward = Mathf.Round(forward);
            side = Mathf.Round(side);

            _character.CharacterAnimationManager.UpdateSideMovement(normalizedSpeed * side);
            _character.CharacterAnimationManager.UpdateMovementParameter(normalizedSpeed * forward);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual void SetTargetMovementSpeed(float movementSpeed)
        {
            _targetMovementSpeed = movementSpeed;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual void SetMovementDirection(Vector3 movementDirection)
        {
            _movementDirection = movementDirection.normalized;
        }

        protected void Update()
        {
            AccelerateTo(_targetMovementSpeed);
        }

        protected void FixedUpdate()
        {
            Move();
        }

        protected virtual void Move()
        {
            CharacterController.Move(_movementDirection * _currentMovementSpeed * Time.fixedDeltaTime);
        }

        public virtual void HandleAllMovement()
        {
            HandleRotation();
            HandleGroundedMovement();
        }

        protected virtual void HandleRotation()
        {
            if(_movementDirection == Vector3.zero)
            {
                _targetRotationDirection = CharacterTransform.forward;
            }
            else
            {
                _targetRotationDirection = _movementDirection;

            }
            Quaternion newRotation = Quaternion.LookRotation(_targetRotationDirection);
            Quaternion targetRotation =
                Quaternion.Slerp(CharacterTransform.rotation, newRotation, _rotationSpeed * Time.fixedDeltaTime);
            CharacterTransform.rotation = targetRotation;
        }

        protected virtual void HandleGroundedMovement()
        {
            //
        }
    }
}