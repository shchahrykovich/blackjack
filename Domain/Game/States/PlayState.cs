using System;
using System.Collections.Generic;
using System.Linq;
using Blackjack.Domain.Game.Cards;
using Blackjack.Domain.Players;
using EnsureUtility;

namespace Blackjack.Domain.Game.States
{
    internal sealed class PlayState: GameState
    {
        internal PlayState(GameState state)
            : base(state)
        {
        }

        internal override GameState MakeTurn(IBlackjackGame game, Player player)
        {
            Ensure.NotNull(game, "game");
            Ensure.NotNull(player, "player");

            bool isValid = Info.ContainsPlayer(player);
            if (!isValid)
            {
                throw new ArgumentException("This player doesn't belong to this game");
            }

            if(player.IsFinishedTurn)
            {
                throw new NotSupportedException("Player has already finished turn");
            }

            if (ShoudlTakeCard(game, player))
            {
                TakeCard(player);
            }

            GameState result = this;
            if (IsGameFinished())
            {
                result = new FinishedState(this);    
            }
            return result;
        }

        internal override GameState FinishGame(IBlackjackGame game)
        {
            foreach (Player player in Info.Players)
            {
                while (!player.IsFinishedTurn && ShoudlTakeCard(game, player))
                {
                    TakeCard(player);
                }
            }

            while (!Info.Dealer.IsFinishedTurn && ShoudlTakeCard(game, Info.Dealer))
            {
                TakeCard(Info.Dealer);
            }

            return new FinishedState(this);
        }

        internal override IEnumerable<Card> GetDealerHand(IBlackjackGame game)
        {
            //During the game show only first card.
            return new[] {Info.Dealer.Hand.First()};
        }

        internal override bool IsGameFinished(Blackjack.Domain.IBlackjackGame blackjackGame)
        {
            return false;
        }

        #region Helper Methods

        private void TakeCard(Player player)
        {
            Card card = Info.Pack.GetCard();
            player.TakeCard(card);
        }

        private bool ShoudlTakeCard(IBlackjackGame game, Player player)
        {
            bool result = (player.ShouldTakeCard(game) && player.Score <= ScoreCalculator.WiningScore);
            return result;
        }

        private bool IsGameFinished()
        {
            int count = Info.GetAllPlayers().Where(p => !p.IsFinishedTurn).Count();
            const int expectedCount = 0;
            bool result = (expectedCount == count);
            return result;
        }

        #endregion
    }
}
