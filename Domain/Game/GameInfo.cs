using System;
using System.Collections.Generic;
using System.Linq;
using Blackjack.Domain.Game.Cards;
using Blackjack.Domain.Players;
using EnsureUtility;

namespace Blackjack.Domain.Game
{
    /// <summary>
    /// Helper class that stores game information.
    /// </summary>
    internal sealed class GameInfo
    {
        #region Fields

        public readonly CardPack Pack;
        public IEnumerable<Player> Players { get; private set; }
        public Player Dealer { get; private set; }

        #endregion

        #region Constructor

        internal GameInfo(Player dealer, IEnumerable<Player> players, CardPack pack)
        {
            Dealer = Ensure<Player>.NotNull(dealer, "dealer");
            Players = Ensure<IEnumerable<Player>>.NotNull(players, "players");
            Pack = Ensure<CardPack>.NotNull(pack, "pack");
        }

        #endregion

        #region Helper Methods

        internal IEnumerable<Player>  GetAllPlayers()
        {
            List<Player> players = new List<Player>(Players);
            players.Add(Dealer);

            return players;
        }

        internal bool ContainsPlayer(Player player)
        {
            Ensure.NotNull(player, "player");

            IEnumerable<Player> players = GetAllPlayers().Where(p => p == player);
            const int expectedAmount = 1;
            bool isValid = (expectedAmount == players.Count());
            return isValid;
        }

        #endregion
    }
}
