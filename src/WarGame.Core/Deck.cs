namespace WarGame.Core
{
    // A standard deck of cards in a stack
    public class Deck
    {
        private Stack<Card> cards = new Stack<Card>();

        public int Count { get { return cards.Count; } }

        public Deck()
        {
            // Build all cards
            List<Card> tempList = new List<Card>();

            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    tempList.Add(new Card(suit, rank));
                }
            }

            // Shuffle the list randomly
            Random rng = new Random();
            for (int i = tempList.Count - 1; i > 0; i--)
            {
                int j = rng.Next(i + 1);
                Card temp = tempList[i];
                tempList[i] = tempList[j];
                tempList[j] = temp;
            }

            // Push shuffled cards onto the stack
            foreach (Card card in tempList)
            {
                cards.Push(card);
            }
        }

        // Take the top card
        public Card Deal()
        {
            return cards.Pop();
        }
    }
}