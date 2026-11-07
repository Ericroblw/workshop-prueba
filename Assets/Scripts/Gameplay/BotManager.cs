using UnityEngine;

namespace OffroadRacing.Gameplay
{
    public sealed class BotManager : MonoBehaviour
    {
        [SerializeField] private int totalSlots = 4;
        [SerializeField] private GameObject botPrefab;
        public bool IsOffline { get; private set; }

        public void FillEmptySlots()
        {
            IsOffline = true;
            var existing = GameObject.FindGameObjectsWithTag("Bot").Length;
            var count = Mathf.Max(0, totalSlots - 1 - existing);
            for (var index = 0; index < count; index++)
            {
                var bot = botPrefab != null
                    ? Instantiate(botPrefab, new Vector3(index * 2f - 3f, 0.6f, -3f), Quaternion.identity)
                    : GameObject.CreatePrimitive(PrimitiveType.Cube);
                bot.name = "BotCar" + index;
                bot.tag = "Bot";
                if (bot.GetComponent<VehicleController>() == null) bot.AddComponent<VehicleController>();
                if (bot.GetComponent<BotController>() == null) bot.AddComponent<BotController>();
                if (bot.GetComponent<Rigidbody>() == null)
                {
                    var body = bot.AddComponent<Rigidbody>();
                    body.isKinematic = true;
                    body.useGravity = false;
                }
            }
        }
    }
}
