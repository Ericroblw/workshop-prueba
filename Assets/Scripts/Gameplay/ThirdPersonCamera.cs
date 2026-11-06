using UnityEngine;

namespace OffroadRacing.Gameplay
{
    public sealed class ThirdPersonCamera : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 offset = new Vector3(0f, 4f, -7f);

        private void LateUpdate()
        {
            if (target == null) return;
            transform.position = target.position + target.rotation * offset;
            transform.LookAt(target.position + Vector3.up);
        }
    }
}
