using NUnit.Framework;
using UnityEngine;
using OffroadRacing.Gameplay;

namespace OffroadRacing.Tests
{
    public sealed class AccessibleDrivingTests
    {
        [Test]
        public void InputIsClampedAndPreservesFullSteeringRange()
        {
            var input = AccessibleDriveInput.Normalize(new Vector2(2f, -2f));

            Assert.That(input.x, Is.EqualTo(1f));
            Assert.That(input.y, Is.EqualTo(-1f));
        }

        [Test]
        public void SteeringAssistReducesHeadingErrorForAccessibleDriving()
        {
            var assisted = SteeringAssist.Apply(Vector3.forward, 0.8f, 0.75f);

            Assert.That(Vector3.Dot(assisted.normalized, Vector3.forward), Is.GreaterThan(0.5f));
        }
    }
}
