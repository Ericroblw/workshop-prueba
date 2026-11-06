using NUnit.Framework;
using UnityEngine;
using OffroadRacing.Gameplay;

namespace OffroadRacing.Tests
{
    public sealed class RecoveryTests
    {
        [Test]
        public void RecoveryReturnsCarUprightAtTheLatestCheckpoint()
        {
            var pose = VehicleRecovery.GetRecoveryPose(
                new Vector3(12f, -3f, 8f),
                Quaternion.Euler(170f, 35f, 140f),
                new Vector3(4f, 0f, 6f),
                Vector3.forward);

            Assert.That(pose.position, Is.EqualTo(new Vector3(4f, 0f, 6f)));
            Assert.That(Vector3.Dot(pose.rotation * Vector3.up, Vector3.up), Is.GreaterThan(0.99f));
        }
    }
}
