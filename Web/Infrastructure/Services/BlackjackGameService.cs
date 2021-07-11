using System;
using System.Linq;
using System.Web;
using Blackjack.Domain;
using Blackjack.Domain.Players;
using Blackjack.Web.Models;
using EnsureUtility;

namespace Blackjack.Web.Infrastructure.Services
{
    /// <summary>
    /// Blackjack service for single player game.
    /// </summary>
    public class BlackjackGameService : IBlackjackGameService
    {
        private readonly HttpSessionStateBase _storage;

        private const String GameKey = "Blackajck";
        private const String PlayerKey = "Player";
        private const String StatisticKey = "Statistic";

        #region Public Methods

        public BlackjackGameService(HttpSessionStateBase storage)
        {
            _storage = Ensure<HttpSessionStateBase>.NotNull(storage, "storage");
        }

        public IBlackjackGame GetCurrentGame()
        {
            IBlackjackGame game = _storage[GameKey] as IBlackjackGame;
            if (null == game)
            {
                HumanPlayer player = GetCurrentPlayer();
                game = new BlackjackGame(player);
                game.Start();
                _storage[GameKey] = game;
            }

            return game;
        }

        public HumanPlayer GetCurrentPlayer()
        {
            HumanPlayer player = _storage[PlayerKey] as HumanPlayer;
            if (null == player)
            {
                player = new HumanPlayer();
                _storage[PlayerKey] = player;
            }

            return player;
        }

        public void NewGame()
        {
            _storage[GameKey] = null;
            _storage[PlayerKey] = null;
        }

        public BlackjackStatisticModel GetStatistic()
        {
            BlackjackStatisticModel statistic = _storage[StatisticKey] as BlackjackStatisticModel;
            if (null == statistic)
            {
                statistic = new BlackjackStatisticModel();
                _storage[StatisticKey] = statistic;
            }

            return statistic;
        }

        public void SaveResults(BlackjackGameResult result)
        {
            bool isPlayerWin = false;
            if (!result.IsDraw())
            {
                isPlayerWin = IsPlayerWin(result);
            }
            int playerScore = result.GetAllPlayers().ElementAt(0).Score;
            //Dealer is always last in this list.
            int dealerScore = result.GetAllPlayers().ElementAt(1).Score;

            BlackjackStatisticModel statistic = GetStatistic();
            if (result.IsDraw())
            {
                statistic.AddDrawRound(playerScore, dealerScore);
            }
            else if (isPlayerWin)
            {
                statistic.AddPlayerRound(playerScore, dealerScore);
            }
            else
            {
                statistic.AddDealerRound(playerScore, dealerScore);
            }
        }

        public bool IsGameExiststs()
        {
            bool result = (null != _storage[GameKey] && !GetCurrentGame().IsGameFinished());
            return result;
        } 

        #endregion

        #region Helper Methods
        
        private bool IsPlayerWin(BlackjackGameResult gameResult)
        {
            Player player = GetCurrentPlayer();
            bool result = gameResult.GetWinner().Contains(player);
            return result;
        } 

        #endregion
    }
}