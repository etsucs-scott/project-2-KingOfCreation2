War Card Game

What is this?
This is a console game based off the card game war. It can supoport 2-4 players and runs just as 
it would playing it in real life. Keeps track of cards and decks.

How to Build and Run

dotnet build
dotnet run --project src/WarGame.Console

It will ask you how many players you want (2 - 4), then the game runs on its own.

How Player Count Works
It checks for a number 2 - 4 after it prompts you and then uses that to determin how many plers to 
put in and how to divide the cards up. If a number outside of that is added it doesnt work.

Project Structure
WarGame.Core: All the game logic (cards, deck, hands, engine)
WarGame.Console: Just handles printing to the screen and getting player count from the user.

Rules
- Standard 52 card deck, Ace is highest
- Each round every player flips their top card
- Highest card wins and takes everyones cards
- If there is a tie, the tied players each play one more card
- If a player runs out of cards they are eliminated
- Game ends when one player has all the cards, or after 10,000 rounds (most cards wins)

Good luck and hopefully it doesnt break