using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blackjack.Domain.Game.Cards;
using Blackjack.Domain.Players;

namespace Blackjack.Domain
{
    public interface IBlackjackGame
    {
        void Start();

        void MakeTurn(Player player);

        BlackjackGameResult GetResult();

        IEnumerable<Card> GetDealerHand();

        int GetDealerScore();

        void FinishGame();

        bool IsGameFinished();
    }
}
