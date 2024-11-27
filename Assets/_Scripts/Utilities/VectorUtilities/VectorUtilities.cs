using UnityEngine;

namespace Core.Utility.VectorUtilities
{
    public static class VectorUtilities
    {
        /// <summary>
        /// Returns vector X0Z with magnitude of 1, with angle between direction equal to given angle clockwise
        /// </summary>
        /// <param name="direction">Vector that sets the direction</param>
        /// <param name="angleInDegrees">Angle between result and direction vectors</param>
        /// <returns>Vector with length 1</returns>
        public static Vector3 GetFlatZXVectorAtAngle(Vector3 direction, float angleInDegrees)
        {
            direction.y = 0;
            direction.Normalize();

            float angleInRadians = angleInDegrees * Mathf.Deg2Rad;

            float cos = Mathf.Cos(angleInRadians);
            float sin = Mathf.Sin(angleInRadians);

            return new Vector3(
                direction.x * cos + direction.z * sin,
                0,
                - direction.x * sin + direction.z * cos
            ).normalized;
        }
    }
}