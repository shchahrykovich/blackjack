using System;
using System.Collections.Generic;
using System.Linq;
using Blackjack.Domain.Game.Cards;
using Blackjack.Domain.Players;
using Blackjack.Domain.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Blackjack.Domain.Tests
{
    [TestClass]
    public class BlackjackGameResultTests
    {
        private readonly Player _playerBust = new ComputerPlayer();
        private readonly Player _player15Score = new ComputerPlayer();
        private readonly Player _player20Score = new ComputerPlayer();
        private readonly Player _player21Score = new ComputerPlayer();
        private readonly Player _playerBlackjack = new ComputerPlayer();
        
        private readonly FakeCardPack _pack = new FakeCardPack();

        [TestInitialize]
        public void Initialize()
        {
            _playerBust.TakeCard(_pack.GetCard(CardSuit.Clubs, CardType.Queen));
            _playerBust.TakeCard(_pack.GetCard(CardSuit.Clubs, CardType.King));
            _playerBust.TakeCard(_pack.GetCard(CardSuit.Clubs, CardType.Jack));
            Assert.IsTrue(_playerBust.IsBust);

            _player15Score.TakeCard(_pack.GetCard(CardSuit.Clubs, CardType.Eight));
            _player15Score.TakeCard(_pack.GetCard(CardSuit.Clubs, CardType.Seven));
            Assert.AreEqual(15, _player15Score.Score);

            _player21Score.TakeCard(_pack.GetCard(CardSuit.Clubs, CardType.Ten));
            _player21Score.TakeCard(_pack.GetCard(CardSuit.Clubs, CardType.Two));
            _player21Score.TakeCard(_pack.GetCard(CardSuit.Clubs, CardType.Nine));
            Assert.AreEqual(21, _player21Score.Score);

            _player20Score.TakeCard(_pack.GetCard(CardSuit.Diamonds, CardType.Ten));
            _player20Score.TakeCard(_pack.GetCard(CardSuit.Diamonds, CardType.Queen));
            Assert.AreEqual(20, _player20Score.Score);

            _playerBlackjack.TakeCard(_pack.GetCard(CardSuit.Hearts, CardType.Ace));
            _playerBlackjack.TakeCard(_pack.GetCard(CardSuit.Hearts, CardType.Queen));
            Assert.IsTrue(_playerBlackjack.IsBlackjack);
        }

        [TestMethod]
        public void CreateTest()
        {
            //Arrange
            DateTime now = DateTime.UtcNow;

            //Act
            BlackjackGameResult game = new BlackjackGameResult(_playerBust, new[]
                                                                                {
                                                                                    _playerBust
                                                                                });

            //Assert
            Assert.IsTrue(now <= game.CreateDate && game.CreateDate <= DateTime.UtcNow);
            Assert.IsTrue(!String.IsNullOrWhiteSpace(game.GameId));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateEmptyPlayerFailTest()
        {
            //Act
            BlackjackGameResult game = new BlackjackGameResult(_playerBust, new List<Player>());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateNullPlayersFailTest()
        {
            //Act
            BlackjackGameResult game = new BlackjackGameResult(_playerBust, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateNullDealerFailTest()
        {
            //Act
            BlackjackGameResult game = new BlackjackGameResult(null, new[]
                                                                         {
                                                                             _playerBust
                                                                         });
        }

        [TestMethod]
        public void GetDealerWinnerTest()
        {
            //Arrange
            BlackjackGameResult blackjack = new BlackjackGameResult(_player20Score, new List<Player>{_player15Score});

            //Act
            IEnumerable<Player> result = blackjack.GetWinner();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(_player20Score, result.First());
            Assert.IsFalse(blackjack.IsDraw());
        }

        [TestMethod]
        public void GetPlayerWinnerTest()
        {
            //Arrange
            BlackjackGameResult blackjack = new BlackjackGameResult(_player15Score, new List<Player> { _player20Score });

            //Act
            IEnumerable<Player> result = blackjack.GetWinner();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(_player20Score, result.First());
            Assert.IsFalse(blackjack.IsDraw());
        }

        [TestMethod]
        public void GetDealerBlackjackTest()
        {
            //Arrange
            BlackjackGameResult blackjack = new BlackjackGameResult(_playerBlackjack, new List<Player> { _player20Score });

            //Act
            IEnumerable<Player> result = blackjack.GetWinner();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(_playerBlackjack, result.First());
            Assert.IsFalse(blackjack.IsDraw());
        }

        [TestMethod]
        public void GetDealerBustTest()
        {
            //Arrange
            BlackjackGameResult blackjack = new BlackjackGameResult(_playerBust, new List<Player> { _player20Score });

            //Act
            IEnumerable<Player> result = blackjack.GetWinner();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(_player20Score, result.First());
            Assert.IsFalse(blackjack.IsDraw());
        }

        [TestMethod]
        public void GetPlayerBustTest()
        {
            //Arrange
            BlackjackGameResult blackjack = new BlackjackGameResult(_player20Score, new List<Player> { _playerBust });

            //Act
            IEnumerable<Player> result = blackjack.GetWinner();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(_player20Score, result.First());
            Assert.IsFalse(blackjack.IsDraw());
        }

        [TestMethod]
        public void GetAllPlayersBustTest()
        {
            //Arrange
            BlackjackGameResult blackjack = new BlackjackGameResult(_playerBust, new List<Player> { _playerBust });

            //Act
            IEnumerable<Player> result = blackjack.GetWinner();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(_playerBust, result.ElementAt(0));
            Assert.IsFalse(blackjack.IsDraw());
        }

        [TestMethod]
        public void GetBlackjackWinnerTest()
        {
            //Arrange
            BlackjackGameResult blackjack = new BlackjackGameResult(_player21Score, new List<Player> { _playerBlackjack, _playerBlackjack });

            //Act
            IEnumerable<Player> result = blackjack.GetWinner();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(_playerBlackjack, result.ElementAt(0));
            Assert.AreEqual(_playerBlackjack, result.ElementAt(1));
            Assert.IsFalse(blackjack.IsDraw());
        }

        [TestMethod]
        public void GetBlackjacDrawnWinnerTest()
        {
            //Arrange
            BlackjackGameResult blackjack = new BlackjackGameResult(_playerBlackjack, new List<Player> { _playerBlackjack, _player21Score });

            //Act
            IEnumerable<Player> result = blackjack.GetWinner();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(_playerBlackjack, result.ElementAt(0));
            Assert.AreEqual(_playerBlackjack, result.ElementAt(1));
            Assert.IsTrue(blackjack.IsDraw());
        }

        [TestMethod]
        public void GetDrawnWinnerTest()
        {
            //Arrange
            BlackjackGameResult blackjack = new BlackjackGameResult(_player20Score, new List<Player> { _player20Score, _player15Score });

            //Act
            IEnumerable<Player> result = blackjack.GetWinner();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(_player20Score, result.ElementAt(0));
            Assert.AreEqual(_player20Score, result.ElementAt(1));
            Assert.IsTrue(blackjack.IsDraw());
        }
    }
}
