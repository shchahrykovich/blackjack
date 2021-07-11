using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blackjack.Domain.Game.Cards;

namespace Blackjack.Domain.Tests.Helpers
{
    public class FakeCardPack
    {
        private readonly CardPack _pack = Card.GetNewPack();

        public Card GetCard(CardSuit suit, CardType type)
        {
            return _pack.First(c => c.Suit == suit && c.Type == type);
        }
    }
}
