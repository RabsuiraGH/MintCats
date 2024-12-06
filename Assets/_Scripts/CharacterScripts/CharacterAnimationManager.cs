using System;
using UnityEngine;

namespace Core.Character
{
    public class CharacterAnimationManager : MonoBehaviour
    {
        [SerializeField] protected Animator _animator;


        [SerializeField] private int _movementXHash;
        [SerializeField] private int _movementZHash;

        protected virtual void Awake()
        {
            _movementXHash = Animator.StringToHash("MovementX");
            _movementZHash = Animator.StringToHash("MovementZ");
        }

        public virtual void UpdateMovementParameters(float horizontal, float vertical)
        {
            _animator.SetFloat(_movementZHash, vertical);
            _animator.SetFloat(_movementXHash, horizontal);
        }

    }
}