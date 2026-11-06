using NUnit.Framework;
using OffroadRacing.Gameplay;

namespace OffroadRacing.Tests
{
    public sealed class BotSlotTests
    {
        [Test]
        public void EmptyRaceSlotsAreFilledWithoutReplacingHumans()
        {
            var assignment = BotSlotFiller.Fill(
                totalSlots: 4,
                humanPlayerIds: new[] { 10, 11 },
                difficulty: BotDifficulty.Accessible);

            Assert.That(assignment.Count, Is.EqualTo(4));
            Assert.That(assignment.HumanPlayerIds, Does.Contain(10));
            Assert.That(assignment.HumanPlayerIds, Does.Contain(11));
            Assert.That(assignment.BotCount, Is.EqualTo(2));
            Assert.That(assignment.BotDifficulty, Is.EqualTo(BotDifficulty.Accessible));
        }
    }
}
