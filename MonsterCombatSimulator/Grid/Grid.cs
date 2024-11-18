using MonsterCombatSimulator.Monster;

namespace MonsterCombatSimulator;

public class Grid
{
    public record GridPosition(int row, int col)
    {
        public GridPosition value { get; set; }
    }

    public int _maxRow { get; private set; }
    public int _maxCol { get; private set; }

    public Grid(int maxRow, int maxCol)
    {
        _maxRow = maxRow;
        _maxCol = maxCol;
    }

    public bool IsValidWithinGrid(GridPosition position)
    {
        return position.row >= 0 && position.row < _maxRow && position.col >= 0 && position.col < _maxCol;
    }


    public void DisplayGrid(Dictionary<GridPosition, Actor> actorPositions)
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
                GridPosition position = new GridPosition(row, col);

                if (actorPositions.ContainsKey(position))
                {
                    Actor actor = actorPositions[position];

                    if (actor.IsInactive)
                    {
                        DisplayRoomTile('+', actor.Color, ConsoleColor.Red);
                    }
                    else
                    {
                        DisplayRoomTile(actor.Symbol, actor.Color, actor.Color);
                    }
                }
                else
                {
                    DisplayRoomTile(' ', ConsoleColor.Black, ConsoleColor.Gray);
                }
            }

            WriteLine();
        }
    }

    public static void DisplayRoomTile(char content, ConsoleColor contentColor, ConsoleColor backgroundColor)
    {
        WriteWithColor("[", backgroundColor);
        WriteWithColor($"{content,3}", contentColor);
        WriteWithColor("]", backgroundColor);
    }
}