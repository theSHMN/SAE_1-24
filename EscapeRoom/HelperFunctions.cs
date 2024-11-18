using EscapeRoom.Actors;
using EscapeRoom.Actors.Player;

namespace EscapeRoom;

public class HelperFunctions
{
    
    private static readonly Random _random = new();

    public static int GetRandom(int min = 0, int max = Int32.MaxValue)
    {
        return _random.Next(min,max);
    }
    
    public static T GetRandomItem<T>(List<T> list)
    {
        int index = _random.Next(list.Count);
        return list[index];
    }
 
    
    public static void DisplayRoomTile(char content, ConsoleColor contentColor, ConsoleColor backgroundColor)
    {
        WriteWithColor("[",backgroundColor );
        WriteWithColor($"{content}", contentColor);
        WriteWithColor("]",backgroundColor);
    }

    public static void WriteWithColor(string text, ConsoleColor color)
    {
        ForegroundColor = color;
        Write(text);
        ResetColor();
    }

    public static void PrintStatsOfActors(ActorManager actorManager)
    {
        foreach (var actor in actorManager.GetAllActors())
        {
            Grid.GridPosition? position = actorManager.GetActorPosition(actor);

            if (position != null)
            {
                WriteWithColor($"Actors Name: {actor.Name,-30} - Row/Col {position.row}/{position.col,-15} - IsColliding: {actor.IsColliding,-10} - IsComplete: {actor.IsComplete,-10}", actor.Color);
            }
            else
                WriteWithColor($"Actors Name: {actor.Name,-20} - Row/Col: 'N/A' - IsColliding: {actor.IsColliding,-15} - IsComplete: {actor.IsComplete,-10}", actor.Color);
            WriteLine();
        }

    }
    

    public static void PrintAllActors(ActorManager actorManager)
    {
        foreach (var actor in actorManager.GetAllActors())
        {
            var position = actorManager.GetActorPosition(actor);
            WriteLine($"{actor.Name} at position ({position?.row}, {position?.col})");
        }
    }
}