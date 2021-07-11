using System.Collections.Generic;
using Blackjack.Domain.Game;
using Blackjack.Domain.Game.Cards;

namespace Blackjack.Domain.Players.Strategies
{
    public abstract class BaseGameStrategy
    {
        /// <summary>
        /// Returns true when user should get next card, otherwise false.
        /// </summary>
        /// <param name="score">The score.</param>
        /// <param name="hand">The hand.</param>
        /// <param name="dealerHand">The dealer hand.</param>
        /// <returns></returns>
        public abstract bool ShouldTakeCard(int score, IEnumerable<Card> hand, IEnumerable<Card> dealerHand);
    }
}
