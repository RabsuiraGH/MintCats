using UnityEngine;

namespace Core.Character
{
    public class CharacterManager : MonoBehaviour
    {
        [field: SerializeField] public CharacterAnimationManager CharacterAnimationManager { get; private set; }

        [SerializeField] protected CharacterLocomotionManager _characterLocomotionManager;




        protected virtual void Awake()
        {
        }

        protected virtual void Update()
        {
        }

        protected virtual void LateUpdate()
        {
        }

        protected virtual void FixedUpdate()
        {
            _characterLocomotionManager.HandleAllMovement();
        }
    }
}