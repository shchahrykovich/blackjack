using System;
using System.Collections.Generic;
using Blackjack.Domain;
using Blackjack.Domain.Game.Cards;
using Blackjack.Domain.Players;
using Blackjack.Domain.Tests.Helpers;

namespace Blackjack.Web.Tests.Fakes
{
    class FakeBlackjackGame : IBlackjackGame
    {
        private readonly FakeCardPack _pack = new FakeCardPack();

        public bool IsFinished { get; set; }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void MakeTurn(Player player)
        {
            throw new NotImplementedException();
        }

        public BlackjackGameResult GetResult()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Card> GetDealerHand()
        {
            IEnumerable<Card> hand = new[]
                                         {
                                             _pack.GetCard(CardSuit.Hearts, CardType.Seven)
                                         };

            return hand;

        }

        public int GetDealerScore()
        {
            return 15;
        }

        public void FinishGame()
        {
            
        }

        public bool IsGameFinished()
        {
            return IsFinished;
        }
    }
}
