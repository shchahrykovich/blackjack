using System;
using System.Collections.Generic;
using Blackjack.Domain.Game.Cards;
using Blackjack.Domain.Players;
using EnsureUtility;

namespace Blackjack.Domain.Game.States
{
    /// <summary>
    /// Base class for state of the game.
    /// See: State pattern (GoF).
    /// </summary>
    internal abstract class GameState
    {
        #region Properties

        protected GameInfo Info { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="GameState"/> class.
        /// </summary>
        /// <param name="info">The info.</param>
        protected GameState(GameInfo info)
        {
            Info = Ensure<GameInfo>.NotNull(info, "info");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameState"/> class.
        /// </summary>
        /// <param name="state">The state.</param>
        protected GameState(GameState state): this(state.Info)
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Starts the game.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <returns></returns>
        internal virtual GameState Start(IBlackjackGame game)
        {
            String errorMessage = GetErrorMessage("Start");
            throw new NotSupportedException(errorMessage);
        }

        /// <summary>
        /// Makes the turn.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <param name="player">The player.</param>
        /// <returns></returns>
        internal virtual GameState MakeTurn(IBlackjackGame game, Player player)
        {
            String errorMessage = GetErrorMessage("MakeTurn");
            throw new NotSupportedException(errorMessage);
        }

        /// <summary>
        /// Finishes the game.
        /// </summary>
        /// <param name="game">The game.</param>
        internal virtual GameState FinishGame(IBlackjackGame game)
        {
            String errorMessage = GetErrorMessage("FinishGame");
            throw new NotSupportedException(errorMessage);            
        }

        /// <summary>
        /// Returns winner if available.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <returns>Player instance or throws exception.</returns>
        internal virtual BlackjackGameResult GetResult(IBlackjackGame game)
        {
            String errorMessage = GetErrorMessage("GetResult");
            throw new NotSupportedException(errorMessage);
        }

        /// <summary>
        /// Gets the dealer hand.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <returns>Retunrs cards.</returns>
        internal virtual IEnumerable<Card> GetDealerHand(IBlackjackGame game)
        {
            String errorMessage = GetErrorMessage("GetDealerHand");
            throw new NotSupportedException(errorMessage);
        }

        /// <summary>
        /// Gets the dealer score.
        /// </summary>
        /// <param name="blackjackGame">The blackjack game.</param>
        /// <returns></returns>
        internal virtual int GetDealerScore(IBlackjackGame blackjackGame)
        {
            String errorMessage = GetErrorMessage("GetDealerScore");
            throw new NotSupportedException(errorMessage);
        }

        /// <summary>
        /// Determines whether the game finished or not.
        /// </summary>
        /// <param name="blackjackGame">The blackjack game.</param>
        /// <returns>
        /// 	<c>true</c> if game is finished; otherwise, <c>false</c>.
        /// </returns>
        internal virtual bool IsGameFinished(IBlackjackGame blackjackGame)
        {
            String errorMessage = GetErrorMessage("IsGameFinished");
            throw new NotSupportedException(errorMessage);
        }

        #endregion

        #region Heler Methods

        private String GetErrorMessage(String methodName)
        {
            const String errorMassageFormat = "Can't call '{0}' method in state - '{1}'.";
            String stateName = GetType().Name;
            String result = String.Format(errorMassageFormat, methodName, stateName);

            return result;
        }

        #endregion
    }
}
