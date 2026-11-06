using System.Collections.Generic;
using System.Linq;

namespace OffroadRacing.Gameplay
{
    public enum BotDifficulty { Accessible, Standard, Expert }

    public sealed class BotAssignment
    {
        public IReadOnlyList<int> HumanPlayerIds { get; internal set; }
        public int BotCount { get; internal set; }
        public BotDifficulty BotDifficulty { get; internal set; }
        public int Count { get { return HumanPlayerIds.Count + BotCount; } }
    }

    public static class BotSlotFiller
    {
        public static BotAssignment Fill(int totalSlots, IEnumerable<int> humanPlayerIds, BotDifficulty difficulty)
        {
            var humans = humanPlayerIds.Distinct().Take(totalSlots).ToList();
            return new BotAssignment
            {
                HumanPlayerIds = humans,
                BotCount = totalSlots - humans.Count,
                BotDifficulty = difficulty
            };
        }
    }
}
