using EscapeRoom.Actors;

namespace EscapeRoom;

public class Grid
{
    public record GridPosition(int row, int col)
    {
        public GridPosition Value { get; set; }
    }

    public int _maxRow { get; }
    public int _maxCol { get; }
    

    public Grid(int maxRow, int maxCol)
    {
        _maxRow = maxRow;
        _maxCol = maxCol;
    }

    public void DisplayGrid(Dictionary<GridPosition, Actor> actorPositions, GridPosition? markTargetPosition = null)
    {
        Write("   ");
        for (int col = 0; col < _maxCol; col++)
        {
            WriteWithColor($" {col} ", ConsoleColor.Yellow);
        }   
        WriteLine();
        
        for (int row = 0; row < _maxRow; row++)
        {
            WriteWithColor($" {row} ", ConsoleColor.Yellow);
    
            for (int col = 0; col < _maxCol; col++)
            {
                GridPosition position = new GridPosition(row, col);
                
                if (markTargetPosition!= null && position.Equals(markTargetPosition))
                    DisplayRoomTile('+', ConsoleColor.Green,ConsoleColor.Red);
                else if (actorPositions.TryGetValue(position, out var actor))
                    DisplayRoomTile(actor.Symbol, actor.Color, ConsoleColor.Yellow);
                else
                    DisplayRoomTile(' ', ConsoleColor.Black, ConsoleColor.Gray);
            }
            WriteLine();
        }
    }
    
  

}