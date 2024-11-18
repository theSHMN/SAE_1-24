namespace EscapeRoom.Actors;

public abstract class Actor
{
    public string Name { get; protected set; }
    public char Symbol { get; protected set; }
    public ConsoleColor Color { get; protected set; }
    public bool IsColliding { get; protected internal set; }
    public bool IsComplete { get; protected set; }
    
    protected Actor(string name, char symbol, ConsoleColor color)
    {
        Name = name;
        Symbol = symbol;
        Color = color;
        IsColliding = false;
        IsComplete = false;
    }

    public abstract void Behaviour();

    public virtual void DisplayInstruction(string text, ConsoleColor color) => WriteWithColor(text, color);
}