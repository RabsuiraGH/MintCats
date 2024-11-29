using UnityEngine;

namespace Core.Character
{
    [CreateAssetMenu(menuName = "Game/Character/CharacterLocomotionConfig", fileName = "CharacterLocomotionConfig")]
    public class CharacterLocomotionConfig : ScriptableObject
    {
        [field: SerializeField] public float MovementSpeed { get; protected set; }
        [field: SerializeField] public float RotationSpeed { get; protected set; }
    }
}