namespace EscapeRoom.Actors.Player;

public class Door : Actor
{
    public Door(string name, char symbol, ConsoleColor color) : base(name, symbol, color)
    {
        
    }

    public override void Behaviour()
    {
        Clear();
        WriteLine("GAME WON!!!");
    }
}