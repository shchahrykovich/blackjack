using System.Collections.Generic;
using System.Linq;
using Blackjack.Domain.Players;
using Blackjack.Domain.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Blackjack.Domain.Tests
{
    [TestClass]
    public class BlackjackGameTests
    {
        [TestMethod]
        public void CreateTest()
        {
            //Act
            BlackjackGame game = new BlackjackGame(new HumanPlayer(), new List<Player>());
        }

        [TestMethod]
        public void SimpleGameTest()
        {
            //Assert
            BlackjackGame game = new BlackjackGame(new ComputerPlayer());

            //Act
            game.Start();
            game.FinishGame();
            BlackjackGameResult result = game.GetResult();

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DealerWinTest()
        {
            //Assert
            Player dealer = new ComputerPlayer(new NoCardStrategy());
            IEnumerable<Player> players = new[]
                                              {
                                                  new ComputerPlayer(new LoserStrategy()),
                                                  new ComputerPlayer(new LoserStrategy()),
                                                  new ComputerPlayer(new LoserStrategy())
                                              };
            BlackjackGame game = new BlackjackGame(dealer, players);

            //Act
            game.Start();
            game.FinishGame();
            BlackjackGameResult result = game.GetResult();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(1 <= result.GetWinner().Count());
            Assert.AreEqual(dealer, result.GetWinner().First());
        }

        [TestMethod]
        public void PlayerWinTest()
        {
            //Assert
            Player dealer = new ComputerPlayer(new LoserStrategy());
            IEnumerable<Player> players = new[]
                                              {
                                                  new ComputerPlayer(new LoserStrategy()),
                                                  new ComputerPlayer(new NoCardStrategy()),
                                                  new ComputerPlayer(new LoserStrategy())
                                              };
            Player player = players.ElementAt(1);
            BlackjackGame game = new BlackjackGame(dealer, players);

            //Act
            game.Start();
            game.FinishGame();
            BlackjackGameResult result = game.GetResult();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(1 <= result.GetWinner().Count());
            Assert.AreEqual(player, result.GetWinner().First());
        }

        [TestMethod]
        public void HumanDealerNoCardStrategyWinTest()
        {
            //Assert
            HumanPlayer dealer = new HumanPlayer();
            IEnumerable<Player> players = new[]
                                              {
                                                  new ComputerPlayer(new LoserStrategy()),
                                                  new ComputerPlayer(new LoserStrategy()),
                                                  new ComputerPlayer(new LoserStrategy())
                                              };
            BlackjackGame game = new BlackjackGame(dealer, players);

            //Act
            game.Start();
            dealer.Stand();
            game.FinishGame();
            BlackjackGameResult result = game.GetResult();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(1 <= result.GetWinner().Count());
            Assert.AreEqual(dealer, result.GetWinner().First());
        }

        [TestMethod]
        public void HumanDealerStandOn11WinTest()
        {
            //Assert
            HumanPlayer dealer = new HumanPlayer();
            IEnumerable<Player> players = new[]
                                              {
                                                  new ComputerPlayer(new LoserStrategy()),
                                                  new ComputerPlayer(new LoserStrategy()),
                                                  new ComputerPlayer(new LoserStrategy())
                                              };
            BlackjackGame game = new BlackjackGame(dealer, players);

            //Act
            game.Start();
            while (!dealer.IsFinishedTurn)
            {
                if (dealer.Score < 11)
                {
                    dealer.Hit();
                }
                else
                {
                    dealer.Stand();
                }
                game.MakeTurn(dealer);
            }
            game.FinishGame();
            BlackjackGameResult result = game.GetResult();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(1 <= result.GetWinner().Count());
            Assert.AreEqual(dealer, result.GetWinner().First());
        }

        [TestMethod]
        public void HumanDealerLoseTest()
        {
            //Assert
            HumanPlayer dealer = new HumanPlayer();
            IEnumerable<Player> players = new[]
                                              {
                                                  new ComputerPlayer(new LoserStrategy()),
                                                  new ComputerPlayer(new NoCardStrategy()),
                                                  new ComputerPlayer(new LoserStrategy())
                                              };
            Player player = players.ElementAt(1);
            BlackjackGame game = new BlackjackGame(dealer, players);

            //Act
            game.Start();
            while (!dealer.IsFinishedTurn)
            {
                dealer.Hit();
                game.MakeTurn(dealer);
            }
            game.FinishGame();
            BlackjackGameResult result = game.GetResult();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(1 <= result.GetWinner().Count());
            Assert.AreEqual(player, result.GetWinner().First());
        }

        [TestMethod]
        public void HumanPlayerWinTest()
        {
            //Assert
            ComputerPlayer dealer = new ComputerPlayer(new LoserStrategy());
            IEnumerable<Player> players = new List<Player>
                                              {
                                                  new ComputerPlayer(new LoserStrategy()),
                                                  new HumanPlayer(),
                                                  new ComputerPlayer(new LoserStrategy())
                                              };
            HumanPlayer player = (HumanPlayer)players.ElementAt(1);
            BlackjackGame game = new BlackjackGame(dealer, players);

            //Act
            game.Start();
            player.Stand();
            while (!player.IsFinishedTurn)
            {
                player.Stand();
                game.MakeTurn(player);
            }
            game.FinishGame();
            BlackjackGameResult result = game.GetResult();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(1 <= result.GetWinner().Count());
            Assert.AreEqual(player, result.GetWinner().First());
        }
    }
}
