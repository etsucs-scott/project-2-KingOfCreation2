namespace WarGame.Core
{
    // Each player's hand is a queue,draw from front, add to back
    public class Hand
    {
        private Queue<Card> cards = new Queue<Card>();

        public int Count { get { return cards.Count; } }

        public void AddCard(Card card)
        {
            cards.Enqueue(card);
        }

        public Card DrawCard()
        {
            return cards.Dequeue();
        }

        public bool IsEmpty()
        {
            return cards.Count == 0;
        }
    }
}