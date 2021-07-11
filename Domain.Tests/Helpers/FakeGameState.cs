using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blackjack.Domain.Game;
using Blackjack.Domain.Game.Cards;
using Blackjack.Domain.Game.States;
using Blackjack.Domain.Players;

namespace Blackjack.Domain.Tests.Helpers
{
    internal class FakeGameState: GameState
    {
        public FakeGameState(GameInfo info) : base(info)
        {
        }

        public FakeGameState(GameState state) : base(state)
        {
        }

        public FakeGameState()
            : base(new GameInfo(new ComputerPlayer(), new [] { new ComputerPlayer() }, Card.GetNewPack()))
        {
            
        }

        internal override GameState Start(IBlackjackGame game)
        {
            throw new NotImplementedException();
        }

        internal override GameState MakeTurn(IBlackjackGame game, Player player)
        {
            throw new NotImplementedException();
        }

        internal override BlackjackGameResult GetResult(IBlackjackGame game)
        {
            throw new NotImplementedException();
        }

        internal override IEnumerable<Card> GetDealerHand(IBlackjackGame game)
        {
            throw new NotImplementedException();
        }
    }
}
