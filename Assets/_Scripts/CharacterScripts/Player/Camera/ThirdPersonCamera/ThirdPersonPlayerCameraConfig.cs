using System;
using UnityEngine;

namespace Core
{
    [Serializable]
    [CreateAssetMenu(fileName = "ThirdPersonCameraConfig", menuName = "Game/Player/Camera/ThirdPersonCameraConfig")]
    public class ThirdPersonPlayerCameraConfig : PlayerCameraConfig
    {
        [field: Header("THIRD PERSON")]
        [field: SerializeField] public float DefaultCameraPosition { get; set; } = -2.5f;
        [field: SerializeField] public float CameraCollisionRadius { get; set; } = 0.2f;
        [field: SerializeField] public float CameraMaxDistance { get; set; } = -5f;
        [field: SerializeField] public float CameraMinDistance { get; set; } = -1.1f;
        [field: SerializeField] public float CameraMaxHeight { get; set; } = 1.6f;
        [field: SerializeField] public float CameraMinHeight { get; set; } = 1.2f;
        [field: SerializeField] public float CameraSmoothTime { get; set; } = 0.01f;
    }
}