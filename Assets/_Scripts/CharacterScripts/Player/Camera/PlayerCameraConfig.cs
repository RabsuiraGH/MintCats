using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "PlayerCameraConfig", menuName = "Game/Player/CameraConfig")]
    public class PlayerCameraConfig : ScriptableObject
    {
        [field: SerializeField] public float CameraSmoothTime { get; set; } = 0.01f;

        [field: SerializeField] public float UpAndDownRotationSpeed { get; set; } = 150;

        [field: SerializeField] public float LeftAndRightRotationSpeed { get; set; } = 220;

        [field: SerializeField] public float MinimumPivot { get; set; } = -30;

        [field: SerializeField] public float MaximumPivot { get; set; } = 60;

        [field: SerializeField] public bool InverseY { get; set; } = true;

        [field: SerializeField] public bool InverseX { get; set; } = false;

        [field: SerializeField] public float SensitiveY { get; set; } = 0.1f;

        [field: SerializeField] public float SensitiveX { get; set; } = 0.1f;

        [field: SerializeField] public float DefaultCameraPosition { get; set; } = -2.5f;
    }
}