namespace WarGame.Core
{
    // The four suits 
    public enum Suit { Hearts, Diamonds, Clubs, Spades }

    // Card ranks from 2 (lowest) to Ace (highest)
    public enum Rank { Two = 2, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace }

    // CARD CLASS
    public class Card
    {
        public Suit Suit { get; set; }
        public Rank Rank { get; set; }

        public Card(Suit suit, Rank rank)
        {
            Suit = suit;
            Rank = rank;
        }

        // Shows only the rank when printing
        public override string ToString()
        {
            if (Rank == Rank.Jack) return "J";
            if (Rank == Rank.Queen) return "Q";
            if (Rank == Rank.King) return "K";
            if (Rank == Rank.Ace) return "A";
            return ((int)Rank).ToString();
        }
    }
}