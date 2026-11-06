using UnityEngine;

namespace OffroadRacing.Gameplay
{
    public sealed class RaceController : MonoBehaviour
    {
        public enum RaceState { Waiting, Racing, Finished }
        [SerializeField] private int laps = 3;
        private int completedLaps;
        public RaceState State { get; private set; }
        public bool RewardUnlocked { get; private set; }

        public void StartRace()
        {
            completedLaps = 0;
            RewardUnlocked = false;
            State = RaceState.Racing;
        }

        public void CompleteLap()
        {
            if (State == RaceState.Waiting) StartRace();
            if (State != RaceState.Racing) return;
            completedLaps++;
            if (completedLaps >= laps)
            {
                State = RaceState.Finished;
                RewardUnlocked = true;
            }
        }
    }
}
