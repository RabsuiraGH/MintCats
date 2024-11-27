using UnityEngine;

namespace Core.Character
{
    public class CharacterLocomotionManager : MonoBehaviour
    {
        [field: SerializeField] public CharacterController CharacterController { get; private set; }

        [Header("MOVEMENT SETTINGS")]
        [SerializeField] protected Vector3 _movementDirection;

        [SerializeField] protected float _movementSpeed;
        [SerializeField] protected Vector3 _targetRotationDirection;
        [SerializeField] protected float _rotationSpeed = 50;

        protected virtual void Awake()
        {
            CharacterController = GetComponent<CharacterController>();
        }

        protected virtual void SetMovementSpeed(float movementSpeed)
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
        }

        protected virtual void Move()
        {
            CharacterController.Move(_movementDirection * _movementSpeed * Time.fixedDeltaTime);
        }
    }
}