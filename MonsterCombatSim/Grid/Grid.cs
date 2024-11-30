namespace MonsterCombatSim;

public class Grid
{
    public record GridPostion(int posRow, int posCol)
    {
        public GridPostion value { get; set; }
    }


    internal int _maxRow { get; set; }
    internal int _maxCol { get; set; }
    
    public static int MaxRow { get; private set; }
    public static int MaxCol { get; private set; }

    public Grid(int maxRow, int maxCol)
    {
        _maxRow = maxRow;
        _maxCol = maxCol;

        MaxRow = maxRow;
        MaxCol = maxCol;
    }
    

    public static bool CheckIfPositionWithinGrid(GridPostion position)
    {
        return position.posRow >= 0 && position.posRow < MaxRow &&
               position.posCol >= 0 && position.posCol < MaxCol;
    }
    
    public void DisplayGridWithActors(Dictionary<Actor, GridPostion> actorPositions)
    {
        Write("   ");
        for (int col = 0; col < _maxCol; col++)
        {
            WriteWithColor($" {col,3} ", ConsoleColor.Yellow);
        }

        WriteLine();

        for (int row = 0; row < _maxRow; row++)
        {
            WriteWithColor($" {row,2} ", ConsoleColor.Yellow);

            for (int col = 0; col < _maxCol; col++)
            {
                GridPostion currentPosition = new GridPostion(row, col);

                Actor actorAtPosition = actorPositions.FirstOrDefault(pair => pair.Value.Equals(currentPosition)).Key;

                if (actorAtPosition != null)
                { 
                    DisplayGridTile(actorAtPosition.Symbol, actorAtPosition.Color, actorAtPosition.Color);
                }
                else
                {
                    DisplayGridTile(' ', ConsoleColor.Black, ConsoleColor.Gray);
                }
            }
            WriteLine();
        }
    }
    
    
    
    public static void DisplayGridTile(char content, ConsoleColor contentColor, ConsoleColor backgroundColor)
    {
        WriteWithColor("[", backgroundColor);
        WriteWithColor($"{content,3}", contentColor);
        WriteWithColor("]", backgroundColor);
    }
}