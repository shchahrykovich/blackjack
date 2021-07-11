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
    public class PlayStateTests
    {
        private readonly BlackjackGame _game = new BlackjackGame(new HumanPlayer());

        [TestMethod]
        public void CreateTest()
        {
            //Arrange
            GameInfo info = new GameInfo(new ComputerPlayer(), new List<Player>(), Card.GetNewPack());
            NonInitializedState initialState = new NonInitializedState(info);

            //Act
            PlayState state = new PlayState(initialState);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void StartFailTest()
        {
            //Arrange
            GameInfo info = new GameInfo(new ComputerPlayer(), new List<Player>(), Card.GetNewPack());
            NonInitializedState initialState = new NonInitializedState(info);
            PlayState state = new PlayState(initialState);

            //Act
            GameState newState = state.Start(_game);           
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void GetResultFailTest()
        {
            //Arrange
            GameInfo info = new GameInfo(new ComputerPlayer(), new List<Player>(), Card.GetNewPack());
            NonInitializedState initialState = new NonInitializedState(info);
            PlayState state = new PlayState(initialState);

            //Act
            state.GetResult(_game);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void GetDealerScoreFailTest()
        {
            //Arrange
            GameInfo info = new GameInfo(new ComputerPlayer(), new List<Player>(), Card.GetNewPack());
            NonInitializedState initialState = new NonInitializedState(info);
            PlayState state = new PlayState(initialState);

            //Act
            state.GetDealerScore(_game);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MakeTurnEmptyGameFailTest()
        {
            //Arrange
            GameInfo info = new GameInfo(new ComputerPlayer(), new List<Player>(), Card.GetNewPack());
            NonInitializedState initialState = new NonInitializedState(info);
            PlayState state = new PlayState(initialState);

            //Act
            state.MakeTurn(null, new ComputerPlayer());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MakeTurnEmptyPlayerFailTest()
        {
            //Arrange
            GameInfo info = new GameInfo(new ComputerPlayer(), new List<Player>(), Card.GetNewPack());
            NonInitializedState initialState = new NonInitializedState(info);
            PlayState state = new PlayState(initialState);

            //Act
            state.MakeTurn(new FakeBlackjackGame(), null);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void MakeTurnFinishedPlayerFailTest()
        {
            //Arrange
            Player player = new ComputerPlayer(new NoCardStrategy());
            GameInfo info = new GameInfo(new ComputerPlayer(), new List<Player> {player}, Card.GetNewPack());
            NonInitializedState initialState = new NonInitializedState(info);
            PlayState state = new PlayState(initialState);
            player.ShouldTakeCard(new FakeBlackjackGame());

            //Act
            state.MakeTurn(new FakeBlackjackGame(), player);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MakeTurnNewPlayerFailTest()
        {
            //Arrange
            Player player = new ComputerPlayer(new LoserStrategy());
            GameInfo info = new GameInfo(new ComputerPlayer(), new List<Player> { player }, Card.GetNewPack());
            NonInitializedState initialState = new NonInitializedState(info);
            PlayState state = new PlayState(initialState);
            player.ShouldTakeCard(new FakeBlackjackGame());

            //Act
            state.MakeTurn(new FakeBlackjackGame(), new ComputerPlayer());
        }

        [TestMethod]
        public void MakeTurnTest()
        {
            //Arrange
            Player player = new ComputerPlayer(new NoCardStrategy());
            GameInfo info = new GameInfo(new ComputerPlayer(), new List<Player> { player }, Card.GetNewPack());
            NonInitializedState initialState = new NonInitializedState(info);
            PlayState state = new PlayState(initialState);

            //Act
            GameState newState = state.MakeTurn(new FakeBlackjackGame(), player);

            //Assert
            Assert.IsNotNull(newState);
            Assert.AreEqual(newState, state);
        }

        [TestMethod]
        public void MakeTurnFinishTest()
        {
            //Arrange
            Player player = new ComputerPlayer(new NoCardStrategy());
            Player dealer = new ComputerPlayer(new NoCardStrategy());
            GameInfo info = new GameInfo(dealer, new List<Player> { player }, Card.GetNewPack());
            NonInitializedState initialState = new NonInitializedState(info);
            PlayState state = new PlayState(initialState);
            state.MakeTurn(new FakeBlackjackGame(), player);

            //Act
            GameState newState = state.MakeTurn(new FakeBlackjackGame(), dealer);

            //Assert
            Assert.IsNotNull(newState);
            Assert.AreNotEqual(newState, state);
            Assert.IsInstanceOfType(newState, typeof(FinishedState));
        }

        [TestMethod]
        public void FinishGameTest()
        {
            //Arrange
            Player player = new ComputerPlayer(new NoCardStrategy());
            Player dealer = new ComputerPlayer(new NoCardStrategy());
            GameInfo info = new GameInfo(dealer, new List<Player> { player }, Card.GetNewPack());
            NonInitializedState initialState = new NonInitializedState(info);
            PlayState state = new PlayState(initialState);
            state.MakeTurn(new FakeBlackjackGame(), player);
            state.MakeTurn(new FakeBlackjackGame(), dealer);

            //Act
            GameState newState = state.FinishGame(new FakeBlackjackGame());

            //Assert
            Assert.IsNotNull(newState);
            Assert.AreNotEqual(newState, state);
            Assert.IsInstanceOfType(newState, typeof(FinishedState));
        }

        [TestMethod]
        public void FinishGameSecondTest()
        {
            //Arrange
            Player player = new ComputerPlayer(new NoCardStrategy());
            Player dealer = new ComputerPlayer(new NoCardStrategy());
            GameInfo info = new GameInfo(dealer, new List<Player> { player }, Card.GetNewPack());
            NonInitializedState initialState = new NonInitializedState(info);
            PlayState state = new PlayState(initialState);

            //Act
            Assert.IsFalse(dealer.IsFinishedTurn);
            Assert.IsFalse(player.IsFinishedTurn);
            GameState newState = state.FinishGame(new FakeBlackjackGame());

            //Assert
            Assert.IsTrue(dealer.IsFinishedTurn);
            Assert.IsTrue(player.IsFinishedTurn);
            Assert.IsNotNull(newState);
            Assert.AreNotEqual(newState, state);
            Assert.IsInstanceOfType(newState, typeof(FinishedState));
        }
    }
}
