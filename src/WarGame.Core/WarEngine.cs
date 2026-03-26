namespace WarGame.Core
{
    // Stores all player hands
    public class PlayerHands : Dictionary<string, Hand> { }

    // Stores the card each player plays
    public class PlayedCards : Dictionary<string, Card> { }

    // Holds all the round result info
    public class RoundResult
    {
        public int RoundNumber { get; set; }
        public PlayedCards Played { get; set; } = new PlayedCards();
        public bool Tied { get; set; }
        public List<string> TiedPlayers { get; set; } = new List<string>();
        public string Winner { get; set; } = "";
        public Dictionary<string, int> CardCounts { get; set; } = new Dictionary<string, int>();
    }

    // The main game engine
    public class WarEngine
    {
        public PlayerHands Hands = new PlayerHands();

        private const int RoundLimit = 10000;

        public event Action<RoundResult>? OnRoundComplete;

        public WarEngine(List<string> playerNames)
        {
            Deck deck = new Deck();

            // Give each player an empty hand
            foreach (string name in playerNames)
            {
                Hands[name] = new Hand();
            }

            // Deal one card at a time going around
            int i = 0;
            while (deck.Count > 0)
            {
                string playerName = playerNames[i % playerNames.Count];
                Hands[playerName].AddCard(deck.Deal());
                i++;
            }
        }

        // Runs the whole game
        public string Run()
        {
            for (int round = 1; round <= RoundLimit; round++)
            {
                // Find players who still have cards
                List<string> activePlayers = new List<string>();
                foreach (var entry in Hands)
                {
                    if (!entry.Value.IsEmpty())
                        activePlayers.Add(entry.Key);
                }

                if (activePlayers.Count == 1)
                    return activePlayers[0];

                if (activePlayers.Count == 0)
                    return "Draw";

                // Play a round and collect a pot
                List<Card> pot = new List<Card>();
                string winner = PlayRound(activePlayers, pot, round);

                // Winner gets all the cards in the pot
                if (winner != "")
                {
                    foreach (Card card in pot)
                        Hands[winner].AddCard(card);
                }
            }

            // Cr3eates a round limit
            string mostCards = "";
            int highest = -1;
            foreach (var entry in Hands)
            {
                if (entry.Value.Count > highest)
                {
                    highest = entry.Value.Count;
                    mostCards = entry.Key;
                }
            }

            return mostCards;
        }

        // Plays one round, handles ties, returns the winner
        private string PlayRound(List<string> players, List<Card> pot, int roundNumber)
        {
            PlayedCards played = new PlayedCards();

            // Everyone flips their top card
            foreach (string name in players)
            {
                Card card = Hands[name].DrawCard();
                played[name] = card;
                pot.Add(card);
            }

            // Find the highest rank played this round
            Rank highRank = Rank.Two;
            foreach (var entry in played)
            {
                if (entry.Value.Rank > highRank)
                    highRank = entry.Value.Rank;
            }

            // Find who tied for the highest rank
            List<string> winners = new List<string>();
            foreach (var entry in played)
            {
                if (entry.Value.Rank == highRank)
                    winners.Add(entry.Key);
            }

            // Build card count snapshot for reporting
            Dictionary<string, int> counts = new Dictionary<string, int>();
            foreach (var entry in Hands)
                counts[entry.Key] = entry.Value.Count;

            // Fire the event so the console can print what happened
            RoundResult result = new RoundResult();
            result.RoundNumber = roundNumber;
            result.Played = new PlayedCards();
            foreach (var entry in played)
                result.Played[entry.Key] = entry.Value;
            result.CardCounts = counts;

            if (winners.Count == 1)
            {
                result.Tied = false;
                result.Winner = winners[0];
                OnRoundComplete?.Invoke(result);
                return winners[0];
            }
            else
            {
                result.Tied = true;
                result.TiedPlayers = new List<string>(winners);
                result.Winner = "";
                OnRoundComplete?.Invoke(result);
            }

            // Tiebreaker only tied players who still have cards play again
            List<string> canPlay = new List<string>();
            foreach (string name in winners)
            {
                if (!Hands[name].IsEmpty())
                    canPlay.Add(name);
            }

            if (canPlay.Count == 0) return "";
            if (canPlay.Count == 1) return canPlay[0];

            return PlayRound(canPlay, pot, roundNumber);
        }
    }
}