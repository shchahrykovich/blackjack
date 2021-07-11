using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blackjack.Domain;
using Blackjack.Domain.Game.Cards;
using Blackjack.Domain.Players;

namespace Blackjack.Web.Models
{
    public class BlackjackGameModel
    {
        #region Properties

        public bool ShowScore { get; set; }

        public String PlayerScore { get; set; }

        public IEnumerable<Card> PlayerHand { get; set; }

        public String DealerScore { get; set; }

        public IEnumerable<Card> DealerHand { get; set; }

        public bool ShowDealerBlankCard { get; set; }

        public BlackjackStatisticModel Statistic { get; set; }

        public bool ShowBanner { get; set; }

        #endregion

        public BlackjackGameModel(IBlackjackGame game, Player player, BlackjackStatisticModel statistic)
        {
            DealerHand = game.GetDealerHand();
            PlayerHand = player.Hand;
            PlayerScore = player.Score.ToString();
            if (game.IsGameFinished())
            {
                DealerScore = game.GetDealerScore().ToString();
            }
            else
            {
                ShowDealerBlankCard = true;
                DealerScore = "?";
            }

            Statistic = statistic;
        }
    }
}