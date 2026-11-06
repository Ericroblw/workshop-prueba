using UnityEngine;

namespace OffroadRacing.Gameplay
{
    public struct CollisionImpact
    {
        public float SpeedMultiplier;
        public bool DamageFeedbackVisible;
        public bool IsEliminated;
        public bool RequiresRepair;
    }

    public static class VehicleDamage
    {
        public static CollisionImpact ResolveCollision(float speed, float impactForce)
        {
            var slowdown = Mathf.Clamp01(impactForce / Mathf.Max(1f, speed + impactForce));
            return new CollisionImpact
            {
                SpeedMultiplier = 1f - slowdown * 0.65f,
                DamageFeedbackVisible = impactForce > 0f,
                IsEliminated = false,
                RequiresRepair = false
            };
        }
    }
}
