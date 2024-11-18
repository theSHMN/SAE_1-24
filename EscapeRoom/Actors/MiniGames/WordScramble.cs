namespace EscapeRoom.Actors.MiniGames;

public class WordScramble : Actor
{
    private List<string> _wordList;
    private string _currentWord;
    private string _scrambledWord;
    private int _remainingAttempts;
    
    public WordScramble(string name, char symbol, ConsoleColor color) : base(name, symbol, color)
    {
        _wordList = new List<string>() { "challenge", "glasses", "restaurant", "scissors", "highway", "safari" };
        _remainingAttempts = 10;
    }

    public override void Behaviour()
    {
        bool isCorrect = false;
        
        SelectWord();
        ScrambleWord();
        DisplayInstruction($"Guess the scrambled word! You have {_remainingAttempts}", ConsoleColor.Yellow);
        WriteLine();

        while (_remainingAttempts > 0 && !isCorrect)
        {
            WriteLine($"The scrambled word is: {_scrambledWord}"); 
            WriteLine($"Attempts: {_remainingAttempts} "); 
            WriteLine("Your guess: ");
            string guess = ReadLine();

            if (string.Equals(guess, _currentWord, StringComparison.OrdinalIgnoreCase))
            {
                HelperFunctions.WriteWithColor("Correct!", ConsoleColor.Green);
                isCorrect = true;
            }
            else
            {
                HelperFunctions.WriteWithColor("Incorrect!", ConsoleColor.Red);
                _remainingAttempts--;
            }
            Clear();
        }

        if (!isCorrect)
            WriteLine("You´ve lost!"); 
        else
            WriteLine("You´ve won!"); 
        IsComplete = true;
        
        
    }

    private void SelectWord()
    {
         _currentWord = HelperFunctions.GetRandomItem(_wordList);
    }

    private void ScrambleWord()
    {
        _scrambledWord = ScrambleString(_currentWord);
    }
    
    // Splits string into array of chars then use .OrderBy LINQ method 
    // to assign random modifier every time it is called to each char 
    // and shuffle the chars. Then concat´s it back to a string.
    private string ScrambleString(string word)
    {
           return new string(word.ToCharArray().OrderBy(c => Guid.NewGuid()).ToArray());
    }
}