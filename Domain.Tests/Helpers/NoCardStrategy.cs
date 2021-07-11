using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blackjack.Domain.Game.Cards;
using Blackjack.Domain.Players.Strategies;

namespace Blackjack.Domain.Tests.Helpers
{
    internal class NoCardStrategy: BaseGameStrategy
    {
        public override bool ShouldTakeCard(int score, IEnumerable<Card> hand, IEnumerable<Card> dealerHand)
        {
            return false;
        }
    }
}
