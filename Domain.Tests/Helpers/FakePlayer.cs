using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blackjack.Domain.Game.Cards;
using Blackjack.Domain.Players;
using Blackjack.Domain.Players.Strategies;

namespace Blackjack.Domain.Tests.Helpers
{
    public class FakePlayer: Player
    {
        private readonly FakeCardPack _pack = new FakeCardPack();

        public int FakeScore;        

        public FakePlayer() : base(new StandOn17Strategy())
        {
        }

        public override int Score
        {
            get
            {
                return FakeScore;
            }
        }

        public override IEnumerable<Card> Hand
        {
            get
            {
                List<Card> hand = new List<Card>();
                hand.Add(_pack.GetCard(CardSuit.Hearts, CardType.Ace));

                return hand;
            }
        }
    }
}
