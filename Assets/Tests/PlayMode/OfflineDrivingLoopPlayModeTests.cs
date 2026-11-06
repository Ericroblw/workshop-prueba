using System;
using System.Collections;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace OffroadRacing.Tests.PlayMode
{
    public sealed class OfflineDrivingLoopPlayModeTests
    {
        private const string RaceControllerName = "RaceController";
        private const string PlayerCarName = "PlayerCar";
        private const string BotManagerName = "BotManager";

        [UnityTest]
        public IEnumerator StartingRaceMovesControllerIntoRaceState()
        {
            var controller = RequireObject(RaceControllerName);

            InvokeRequired(controller, "StartRace");
            yield return null;

            Assert.That(ReadRequired(controller, "State").ToString(), Is.EqualTo("Racing"));
        }

        [UnityTest]
        public IEnumerator ThirdPersonVehicleRespondsToForwardInput()
        {
            var playerCar = RequireObject(PlayerCarName);
            var camera = Camera.main;
            Assert.That(camera, Is.Not.Null, "A third-person camera must be assigned to the player car.");

            var initialPosition = playerCar.transform.position;
            InvokeRequired(playerCar, "SetInput", Vector2.up);
            yield return new WaitForFixedUpdate();

            Assert.That(playerCar.transform.position, Is.Not.EqualTo(initialPosition));
            Assert.That(Vector3.Distance(camera.transform.position, playerCar.transform.position), Is.GreaterThan(1f));
        }

        [UnityTest]
        public IEnumerator CollisionSlowsVehicleWithoutEliminatingIt()
        {
            var playerCar = RequireObject(PlayerCarName);

            var initialSpeed = Convert.ToSingle(ReadRequired(playerCar, "Speed"));
            InvokeRequired(playerCar, "ApplyCollisionDamage", 1f);
            yield return null;

            Assert.That(Convert.ToSingle(ReadRequired(playerCar, "Speed")), Is.LessThan(initialSpeed));
            Assert.That(ReadRequired(playerCar, "IsEliminated"), Is.EqualTo(false));
            Assert.That(Convert.ToSingle(ReadRequired(playerCar, "Damage")), Is.GreaterThan(0f));
        }

        [UnityTest]
        public IEnumerator FinalLapFinishesRaceAndCreatesReward()
        {
            var controller = RequireObject(RaceControllerName);

            InvokeRequired(controller, "CompleteLap");
            InvokeRequired(controller, "CompleteLap");
            InvokeRequired(controller, "CompleteLap");
            yield return null;

            Assert.That(ReadRequired(controller, "State").ToString(), Is.EqualTo("Finished"));
            Assert.That(ReadRequired(controller, "RewardUnlocked"), Is.EqualTo(true));
        }

        [UnityTest]
        public IEnumerator OfflineFallbackFillsEmptySlotsWithBots()
        {
            var botManager = RequireObject(BotManagerName);

            InvokeRequired(botManager, "FillEmptySlots");
            yield return null;

            Assert.That(GameObject.FindGameObjectsWithTag("Bot"), Is.Not.Empty);
            Assert.That(ReadRequired(botManager, "IsOffline"), Is.EqualTo(true));
        }

        private static GameObject RequireObject(string objectName)
        {
            var gameObject = GameObject.Find(objectName);
            Assert.That(
                gameObject,
                Is.Not.Null,
                $"Checkpoint #2 requires a scene object named '{objectName}'.");
            return gameObject;
        }

        private static void InvokeRequired(GameObject gameObject, string methodName, params object[] arguments)
        {
            var component = FindComponentWithMember(gameObject, methodName);
            Assert.That(component, Is.Not.Null, $"'{gameObject.name}' must implement {methodName}.");
            component.GetType().GetMethod(
                methodName,
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Invoke(component, arguments);
        }

        private static object ReadRequired(GameObject gameObject, string memberName)
        {
            var component = FindComponentWithMember(gameObject, memberName);
            Assert.That(component, Is.Not.Null, $"'{gameObject.name}' must expose {memberName}.");
            var type = component.GetType();
            var property = type.GetProperty(memberName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (property != null)
            {
                return property.GetValue(component);
            }

            return type.GetField(memberName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).GetValue(component);
        }

        private static Component FindComponentWithMember(GameObject gameObject, string memberName)
        {
            foreach (var component in gameObject.GetComponents<Component>())
            {
                if (component == null)
                {
                    continue;
                }

                var type = component.GetType();
                if (type.GetMethod(memberName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) != null ||
                    type.GetProperty(memberName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) != null ||
                    type.GetField(memberName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) != null)
                {
                    return component;
                }
            }

            return null;
        }
    }
}
