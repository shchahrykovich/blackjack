using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blackjack.Domain.Game;
using Blackjack.Domain.Game.Cards;
using Blackjack.Domain.Game.States;
using Blackjack.Domain.Players;
using Blackjack.Domain.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Blackjack.Domain.Tests.Game.States
{
    [TestClass]
    public class FinishedStateTests
    {
        private readonly FakeGameState _state = new FakeGameState();
        private readonly BlackjackGame _game = new BlackjackGame(new HumanPlayer());

        [TestMethod]
        public void CreateTest()
        {
            //Arrange
            GameInfo info = new GameInfo(new ComputerPlayer(), new List<Player>(), Card.GetNewPack());

            //Act
            FinishedState state = new FinishedState(_state);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void StartTest()
        {
            //Arrange
            FinishedState state = new FinishedState(_state);

            //Act
            state.Start(_game);
        }
    }
}
