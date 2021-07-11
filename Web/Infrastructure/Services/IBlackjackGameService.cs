using System.Collections.Generic;
using Blackjack.Domain;
using Blackjack.Domain.Players;
using Blackjack.Web.Models;

namespace Blackjack.Web.Infrastructure.Services
{
    /// <summary>
    /// Blackjack service for single player game.
    /// </summary>
    public interface IBlackjackGameService
    {
        /// <summary>
        /// Returns current game.
        /// </summary>
        /// <returns>BlackjackGame.</returns>
        IBlackjackGame GetCurrentGame();

        /// <summary>
        /// Returns current player.
        /// </summary>
        /// <returns>HumanPlayer.</returns>
        HumanPlayer GetCurrentPlayer();

        /// <summary>
        /// Starts new game.
        /// </summary>
        void NewGame();

        BlackjackStatisticModel GetStatistic();
        
        void SaveResults(BlackjackGameResult result);

        bool IsGameExiststs();
    }
}