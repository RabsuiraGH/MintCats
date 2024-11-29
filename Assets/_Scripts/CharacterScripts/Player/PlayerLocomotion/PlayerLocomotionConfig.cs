using Core.Character;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "PlayerLocomotionConfig", menuName = "Game/Player/PlayerLocomotionConfig")]
    public class PlayerLocomotionConfig : CharacterLocomotionConfig
    {
        [field: SerializeField] public float BackwardsMovementSpeed { get; protected set; }
    }
}