using System.Collections.Generic;
using System.Linq;
using Blackjack.Domain.Game.Cards;
using Blackjack.Domain.Players;

namespace Blackjack.Domain.Game.States
{
    internal class FinishedState : GameState
    {
        private BlackjackGameResult _result;

        internal FinishedState(GameState state)
            : base(state)
        {
        }

        internal override BlackjackGameResult GetResult(IBlackjackGame game)
        {
            if(null == _result)
            {
                _result = new BlackjackGameResult(Info.Dealer, Info.Players);
            }
            return _result;
        }

        internal override IEnumerable<Card> GetDealerHand(IBlackjackGame game)
        {
            return Info.Dealer.Hand;
        }

        internal override int GetDealerScore(IBlackjackGame blackjackGame)
        {
            return Info.Dealer.Score;
        }

        internal override bool IsGameFinished(Blackjack.Domain.IBlackjackGame blackjackGame)
        {
            return true;
        }
    }
}
