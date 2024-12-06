using UnityEngine;

namespace Core.Character
{
    public class CharacterLocomotionManager : MonoBehaviour
    {
        [SerializeField] protected Transform _characterTransform;
        [SerializeField] private CharacterManager _character;
        [field: SerializeField] public CharacterController CharacterController { get; private set; }

        [field: SerializeField] public bool IsRunning { get; protected set; }
        [Header("MOVEMENT SETTINGS")]
        [SerializeField] protected Vector3 _movementDirection;
        [SerializeField] private Vector3 _moveAmount;

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


            _character.CharacterAnimationManager.UpdateMovementParameters(0, normalizedSpeed);
        }

        /*[MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void SetMovementSpeed(float movementSpeed)
        {
            _movementSpeed = movementSpeed;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void SetMovementDirection(Vector3 movementDirection)
        {
            _movementDirection = movementDirection;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void SetMovement(Vector3 movementDirection, float movementSpeed)
        {
            _movementDirection = movementDirection;
            _movementSpeed = movementSpeed;
        }*/

        protected void Update()
        {
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
        }

        protected virtual void HandleGroundedMovement()
        {
            //
        }
    }
}