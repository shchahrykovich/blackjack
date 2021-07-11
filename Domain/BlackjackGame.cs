using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blackjack.Domain.Game;
using Blackjack.Domain.Game.Cards;
using Blackjack.Domain.Game.States;
using Blackjack.Domain.Players;
using Blackjack.Domain.Players.Strategies;
using EnsureUtility;

namespace Blackjack.Domain
{
    /// <summary>
    /// Provides simple implementation of Blackjack game.
    /// See: http://en.wikipedia.org/wiki/Blackjack#How_to_play_blackjack
    /// </summary>
    public sealed class BlackjackGame : IBlackjackGame
    {
        #region Fields

        private readonly GameInfo _info;
        private GameState _state;

        #endregion

        #region Constructor

        public BlackjackGame(Player player)
            : this(new ComputerPlayer(new StandOn17Strategy()), new[] { player })
        {

        }

        internal BlackjackGame(Player dealer, IEnumerable<Player> players)
        {
            Ensure.NotNull(dealer, "dealer");
            Ensure.NotNull(players, "players");

            _info = new GameInfo(dealer, players, Card.GetNewPack());
            _state = new NonInitializedState(_info);
        }

        #endregion

        #region Public Methods

        public void Start()
        {
            _state = _state.Start(this);
        }

        public void MakeTurn(Player player)
        {
            Ensure.NotNull(player, "player");
            if(!_info.ContainsPlayer(player))
            {
                throw new ArgumentException("This player doesn't belong to this game");
            }

            _state = _state.MakeTurn(this, player);
        }

        public IEnumerable<Card> GetDealerHand()
        {
            return _state.GetDealerHand(this);
        }

        public BlackjackGameResult GetResult()
        {
            return _state.GetResult(this);
        }

        public int GetDealerScore()
        {
            return _state.GetDealerScore(this);
        }

        public void FinishGame()
        {
            _state = _state.FinishGame(this);
        }

        public bool IsGameFinished()
        {
            return _state.IsGameFinished(this);
        }

        #endregion
    }
}
