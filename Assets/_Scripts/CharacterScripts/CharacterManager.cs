using UnityEngine;

namespace Core.Character
{
    public class CharacterManager : MonoBehaviour
    {
        [field: SerializeField] public CharacterAnimationManager CharacterAnimationManager { get; private set; }

        [field: SerializeField] public  CharacterLocomotionManager CharacterLocomotionManager{ get; private set; }



        public virtual Transform GetCharacterTransform()
        {
            return CharacterLocomotionManager.CharacterTransform;
        }

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
            CharacterLocomotionManager.HandleAllMovement();
        }
    }
}