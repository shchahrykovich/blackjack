using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EnsureUtility;

namespace Blackjack.Domain.Game.Cards
{
    /// <summary>
    /// Container for cards.
    /// Not thread safe.
    /// </summary>
    internal sealed class CardPack: IEnumerable<Card>
    {
        private const int InitialCardPackLength = 52;

        private readonly List<Card> _cards;

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CardPack"/> class.
        /// </summary>
        /// <param name="cards">The cards.</param>
        internal CardPack(IEnumerable<Card> cards)
        {
            Ensure.NotNull(cards, "cards");
            
            //Simple validation check
            if(InitialCardPackLength != cards.Count())
            {
                throw new ArgumentException("The size of pack ins't valid.");
            }
            _cards = new List<Card>(cards);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the first card in the pack.
        /// </summary>
        /// <returns>Card.</returns>
        public Card GetCard()
        {
            Card result = _cards.First();

            const int first = 0;
            _cards.RemoveAt(first);

            return result;
        }

        public void Shuffle()
        {
            ShuffleInternal();
            ShuffleInternal();
            ShuffleInternal();
        }

        public int Count()
        {
            return _cards.Count;
        }

        public IEnumerator<Card> GetEnumerator()
        {
            return _cards.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _cards.GetEnumerator();
        }

        #endregion

        #region Helper Methods

        private void ShuffleInternal()
        {
            List<int> indexes = new List<int>(Enumerable.Range(0, _cards.Count));
            List<Card> newOrder = new List<Card>(_cards.Count);

            for (int i = 0; i < _cards.Count; i++)
            {
                Random random = new Random();
                int position = random.Next(indexes.Count);
                int realPosition = indexes[position];
                newOrder.Add(_cards[realPosition]);
                indexes.Remove(realPosition);
            }

            _cards.Clear();
            _cards.AddRange(newOrder);
        }

        #endregion
    }
}
