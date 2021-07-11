using Blackjack.Domain.Game.Cards;
using Blackjack.Domain.Players;
using EnsureUtility;

namespace Blackjack.Domain.Game.States
{
    /// <summary>
    /// The first state after the game was created.
    /// </summary>
    internal class NonInitializedState: GameState
    {
        #region Constructors

        internal NonInitializedState(GameInfo info)
            : base(info)
        {
        }

        protected NonInitializedState(GameState state)
            : base(state)
        {

        } 

        #endregion

        /// <summary>
        /// Starts the game.
        /// </summary>
        internal override GameState Start(IBlackjackGame game)
        {
            Ensure.NotNull(game, "game");

            Info.Pack.Shuffle();
            foreach (Player player in Info.GetAllPlayers())
            {
                Card card = Info.Pack.GetCard();
                player.TakeCard(card);
                
                card = Info.Pack.GetCard();
                player.TakeCard(card);
            }
            return new PlayState(this);
        }
    }
}
