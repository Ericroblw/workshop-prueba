using NUnit.Framework;
using OffroadRacing.Gameplay;

namespace OffroadRacing.Tests
{
    public sealed class VehicleDamageTests
    {
        [Test]
        public void CollisionSlowsCarAndShowsDamageWithoutEliminationOrRepair()
        {
            var impact = VehicleDamage.ResolveCollision(speed: 24f, impactForce: 18f);

            Assert.That(impact.SpeedMultiplier, Is.LessThan(1f));
            Assert.That(impact.DamageFeedbackVisible, Is.True);
            Assert.That(impact.IsEliminated, Is.False);
            Assert.That(impact.RequiresRepair, Is.False);
        }
    }
}
