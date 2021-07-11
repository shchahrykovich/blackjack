using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blackjack.Domain.Game.Cards;
using Blackjack.Web.Infrastructure;
using EnsureUtility;

namespace Blackjack.Web.Models
{
    public class BlackjackGameJson
    {
        public String PlayerScore { get; private set; }

        public IEnumerable<String> PlayerHand { get; private set; }

        public String DealerScore { get; private set; }

        public IEnumerable<String> DealerHand { get; private set; }

        public bool ShowDealerBlankCard { get; private set; }

        public String WinsCounter { get; private set; }

        public String DrawsCounter { get; private set; }

        public String LoosesCounter { get; private set; }

        public IEnumerable<String> StatisticTable { get; private set; }

        public BlackjackGameJson(BlackjackGameModel model)
        {
            Ensure.NotNull(model, "model");

            PlayerScore = model.PlayerScore;
            DealerScore = model.DealerScore;
            ShowDealerBlankCard = model.ShowDealerBlankCard;
            PlayerHand = PrepareHand(model.PlayerHand);
            DealerHand = PrepareHand(model.DealerHand);

            WinsCounter = GetCounter(model.Statistic.WinsCount, model.Statistic.WinsPercent);
            DrawsCounter = GetCounter(model.Statistic.DrawsCount, model.Statistic.DrawsPercent);
            LoosesCounter = GetCounter(model.Statistic.LoosesCount, model.Statistic.LoosesPercent);
            if(!model.ShowDealerBlankCard)
            {
                StatisticTable = GetStatisticTable(model);
            }
        }

        private IEnumerable<string> GetStatisticTable(BlackjackGameModel model)
        {
            List<String> result = null;
            if (null != model.Statistic.Rounds && 0 < model.Statistic.Rounds.Count())
            {
                BlackjackGameRoundModel last = model.Statistic.Rounds.Last();
                result = new List<string>();
                result.Add(model.Statistic.Rounds.Count().ToString());
                result.Add(last.PlayerScore.ToString());
                result.Add(last.DealerScore.ToString());
                result.Add(last.Result);
            }

            return result;
        }

        private String GetCounter(int count, string percent)
        {
            String result = count + " (" + percent + ")";
            return result;
        }

        private IEnumerable<String> PrepareHand(IEnumerable<Card> hand)
        {
            List<String> result = new List<String>();
            foreach (Card card in hand)
            {
                String item = HtmlHelpers.Card(null, card).ToHtmlString();
                result.Add(item);
            }

            return result;
        }
    }
}