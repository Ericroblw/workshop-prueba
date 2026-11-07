using UnityEngine;

namespace OffroadRacing.Gameplay
{
    public sealed class RaceFinishTrigger : MonoBehaviour
    {
        [SerializeField] private RaceController raceController;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            if (raceController != null) raceController.CompleteLap();
        }
    }
}
