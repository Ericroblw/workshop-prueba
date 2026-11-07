using UnityEngine;

namespace OffroadRacing.Gameplay
{
    public sealed class BotController : MonoBehaviour
    {
        [SerializeField] private float steeringAmplitude = 0.35f;
        [SerializeField] private float steeringPeriod = 4f;
        private VehicleController vehicle;

        private void Awake()
        {
            vehicle = GetComponent<VehicleController>();
        }

        private void Update()
        {
            if (vehicle == null) return;
            var steering = Mathf.Sin(Time.time / Mathf.Max(0.1f, steeringPeriod)) * steeringAmplitude;
            vehicle.SetInput(new Vector2(steering, 1f));
        }
    }
}
