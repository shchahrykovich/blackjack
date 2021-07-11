using System.Collections.Generic;
using Blackjack.Domain.Game;
using Blackjack.Domain.Game.Cards;

namespace Blackjack.Domain.Players.Strategies
{
    /// <summary>
    /// Due to this strategy player stops taking cards, when reaches 17.
    /// </summary>
    internal class StandOn17Strategy : BaseGameStrategy
    {
        private const int ExpectedScore = 17;

        public override bool ShouldTakeCard(int score, IEnumerable<Card> hand, IEnumerable<Card> dealerHand)
        {
            bool result = (score < ExpectedScore);
            return result;
        }
    }
}
