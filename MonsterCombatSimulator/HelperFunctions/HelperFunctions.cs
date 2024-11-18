using DefaultNamespace;

namespace MonsterCombatSimulator;

public class HelperFunctions
{
    private static readonly Random _random = new();

    public static void WriteWithColor(string text, ConsoleColor color, bool isWriteLine = false)
    {
        ForegroundColor = color;
        if (isWriteLine)
            WriteLine(text);
        else
            Write(text);
        ResetColor();
    }

    public static int GetRandom(int min = Int32.MinValue, int max = Int32.MaxValue)
    {
        return _random.Next(min, max);
    }

    public static T GetRandomItem<T>(List<T> list)
    {
        int index = _random.Next(list.Count);
        return list[index];
    }

    public static void DisplayActorStats(ActorManager actorManager)
    {
        WriteLine("Actor Stats:");
        WriteLine(
            "----------------------------------------------------------------------------------------------------------------------");
        WriteLine(
            $"{"Name",-15} {"Symbol",-7} {"Color",-10} {"HP",-8} {"Attack",-10} {"Defense",-10} {"Speed",-10} {"SLM",-6} {"Inactive",-10} {"Colliding",-10} {"Adjacent",-30}");
        WriteLine(
            "----------------------------------------------------------------------------------------------------------------------");

        foreach (var actor in actorManager.GetAllActors())
        {
            var adjacentActors = actorManager.AdjacentActors(actor);
            string adjacentActorsInfo = adjacentActors.Count > 0
                ? string.Join(", ", adjacentActors.Select(a => a.Symbol.ToString()))
                : "None";

            WriteWithColor(
                $"{actor.Name,-15} " +
                $"{actor.Symbol,-7} " +
                $"{actor.Color,-10} " +
                $"{actor.HealthPoints,-8:F1} " +
                $"{actor.AttackPoints,-10:F1} " +
                $"{actor.DefensePoints,-10:F1} " +
                $"{actor.Speed,-10} " +
                $"{actor.SpeedLevelMultiplier(),-6:F1} " +
                $"{actor.IsInactive,-10} " +
                $"{actor.IsColliding,-10} " +
                $"{adjacentActorsInfo,-30}",
                actor.Color, true);
        }

        WriteLine(
            "----------------------------------------------------------------------------------------------------------------------");
    }
}