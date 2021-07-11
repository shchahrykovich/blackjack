using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blackjack.Domain.Game.Cards;
using Blackjack.Domain.Players;
using EnsureUtility;

namespace Blackjack.Domain
{
    public class BlackjackGameResult
    {
        private readonly Player _dealer;
        private readonly IEnumerable<Player> _players;
        private readonly IEnumerable<Player> _allPlayers;

        public readonly DateTime CreateDate;
        public readonly String GameId;

        private bool _isDraw;

        internal BlackjackGameResult(Player dealer, IEnumerable<Player> players)
        {
            _dealer = Ensure<Player>.NotNull(dealer, "dealer");
            Ensure.NotNull(players, "players");

            if(0 == players.Count())
            {
                throw new ArgumentException("List of players is empty");
            }
            _players = players;

            List<Player> allPlayers = new List<Player>(_players);
            allPlayers.Add(_dealer);
            _allPlayers = allPlayers;

            CreateDate = DateTime.UtcNow;
            GameId = Guid.NewGuid().ToString("N").ToUpper();
        }

        #region Public Methods

        public bool IsDraw()
        {
            return _isDraw;
        }

        public IEnumerable<Player> GetWinner()
        {
            IEnumerable<Player> winners;

            if (_dealer.IsBust)
            {
                winners = _players.Where(p => !p.IsBust).ToArray();
                if (0 == winners.Count())
                {
                    return new[] {_dealer};
                }
                else
                {
                    return winners;
                }
            }

            winners = _allPlayers.Where(p => p.IsBlackjack).ToArray();
            if (0 < winners.Count())
            {
                if(winners.Contains(_dealer) && 1< winners.Count())
                {
                    _isDraw = true;
                }
                return winners;
            }

            winners = _players.Where(p => !p.IsBust && _dealer.Score == p.Score).ToArray();
            if (0 < winners.Count())
            {
                _isDraw = true;
                List<Player> result = new List<Player>(winners);
                result.Add(_dealer);
                return result;
            }

            winners = _players.Where(p => !p.IsBust && _dealer.Score < p.Score).ToArray();
            if (0 < winners.Count())
            {
                return winners;
            }
            else
            {
                return new[] {_dealer};
            }
        }

        public IEnumerable<Player> GetAllPlayers()
        {
            return _allPlayers;
        }

        #endregion
    }
}
