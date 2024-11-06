namespace EscapeRoom.Actors;

public abstract class Actor
{
    public string Name { get; protected set; }
    public char Symbol { get; protected set; }
    
    public ConsoleColor Color { get; protected set; }
    public bool IsCollision { get; protected internal set; }
    public bool IsComplete { get; protected set; }
    

    protected Actor(string name, char symbol, ConsoleColor color)
    {
        Name = name;
        Symbol = symbol;
        Color = color;
        IsCollision = false;
        IsComplete = false;
        
    }
    

    public abstract void Behaviour();

    public virtual void DisplayInstruction(string text, ConsoleColor color) => HelperFunctions.WriteWithColor(text, color);

    public virtual void StartInteraction()
    {
        WriteLine($"{Name} is near by. Start interaction? (Y/N)");
        string input = ReadLine();
        
        if (input?.Trim().ToLower() == "y") 
            EndInteraction(true);
        else
            EndInteraction(false);
    }

    public virtual void EndInteraction(bool isSuccessful)
    {
        if (isSuccessful)
        {
            IsComplete = true;
            WriteLine("Interaction successful!");
        }
        else
        {
            IsComplete = false;
            WriteLine("Interaction successful!");  
        }
    }
    
    
    
}