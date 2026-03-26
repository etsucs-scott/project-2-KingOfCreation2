using WarGame.Core;

// Check for command line argument first
int playerCount = 0;

if (args.Length > 0)
{
    int.TryParse(args[0], out playerCount);
}

// If no valid argument was given
while (playerCount < 2 || playerCount > 4)
{
    Console.Write("Enter number of players (2-4): ");
    int.TryParse(Console.ReadLine(), out playerCount);
}

// Cfreats players names
List<string> names = new List<string>();
for (int i = 1; i <= playerCount; i++)
{
    names.Add("Player " + i);
}

Console.WriteLine("\nStarting War with " + playerCount + " players!\n");

// Set up the game engine
WarEngine engine = new WarEngine(names);

// Print results after each round
engine.OnRoundComplete += result =>
{
    Console.WriteLine("Round " + result.RoundNumber);

    // Print each player's card
    foreach (var entry in result.Played)
        Console.WriteLine("  " + entry.Key + ": " + entry.Value);

    // If tie, print tie info
    if (result.Tied)
    {
        Console.WriteLine("  Tie between " + string.Join(" and ", result.TiedPlayers) + "!");

        // Show what is in the pot
        string potCards = "";
        foreach (var entry in result.Played)
            potCards += entry.Value + ", ";
        Console.WriteLine("  Pot includes: " + potCards.TrimEnd(',', ' '));
    }

    // Print the winner and card counts
    if (result.Winner != "")
    {
        string counts = "";
        foreach (var entry in result.CardCounts)
            counts += entry.Key + "=" + entry.Value + ", ";

        Console.WriteLine("  Winner: " + result.Winner + " (Cards: " + counts.TrimEnd(',', ' ') + ")");
    }

    Console.WriteLine();

};

// Run the game
string winner = engine.Run();

Console.WriteLine("MEOWMEOWMEOWMEOWMEOWMEOWMEOW");
Console.WriteLine("  GAME OVER - Winner: " + winner);
Console.WriteLine("MEOWMEOWMEOWMEOWMEOWMEOWMEOW");