using System;
using System.Collections.Generic;
using System.Globalization;
using Blackjack.Web.Resources;

namespace Blackjack.Web.Models
{
    public class BlackjackStatisticModel
    {
        private readonly List<BlackjackGameRoundModel> _rounds = new List<BlackjackGameRoundModel>();

        #region Properties
        
        public int WinsCount { get; private set; }

        public String WinsPercent
        {
            get { return GetPercents(WinsCount); }
        }

        public int DrawsCount { get; private set; }

        public String DrawsPercent
        {
            get { return GetPercents(DrawsCount); }
        }

        public int LoosesCount { get; private set; }

        public String LoosesPercent
        {
            get { return GetPercents(LoosesCount); }
        }

        public IEnumerable<BlackjackGameRoundModel> Rounds
        {
            get { return _rounds; }
        }

        #endregion

        #region Public Methods
    
        public void AddDrawRound(int playerScore, int dealerScore)
        {
            if(playerScore != dealerScore)
            {
                throw new ArgumentException("playerScore can not be equal to dealer score.");
            }

            DrawsCount++;
            String result = Common.Draw;
            AddRoundResult(playerScore, dealerScore, result);
        }

        public void AddPlayerRound(int playerScore, int dealerScore)
        {
            WinsCount++;
            String result = Common.PlayerWin;
            AddRoundResult(playerScore, dealerScore, result);
        }

        public void AddDealerRound(int playerScore, int dealerScore)
        {
            LoosesCount++;
            String result = Common.DelaerWin;
            AddRoundResult(playerScore, dealerScore, result);
        }

        #endregion

        #region Helper Methods

        private void AddRoundResult(int playerScore, int dealerScore, string result)
        {
            BlackjackGameRoundModel model = new BlackjackGameRoundModel(playerScore, dealerScore, result);
            _rounds.Add(model);
        }

        private String GetPercents(int number)
        {
            String result = "0 %";
            float total = WinsCount + DrawsCount + LoosesCount;
            if (0 != total)
            {
                float percents = (number/total)*100;
                result = percents.ToString("0.", new CultureInfo("en-us", true)) + " %";
            }
            return result;
        } 

        #endregion
    }
}