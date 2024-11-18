using System.Timers;


namespace EscapeRoom;

public class RoundManager
{
    public int _roundCounter { get; private set; }

    public RoundManager()
    {
        _roundCounter = 0;
    }
    
    public void IncrementRoundCounter()
    {
        _roundCounter++;
    }

    public void DisplayCurrentRound()
    {
        WriteLine($"Current round: {_roundCounter}");
    }

}