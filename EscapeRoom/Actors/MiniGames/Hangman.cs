namespace EscapeRoom.Actors.MiniGames;

public class Hangman : Actor
{

    private string[] _guessWordList;
    private readonly string _guessWord;
    private readonly HashSet<char> _correctlyGuessedChars;
    private readonly HashSet<char> _wronglyGuessedChars;
    private int _remainingAttempts;
    
    public Hangman(string name, char symbol, ConsoleColor color) : base(name, symbol, color)
    {
        _guessWordList = ["challenge", "glasses", "restaurant", "scissors", "highway", "safari"];
        _guessWord = GetRandomWord(_guessWordList);
        _correctlyGuessedChars = new HashSet<char>();
        _wronglyGuessedChars = new HashSet<char>();
        _remainingAttempts = 10;
    }

    public override void Behaviour()
    {
        DisplayInstruction($"Guess the letters till the words forms. You have {_remainingAttempts} attempts.",
            ConsoleColor.Yellow);
        WriteLine();
        
        // .ToHashSet converts string _guessWord into Hashset of char and 
        // eliminates all duplicates.
        // .SetEqual compares the contents of _guessWord and __correctlyGuessed
        // and returns a bool
        while (_remainingAttempts > 0 && !_correctlyGuessedChars.SetEquals(_guessWord.ToHashSet()))
        {
            DisplayCurrentGameState();
            WriteLine();
            char guess = ReadLine()?.ToLower()[0] ?? ' '; // ?? ' ' defines a fallback value.

            if (_guessWord.Contains(guess))
            {
                    _correctlyGuessedChars.Add(guess);
                    WriteLine($"Correct guess!!! '{guess}' is a letter in the word.");
            }
            else if (!_guessWord.Contains(guess))
            {
                _wronglyGuessedChars.Add(guess);
                _remainingAttempts--;
                WriteLine($"Guess incorrect. '{guess}' is not a letter in the word. Remaining attempts: {_remainingAttempts}");
            }
            else
            {
                WriteLine($"You´ve already guessed the letter '{guess}'.");
            }
        }

        IsComplete = true;
    }


    private string GetRandomWord(string[] guessWordList)
    {
        return GetRandomItem(guessWordList.ToList());
    }

    private void DisplayCurrentGameState()
    {
        WriteLine("Word to guess: ");
        foreach (char c in _guessWord)
        {
            if (_correctlyGuessedChars.Contains(c))
                Write(c + " ");
            else
                Write("_");
        }
        WriteLine();
        WriteLine("Wrong guesses: " + string.Join(",", _wronglyGuessedChars));
        
    }
}