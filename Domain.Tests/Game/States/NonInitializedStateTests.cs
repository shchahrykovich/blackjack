using System;
using System.Collections.Generic;
using System.Linq;
using Blackjack.Domain.Game;
using Blackjack.Domain.Game.Cards;
using Blackjack.Domain.Game.States;
using Blackjack.Domain.Players;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Blackjack.Domain.Tests.Game.States
{
    [TestClass]
    public class NonInitializedStateTests
    {
        private readonly BlackjackGame _game = new BlackjackGame(new HumanPlayer());

        [TestMethod]
        public void CreateTest()
        {
            //Arrange
            GameInfo info = new GameInfo(new ComputerPlayer(), new List<Player>(), Card.GetNewPack());

            //Act
            NonInitializedState state = new NonInitializedState(info);
        }

        [TestMethod]
        public void StartTest()
        {
            //Arrange
            GameInfo info = new GameInfo(new ComputerPlayer(), new List<Player>(), Card.GetNewPack());
            NonInitializedState state = new NonInitializedState(info);            

            //Act
            GameState newState = state.Start(_game);

            //Assert
            Assert.IsInstanceOfType(newState, typeof (PlayState));
            foreach(Player player in info.GetAllPlayers())
            {
                Assert.AreEqual(2, player.Hand.Count());
            }
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void MakeTurnTest()
        {
            //Arrange
            GameInfo info = new GameInfo(new ComputerPlayer(), new List<Player>(), Card.GetNewPack());
            NonInitializedState state = new NonInitializedState(info);

            //Act
            state.MakeTurn(_game, new ComputerPlayer());
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void GetResultTest()
        {
            //Arrange
            GameInfo info = new GameInfo(new ComputerPlayer(), new List<Player>(), Card.GetNewPack());
            NonInitializedState state = new NonInitializedState(info);

            //Act
            state.GetResult(_game);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void GetDealerHandTest()
        {
            //Arrange
            GameInfo info = new GameInfo(new ComputerPlayer(), new List<Player>(), Card.GetNewPack());
            NonInitializedState state = new NonInitializedState(info);

            //Act
            state.GetDealerHand(_game);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void GetDealerScoreTest()
        {
            //Arrange
            GameInfo info = new GameInfo(new ComputerPlayer(), new List<Player>(), Card.GetNewPack());
            NonInitializedState state = new NonInitializedState(info);

            //Act
            state.GetDealerScore(_game);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void FinishGameTest()
        {
            //Arrange
            GameInfo info = new GameInfo(new ComputerPlayer(), new List<Player>(), Card.GetNewPack());
            NonInitializedState state = new NonInitializedState(info);

            //Act
            state.FinishGame(_game);
        }
    }
}
