using NUnit.Framework;
using OffroadRacing.Gameplay;

namespace OffroadRacing.Tests
{
    public sealed class RaceRulesTests
    {
        [Test]
        public void CompletedRaceMustFitTheThreeToFiveMinuteTarget()
        {
            var rules = new RaceRules(minimumDurationSeconds: 180, maximumDurationSeconds: 300);

            Assert.That(rules.IsValidDuration(180), Is.True);
            Assert.That(rules.IsValidDuration(300), Is.True);
            Assert.That(rules.IsValidDuration(301), Is.False);
        }

        [Test]
        public void FinishProducesAnOrderedResultAndRewardUnlock()
        {
            var result = RaceRules.Finish(playerId: 7, elapsedSeconds: 214f, position: 1);

            Assert.That(result.IsFinished, Is.True);
            Assert.That(result.Position, Is.EqualTo(1));
            Assert.That(result.Reward.UnlockId, Is.Not.Empty);
        }
    }
}
