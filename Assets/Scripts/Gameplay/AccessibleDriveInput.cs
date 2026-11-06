using UnityEngine;

namespace OffroadRacing.Gameplay
{
    public static class AccessibleDriveInput
    {
        public static Vector2 Normalize(Vector2 input)
        {
            return new Vector2(Mathf.Clamp(input.x, -1f, 1f), Mathf.Clamp(input.y, -1f, 1f));
        }
    }

    public static class SteeringAssist
    {
        public static Vector3 Apply(Vector3 heading, float steering, float assistStrength)
        {
            var target = Quaternion.Euler(0f, steering * 30f * assistStrength, 0f) * heading;
            return Vector3.Lerp(target, heading, Mathf.Clamp01(assistStrength * 0.35f));
        }
    }
}
