using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blackjack.Domain.Game;
using Blackjack.Domain.Game.Cards;
using Blackjack.Domain.Players;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Blackjack.Domain.Tests.Game
{
    [TestClass]
    public class GameInfoTests
    {
        [TestMethod]
        public void CreateTest()
        {
            //Arrange
            Player dealer = new ComputerPlayer();
            List<Player> players = new List<Player>()
                                       {
                                           new HumanPlayer()
                                       };
            CardPack pack = Card.GetNewPack();

            //Act
            GameInfo info = new GameInfo(dealer, players, pack);
            
            //Assert
            Assert.IsNotNull(info.Dealer);
            Assert.IsNotNull(info.Players);
            Assert.AreEqual(1, info.Players.Count());
            Assert.IsNotNull(info.Pack);
        }

        [TestMethod]
        public void GetAllPlayersTest()
        {
            //Arrange
            Player dealer = new ComputerPlayer();
            List<Player> players = new List<Player>()
                                       {
                                           new HumanPlayer(),
                                           new HumanPlayer(),
                                           new HumanPlayer()
                                       };
            CardPack pack = Card.GetNewPack();
            GameInfo info = new GameInfo(dealer, players, pack);

            //Act
            IEnumerable<Player> result = info.GetAllPlayers();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.Count());
        }

        [TestMethod]
        public void NotContainPlayerTest()
        {
            //Arrange
            Player dealer = new ComputerPlayer();
            List<Player> players = new List<Player>()
                                       {
                                           new HumanPlayer(),
                                           new HumanPlayer(),
                                           new HumanPlayer()
                                       };
            CardPack pack = Card.GetNewPack();
            GameInfo info = new GameInfo(dealer, players, pack);

            //Act
            bool result = info.ContainsPlayer(new ComputerPlayer());

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ContainsPlayerTest()
        {
            //Arrange
            Player dealer = new ComputerPlayer();
            Player player = new HumanPlayer();
            List<Player> players = new List<Player>()
                                       {
                                           player,
                                           new HumanPlayer(),
                                           new HumanPlayer()
                                       };
            CardPack pack = Card.GetNewPack();
            GameInfo info = new GameInfo(dealer, players, pack);

            //Act
            bool result = info.ContainsPlayer(player);

            //Assert
            Assert.IsTrue(result);
        }
    }
}
