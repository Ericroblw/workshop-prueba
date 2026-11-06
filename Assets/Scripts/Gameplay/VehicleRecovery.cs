using UnityEngine;

namespace OffroadRacing.Gameplay
{
    public static class VehicleRecovery
    {
        public static Pose GetRecoveryPose(Vector3 position, Quaternion rotation, Vector3 checkpoint, Vector3 forward)
        {
            var direction = forward.sqrMagnitude > 0.001f ? forward.normalized : Vector3.forward;
            return new Pose(checkpoint, Quaternion.LookRotation(direction, Vector3.up));
        }
    }
}
