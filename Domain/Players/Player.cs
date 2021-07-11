using System;
using System.Collections.Generic;
using Blackjack.Domain.Game;
using Blackjack.Domain.Game.Cards;
using Blackjack.Domain.Players.Strategies;
using EnsureUtility;

namespace Blackjack.Domain.Players
{
    public abstract class Player
    {
        #region Fields

        private readonly List<Card> _hand = new List<Card>();
        
        protected BaseGameStrategy Strategy { get; private set; }

        #endregion

        #region Properties
        
        public virtual int Score
        {
            get
            {
                return ScoreCalculator.GetScore(_hand);
            }
        }

        public bool IsBlackjack
        {
            get
            {
                bool result = ScoreCalculator.IsBlackjack(_hand);
                return result;
            }
        }

        public bool IsBust
        {
            get 
            { 
                bool result = (ScoreCalculator.WiningScore < Score);
                return result;
            }
        }

        public virtual IEnumerable<Card> Hand
        {
            get
            {
                return _hand;
            }
        }

        public bool IsFinishedTurn { get; protected set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="strategy">The strategy.</param>
        protected Player(BaseGameStrategy strategy)
        {
            Strategy = Ensure<BaseGameStrategy>.NotNull(strategy, "strategy");
        }

        #endregion

        #region Helper Methods

        internal void TakeCard(Card card)
        {
            Ensure.NotNull(card, "card");

            _hand.Add(card);
        }

        /// <summary>
        /// Returns true when player needs one more card.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <returns></returns>
        internal virtual bool ShouldTakeCard(IBlackjackGame game)
        {
            Ensure.NotNull(game, "game");

            if (IsFinishedTurn)
            {
                throw new NotSupportedException("This player has already finished turn.");
            }

            IEnumerable<Card> dealerHand = game.GetDealerHand();
            bool result = Strategy.ShouldTakeCard(Score, Hand, dealerHand);
            IsFinishedTurn = !result;
            return result;
        }

        #endregion
    }
}
