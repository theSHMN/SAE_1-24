using EscapeRoom.Actors;
using EscapeRoom.Actors.MiniGames;
using EscapeRoom.Actors.Player;

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

        Player player = new Player("Player",'X',ConsoleColor.Red);
        Hangman hangman = new Hangman("Hangman Game", 'H', ConsoleColor.Magenta);
        NumberGuessingGame numberGuessingGame = new NumberGuessingGame("Number Guessing Game", 'N', ConsoleColor.Cyan);
        RockPaperScissors rockPaperScissors = new RockPaperScissors("Rock Paper Scissors Game", 'R', ConsoleColor.Yellow);
        WordScramble wordScramble = new WordScramble("Word Scramble", 'W', ConsoleColor.Blue);
        
        actorManager.AddActor(player);
        actorManager.AddActor(hangman);
        actorManager.AddActor(numberGuessingGame);
        actorManager.AddActor(rockPaperScissors);
        actorManager.AddActor(wordScramble);
        

        while (true)
        {
            //grid.DisplayGrid(actorManager.GetActorPositions());
            //HelperFunctions.PrintStatsOfActors(actorManager);
            Input.DirectionalInput(player,actorManager,grid);
            
        }
    }
}
