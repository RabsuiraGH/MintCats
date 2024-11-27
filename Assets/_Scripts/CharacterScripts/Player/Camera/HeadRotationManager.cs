using System;
using Core.Utility.VectorUtilities;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Core
{
    public class HeadRotationManager : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        [SerializeField] private Transform _camera;
        [SerializeField] private MultiAimConstraint _headAimConstraint;
        [SerializeField] private Transform _aim;
        [SerializeField] private float _angleLimit = 80f;
        [SerializeField] private float _maxAngle = 180;

        private void OnValidate()
        {
            if (_headAimConstraint == null) return;
            _headAimConstraint.data.limits = new Vector2(_headAimConstraint.data.limits.x, _angleLimit);
        }

        void LateUpdate()
        {
            Vector3 playerForward = _player.forward;

            Vector3 toCamera = (_camera.position - _player.position).normalized;
            toCamera.y = 0;
            float angle = Vector3.Angle(playerForward, toCamera);
            _headAimConstraint.weight = 1 - (_maxAngle - angle) / _maxAngle;
        }

        private void OnDrawGizmosSelected()
        {
            if (_player == null || _camera == null) return;

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