using System.Linq;
using Core.Utility.VectorUtilities;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Core.Character.Player.Camera
{
    public class HeadRotationManager : MonoBehaviour
    {
        [SerializeField] private PlayerCameraManager _playerCameraManager;

        [SerializeField] private Transform _player;
        [SerializeField] private Transform _thirdPersonCamera;
        [SerializeField] private MultiAimConstraint _headAimConstraint;
        [SerializeField] private Transform _firstPersonAim;
        [SerializeField] private Transform _thirdPersonAim;
        [SerializeField] private float _angleLimit = 80f;
        [SerializeField] private float _maxAngle = 180;

        [SerializeField] private bool _processThirdPersonAiming;

        private void Awake()
        {
            _playerCameraManager.OnPlayerViewChanged += OnPlayerViewChanged;
        }

        private void OnPlayerViewChanged(PlayerCamera camera, PlayerViewMode viewMode)
        {
            WeightedTransformArray sources = _headAimConstraint.data.sourceObjects;

            int firstAimIndex = sources.IndexOf(sources.FirstOrDefault(x => x.transform == _firstPersonAim));
            int thirdAimIndex = sources.IndexOf(sources.FirstOrDefault(x => x.transform == _thirdPersonAim));

            WeightedTransform firstAimSource = sources[firstAimIndex];
            WeightedTransform thirdAimSource = sources[thirdAimIndex];

            if (viewMode is PlayerViewMode.FirstPerson)
            {
                firstAimSource.weight = 1;
                thirdAimSource.weight = 0;
                _processThirdPersonAiming = false;
            }
            else if (viewMode is PlayerViewMode.ThirdPerson)
            {
                firstAimSource.weight = 0;
                thirdAimSource.weight = 1;
                _processThirdPersonAiming = true;
            }

            sources[firstAimIndex] = firstAimSource;
            sources[thirdAimIndex] = thirdAimSource;

            _headAimConstraint.data.sourceObjects = sources;
        }

        private void OnValidate()
        {
            if (_headAimConstraint == null) return;
            _headAimConstraint.data.limits = new Vector2(_headAimConstraint.data.limits.x, _angleLimit);
        }

        void LateUpdate()
        {
            SetThirdPersonAimWeight();
        }

        private void SetThirdPersonAimWeight()
        {
            if (!_processThirdPersonAiming) return;

            Vector3 playerForward = _player.forward;

            Vector3 toCamera = (_thirdPersonCamera.position - _player.position).normalized;
            toCamera.y = 0;
            float angle = Vector3.Angle(playerForward, toCamera);
            _headAimConstraint.weight = 1 - (_maxAngle - angle) / _maxAngle;
        }

        private void OnDrawGizmosSelected()
        {
            if (_player == null || _thirdPersonCamera == null) return;

            Vector3 playerPos = _player.position;
            Vector3 forward = _player.forward;
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(playerPos, playerPos + forward);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(playerPos, playerPos + VectorUtilities.GetFlatZXVectorAtAngle(forward, _angleLimit) * 5f);
            Gizmos.DrawLine(playerPos, playerPos + VectorUtilities.GetFlatZXVectorAtAngle(forward, -_angleLimit) * 5f);
        }
    }
}