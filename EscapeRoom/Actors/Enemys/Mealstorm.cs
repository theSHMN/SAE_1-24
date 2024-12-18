namespace EscapeRoom.Actors.Trap;

public class Mealstorm : Actor
{

    private ActorManager _actorManager;
    public Mealstorm(string name, char symbol, ConsoleColor color, ActorManager actorManager) : base(name, symbol, color)
    {
        _actorManager = actorManager;
    }
    
    public override void Behaviour()
    {
        Actor player = _actorManager.GetActorByName("Classes");

        if (player == null)
        {
            WriteLine("Classes not found!");
            return;
        }

        Grid.GridPosition? currentPlayerPosition = _actorManager.GetActorPosition(player);

        if (currentPlayerPosition == null)
        {
            WriteLine("Classes position not found!");
            return;
        }
        _actorManager.MoveActorRandomly(player.Name,10);
        WriteLine($"{player.Name} has been moved by Maelstorm!");
        

    }
}