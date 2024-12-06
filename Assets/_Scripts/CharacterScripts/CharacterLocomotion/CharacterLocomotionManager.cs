using UnityEngine;

namespace Core.Character
{
    public class CharacterLocomotionManager : MonoBehaviour
    {
        [SerializeField] protected Transform _characterTransform;
        [field: SerializeField] public CharacterController CharacterController { get; private set; }

        [Header("MOVEMENT SETTINGS")]
        [SerializeField] protected Vector3 _movementDirection;

        [SerializeField] protected float _currentMovementSpeed;
        [SerializeField] protected float _movementSpeed;
        [SerializeField] protected Vector3 _targetRotationDirection;
        [SerializeField] protected float _rotationSpeed = 50;

        [SerializeField] private CharacterLocomotionConfig _currentConfig;

        protected virtual void Awake()
        {
            if (_currentConfig != null)
            {
                ApplyConfig(_currentConfig);
            }
        }

        [EasyButtons.Button]
        protected virtual void ApplyConfig(CharacterLocomotionConfig config)
        {
            _currentConfig = config;
            _movementSpeed = config.MovementSpeed;
            _rotationSpeed = config.RotationSpeed;
        }



        /*protected virtual void SetMovementSpeed(float movementSpeed)
        {
            _movementSpeed = movementSpeed;
        }

        protected virtual void SetMovementDirection(Vector3 movementDirection)
        {
            _movementDirection = movementDirection;
        }

        protected virtual void SetMovement(Vector3 movementDirection, float movementSpeed)
        {
            _movementDirection = movementDirection;
            _movementSpeed = movementSpeed;
        }*/

        protected virtual void Move()
        {
            CharacterController.Move(_movementDirection * _currentMovementSpeed * Time.fixedDeltaTime);
        }
    }
}