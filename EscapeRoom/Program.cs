using EscapeRoom.Actors;
using EscapeRoom.Actors.MiniGames;
using EscapeRoom.Actors.Player;
using EscapeRoom.Actors.Trap;

namespace EscapeRoom;

class Program
{
    static void Main(string[] args)
    {
        Clear();
        int maxRow = 10;
        int maxCol = 10;

        Grid grid = new Grid(maxRow, maxCol);
        ActorManager actorManager = new ActorManager(grid);

        GridManager gridManager = new GridManager(actorManager, grid);


        Player player = new Player("Player", 'X', ConsoleColor.Red);
        actorManager.AddActor(player);

        Hangman hangman = new Hangman("Hangman Game", 'H', ConsoleColor.Cyan);
        NumberGuessingGame numberGuessingGame = new NumberGuessingGame("Number Guessing Game", 'N', ConsoleColor.Cyan);
        RockPaperScissors rockPaperScissors = new RockPaperScissors("Rock Paper Scissors Game", 'R', ConsoleColor.Cyan);
        WordScramble wordScramble = new WordScramble("Word Scramble", 'W', ConsoleColor.Cyan);
        Pitfall pitfall = new Pitfall("Pitfall", '§', ConsoleColor.Magenta);

        actorManager.AddActor(hangman);
        actorManager.AddActor(numberGuessingGame);
        actorManager.AddActor(rockPaperScissors);
        actorManager.AddActor(wordScramble);
        actorManager.AddActor(pitfall);

        Grid.GridPosition
            targetPosition =
                actorManager.GetActorPosition(player) ?? new Grid.GridPosition(0, 0); // Null-coalescing operator

        while (true)
        {
            HelperFunctions.PrintStatsOfActors(actorManager);
            grid.DisplayGrid(actorManager.GetActorPositions(), targetPosition);
            bool moveConfirmed = Input.DirectionalInput(player, actorManager, grid, ref targetPosition);
            WriteLine();
            Clear();


            if (moveConfirmed)
            {
                actorManager.UpdateActorPosition(player, targetPosition);
                actorManager.MoveActorRandomly("Pitfall",3);
            }

            if (actorManager.GetActorPositions().TryGetValue(targetPosition, out Actor targetActor))
            {
                if (targetActor != player)
                {
                    HelperFunctions.WriteWithColor($"Player actor is near {targetActor.Name}", ConsoleColor.Cyan);
                    WriteLine();
                }
            }

            gridManager.CheckAdjacency();
        }
    }
}