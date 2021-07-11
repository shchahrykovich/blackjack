using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blackjack.Domain.Game.Cards;
using Blackjack.Domain.Players;

namespace Blackjack.Domain.Tests.Helpers
{
    public class FakeBlackjackGame: IBlackjackGame
    {
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
            CardPack pack = Card.GetNewPack();
            IEnumerable<Card> result = new[]
                                           {
                                               pack.GetCard(),
                                           };
            return result;
        }

        public int GetDealerScore()
        {
            throw new NotImplementedException();
        }

        public void FinishGame()
        {
            throw new NotImplementedException();
        }

        public bool IsGameFinished()
        {
            throw new NotImplementedException();
        }
    }
}
