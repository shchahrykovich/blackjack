namespace Blackjack.Domain.Game.Cards
{
    [System.Diagnostics.DebuggerDisplay("{Suit}; {Type}")]
    public class Card
    {
        #region Properties

        public CardSuit Suit { get; private set; }

        public CardType Type { get; private set; }

        #endregion

        #region Constructors

        private Card(CardSuit suit, CardType type)
        {
            Suit = suit;
            Type = type;
        }

        #endregion

        #region Public Methods

        internal static CardPack GetNewPack()
        {
            const int suitNumber = 4;
            const int suitSize = 13;

            Card[] cards = new Card[suitNumber * suitSize];
            for (int suiteIndex = 0; suiteIndex < suitNumber; suiteIndex++)
            {
                for (int cardIndex = 0; cardIndex < suitSize; cardIndex++)
                {
                    Card card = new Card((CardSuit)suiteIndex + 1, (CardType)cardIndex + 1);
                    int currentIndex = suiteIndex * suitSize + cardIndex;
                    cards[currentIndex] = card;
                }
            }

            CardPack pack = new CardPack(cards);
            return pack;
        }

        #endregion
    }
}
