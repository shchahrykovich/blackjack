using System;
using EnsureUtility;

namespace Blackjack.Web.Models
{
    public class BlackjackGameRoundModel
    {
        public int PlayerScore { get; private set; }

        public int DealerScore { get; private set; }

        public String Result { get; private set; }

        public BlackjackGameRoundModel(int playerScore, int dealerScore, String result)
        {
            Ensure.Positive(playerScore, "playerScore");
            Ensure.Positive(dealerScore, "dealerScore");
            Ensure.StringNotEmpty(result, "result");

            PlayerScore = playerScore;
            DealerScore = dealerScore;
            Result = result;
        }
    }
}