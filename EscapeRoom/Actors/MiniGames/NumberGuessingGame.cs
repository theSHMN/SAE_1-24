namespace EscapeRoom.Actors.MiniGames;

public class NumberGuessingGame : Actor
{

    private int _lowerRandomnessBound;
    private int _upperRandomnessBound; 
    private int _targetNumber;
    private int guess;
    private int _remainingAttemptls;


    public NumberGuessingGame(string name, char symbol, ConsoleColor color) : base(name, symbol, color)
    {
        _lowerRandomnessBound = 1;
        _upperRandomnessBound = 10;
        _targetNumber = HelperFunctions.GetRandom(_lowerRandomnessBound, _upperRandomnessBound);
        _remainingAttemptls = 10;
    }

    public override void Behaviour()
    {
        do
        {
            DisplayInstruction("Guess the number correctly.", ConsoleColor.Yellow);
            WriteLine();
            guess = Input.ParseToInteger("Your guess: ", _lowerRandomnessBound, _upperRandomnessBound);
            if(guess < _targetNumber)
                WriteLine("To low...");
            else if (guess > _targetNumber)
                WriteLine("A little higher...");
            else
                WriteLine("Correct!");
            
            WriteLine("Press ENTER to proceed");
            ReadLine();
        } while (guess != _targetNumber);
        //ENDGAME();
    }
}