using UnityEngine;
using UnityEngine.InputSystem;

namespace OffroadRacing.Gameplay
{
    public sealed class VehicleController : MonoBehaviour
    {
        [SerializeField] private float acceleration = 10f;
        [SerializeField] private float steeringSpeed = 80f;
        [SerializeField] private float assistStrength = 0.75f;
        private Vector2 input;
        private float speed;
        private float damage;

        public float Speed { get { return speed; } }
        public bool IsEliminated { get; private set; }
        public float Damage { get { return damage; } }

        public void SetInput(Vector2 value)
        {
            input = AccessibleDriveInput.Normalize(value);
        }

        private void Update()
        {
            if (Keyboard.current != null)
            {
                SetInput(new Vector2(
                    (Keyboard.current.dKey.isPressed ? 1f : 0f) - (Keyboard.current.aKey.isPressed ? 1f : 0f),
                    (Keyboard.current.wKey.isPressed ? 1f : 0f) - (Keyboard.current.sKey.isPressed ? 1f : 0f)));
            }
        }

        private void FixedUpdate()
        {
            speed = Mathf.MoveTowards(speed, input.y * 12f, acceleration * Time.fixedDeltaTime);
            transform.Rotate(Vector3.up, input.x * steeringSpeed * Time.fixedDeltaTime * Mathf.Clamp01(Mathf.Abs(speed) / 4f));
            transform.position += transform.forward * speed * Time.fixedDeltaTime;
            if (transform.position.y < -2f)
            {
                transform.SetPositionAndRotation(new Vector3(0f, 0.6f, 0f), Quaternion.identity);
                speed = 0f;
            }
        }

        public void ApplyCollisionDamage(float impactForce)
        {
            var impact = VehicleDamage.ResolveCollision(Mathf.Max(1f, Mathf.Abs(speed)), impactForce);
            speed *= impact.SpeedMultiplier;
            damage = Mathf.Clamp(damage + impactForce, 0f, 100f);
        }
    }
}
