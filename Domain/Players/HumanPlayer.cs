using System;
using Blackjack.Domain.Game.Cards;
using Blackjack.Domain.Players.Strategies;

namespace Blackjack.Domain.Players
{
    public class HumanPlayer: Player
    {
        private bool _shouldTakeCardForNextTurn = false;

        public HumanPlayer() : base(new ManualStrategy())
        {
        }

        #region Public Methods

        public void Hit()
        {
            _shouldTakeCardForNextTurn = true;
        }

        public void Stand()
        {
            _shouldTakeCardForNextTurn = false;
        }

        #endregion

        #region Helper Methods

        internal override bool ShouldTakeCard(IBlackjackGame game)
        {
            if (IsFinishedTurn)
            {
                throw new NotSupportedException("This player has already finished turn.");
            }

            if(ScoreCalculator.WiningScore < Score)
            {
                IsFinishedTurn = true;
                _shouldTakeCardForNextTurn = false;
            }
            else
            {
                IsFinishedTurn = !_shouldTakeCardForNextTurn;
            }
            return _shouldTakeCardForNextTurn;
        }

        #endregion
    }
}
