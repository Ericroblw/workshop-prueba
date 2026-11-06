namespace OffroadRacing.Gameplay
{
    public sealed class RaceRules
    {
        private readonly float minimumDuration;
        private readonly float maximumDuration;

        public RaceRules(float minimumDurationSeconds, float maximumDurationSeconds)
        {
            minimumDuration = minimumDurationSeconds;
            maximumDuration = maximumDurationSeconds;
        }

        public bool IsValidDuration(float seconds)
        {
            return seconds >= minimumDuration && seconds <= maximumDuration;
        }

        public static RaceResult Finish(int playerId, float elapsedSeconds, int position)
        {
            return new RaceResult
            {
                IsFinished = true,
                PlayerId = playerId,
                ElapsedSeconds = elapsedSeconds,
                Position = position,
                Reward = new RaceReward { UnlockId = "mud-runner" }
            };
        }
    }

    public struct RaceReward
    {
        public string UnlockId;
    }

    public struct RaceResult
    {
        public bool IsFinished;
        public int PlayerId;
        public float ElapsedSeconds;
        public int Position;
        public RaceReward Reward;
    }
}
