namespace EscapeRoom.Actors.MiniGames;

public class RockPaperScissors : Actor
{
    public enum Hand
    {
        Rock,
        Paper,
        Scissors,
    }

    private const int WinnigScore = 5;
    private int _playerWins;
    private int _computerWins;
    private int _draw;

    public record EnumBounds(int lowerBounds, int upperBound);

    public RockPaperScissors(string name, char symbol, ConsoleColor color) :
        base(name, symbol, color)
    {
        _playerWins = 0;
        _computerWins = 0;
        _draw = 0;
    }

    public override void Behaviour()
    {
        DisplayInstruction("Rock >> Paper >> Scissors >> Rock / [5 wins]", ConsoleColor.Yellow);
        WriteLine();

        while (_playerWins < WinnigScore && _computerWins < WinnigScore)
        {
            Clear();
            ShowStat();

            Hand playerChoice = PlayerChoice();
            Hand computerChoice = ComputerChoice();

            WriteLine($"Misc:     {playerChoice}");
            WriteLine($"Computer:   {computerChoice}");
            WriteLine("Press Enter...");
            ReadLine();

            string result = DetermineWinner(playerChoice, computerChoice);
            WriteLine(result);

            if (_playerWins == WinnigScore)
            {
                Clear();
                HelperFunctions.WriteWithColor($"Misc wins!", ConsoleColor.Green);
                WriteLine();
                ShowStat();
            }
            else if (_computerWins == WinnigScore)
            {
                Clear();
                HelperFunctions.WriteWithColor($"Computer wins!", ConsoleColor.Red);
                WriteLine();
                ShowStat();
            }
        }
        IsComplete = true;
    }

    private Hand PlayerChoice()
    {
        EnumBounds bounds = GetEnumBounds<Hand>();

        IEnumerable<Hand> handChoices = Enum.GetValues(typeof(Hand)).Cast<Hand>();
        string menu = string.Join(" / ", handChoices.Select((hand, index) => $"[{index + 1}] = {hand}"));
        
        // Do I want to change the Menu
        int choice =
            Input.ParseToInteger(menu, bounds.lowerBounds + 1, bounds.upperBound + 1);
        return ChooseHand(choice);
    
    }

    private Hand ComputerChoice()
    {
        EnumBounds bounds = GetEnumBounds<Hand>();
        int choice = HelperFunctions.GetRandom(bounds.lowerBounds + 1, bounds.upperBound + 1);
        return ChooseHand(choice);
    }


    private string DetermineWinner(Hand playerChoice, Hand computerChoice)
    {
        if (playerChoice == computerChoice)
        {
            _draw++;
            return "Draw!";
        }
        
        switch (playerChoice, computerChoice)
        {
            case (Hand.Rock, Hand.Scissors):
            case (Hand.Scissors, Hand.Paper):
            case (Hand.Paper, Hand.Rock):
                _playerWins++;
                return $"Misc wins with {playerChoice}";

            default:
                _computerWins++;
                return $"Computer wins with {computerChoice}";
        }
    }

    private Hand ChooseHand(int choice)
    {
        return (Hand)(choice - 1);
    }

    private void ShowStat()
    {
        HelperFunctions.WriteWithColor($"Misc:     {_playerWins}", ConsoleColor.Green);
        WriteLine();
        HelperFunctions.WriteWithColor($"Computer:   {_computerWins}", ConsoleColor.Red);
        WriteLine();
        HelperFunctions.WriteWithColor($"Draws:      {_draw}", ConsoleColor.DarkCyan);
        WriteLine();
    }

    private EnumBounds GetEnumBounds<T>() where T : Enum
    {
        IEnumerable<int> values = Enum.GetValues(typeof(T)).Cast<int>();
        return new EnumBounds(values.Min(), values.Max());
    }
}