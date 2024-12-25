using UnityEngine;

namespace Core.Character
{
    public interface IMovementManager
    {
        public bool IsMoving { get; set;}
        public bool IsRunning { get; set; }
        public bool IsGrounded { get; set;}
        public bool IsJumping { get; set;}
        public bool CanMove { get; set;}
    }
}