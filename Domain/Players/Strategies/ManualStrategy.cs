using System;
using System.Collections.Generic;
using Blackjack.Domain.Game;
using Blackjack.Domain.Game.Cards;

namespace Blackjack.Domain.Players.Strategies
{
    /// <summary>
    /// This is strategy only for human players.
    /// </summary>
    internal class ManualStrategy : BaseGameStrategy
    {
        public override bool ShouldTakeCard(int score, IEnumerable<Card> hand, IEnumerable<Card> dealerHand)
        {
            throw new NotImplementedException();
        }
    }
}
