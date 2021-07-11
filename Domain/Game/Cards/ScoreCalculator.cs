using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnsureUtility;

namespace Blackjack.Domain.Game.Cards
{
    /// <summary>
    /// Scores cards.
    /// </summary>
    internal static class ScoreCalculator
    {
        #region Fields

        private const int AceMaxScore = 11;
        private const int AceMinScore = 1;
        private const int FaceCardScore = 10;
        internal const int WiningScore = 21;

        #endregion

        public static int GetScore(IEnumerable<Card> hand)
        {
            Ensure.NotNull(hand, "hand");

            int total = 0;
            if (null != hand)
            {
                foreach (Card card in hand.OrderByDescending(c => c.Type))
                {
                    int score = GetScore(card, total);
                    total += score;
                }
            }

            return total;
        }

        public static bool IsBlackjack(IEnumerable<Card> hand)
        {
            Ensure.NotNull(hand, "hand");

            bool result = false;

            const int expectedCount = 2;
            int count = hand.Count();
            if(expectedCount == count)
            {
                Card ace = hand.Where(c => c.Type == CardType.Ace).FirstOrDefault();
                if(null != ace)
                {
                    Card picture = hand.Where(c => IsPicture(c.Type)).FirstOrDefault();
                    result = (null != picture);
                }
            }

            return result;
        }

        #region Helper Methods

        private static bool IsPicture(CardType type)
        {
            bool result;
            switch (type)
            {
                case CardType.Ace:
                case CardType.Two:
                case CardType.Three:
                case CardType.Four:
                case CardType.Five:
                case CardType.Six:
                case CardType.Seven:
                case CardType.Eight:
                case CardType.Nine:
                    result = false;
                    break;
                case CardType.Ten:
                case CardType.Jack:
                case CardType.Queen:
                case CardType.King:
                    result = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("type");
            }

            return result;
        }

        private static int GetScore(Card card, int totalScore)
        {
            int result;
            switch (card.Type)
            {
                case CardType.Ace:
                    result = GetAceScore(card, totalScore);
                    break;
                case CardType.Two:
                case CardType.Three:
                case CardType.Four:
                case CardType.Five:
                case CardType.Six:
                case CardType.Seven:
                case CardType.Eight:
                case CardType.Nine:
                case CardType.Ten:
                    result = (int)card.Type;
                    break;
                case CardType.Jack:
                case CardType.Queen:
                case CardType.King:
                    result = FaceCardScore;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return result;
        }

        private static int GetAceScore(Card card, int totalScore)
        {
            if (CardType.Ace != card.Type)
            {
                throw new NotSupportedException("Can't calculate score");
            }

            int result;

            int expected = totalScore + AceMaxScore;
            if (expected <= WiningScore)
            {
                result = AceMaxScore;
            }
            else
            {
                result = AceMinScore;
            }

            return result;
        }        

        #endregion
    }
}
