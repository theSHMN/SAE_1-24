using EscapeRoom.Actors;

namespace EscapeRoom;

public static class Input
{
    public static int MarkDirectionWithInput (string prompt)
    {
        Write(prompt);
        ConsoleKeyInfo keyInfo = ReadKey(true); // Keypress without Enter
        WriteLine($"Key pressed: {keyInfo.Key}");

        return keyInfo.Key switch
        {
            ConsoleKey.D8 => 8,
            ConsoleKey.D9 => 9,
            ConsoleKey.D6 => 6,
            ConsoleKey.D3 => 3,
            ConsoleKey.D2 => 2,
            ConsoleKey.D1 => 1,
            ConsoleKey.D4 => 4,
            ConsoleKey.D7 => 7,
            ConsoleKey.D0 => 0, // Enter
            _ => -1
        };
    }
    
    // ref > By reference not by value. To update targetPosition within method.
    // Update of target position is made outside method so the variable is always up to date
    public static bool DirectionalInput(Actor playerActor, ActorManager actorManager, Grid grid, ref Grid.GridPosition targetPosition)
    {
        int direction = MarkDirectionWithInput("Use Numpad to mark direction and (Numpad 0) to confirm: ");
        WriteLine();

        if (direction == 0)
        {
            actorManager.UpdateActorPosition(playerActor, targetPosition);
            return true; // Move confirmed
        }

        targetPosition = GetTargetPosition(direction, targetPosition, grid);
        return false; // Move not confirmed
    }
    
             #region Old code
    
//     
//     public static void DirectionalInput(Actors playerActor, Manager actorManager, Grid grid)
//     {
//         //int direction = InputDirection("Use Numpad to move: ");
//         Grid.GridPosition? currentPosition = actorManager.GetActorPosition(playerActor);
//
//         if (currentPosition == null)
//         {
//             WriteLine("Error: Actors position not found!");
//             return;
//         }
//
//         Grid.GridPosition targetPosition = currentPosition;
//
//         while (true)
//         {
//             grid.DisplayGrid(actorManager.GetActorPositions(), targetPosition);
//             WriteLine();
//             int direction = MarkDirectionWithInput("Use Numpad to mark direction and (Numpad 0) to confirm: ");
//             WriteLine();
//             
//             
//
//             if (direction == 0)
//             {
//                 actorManager.UpdateActorPosition(playerActor, targetPosition);
//                 break;
//             }
//
//
//             targetPosition = GetTargetPosition(direction, targetPosition, grid);
//
// 
//             //A record is immutable > new instance
//             switch (direction)
//             {
//                 case 8:
//                     if (targetPosition.row > 0)
//                         targetPosition = new Grid.GridPosition(targetPosition.row - 1, targetPosition.col);
//                     break;
//                 case 9:
//                     if (targetPosition.row > 0 && targetPosition.col < grid._maxCol - 1)
//                         targetPosition = new Grid.GridPosition(targetPosition.row - 1, targetPosition.col + 1);
//                     break;
//                 case 6:
//                     if (targetPosition.col < grid._maxCol - 1)
//                         targetPosition = new Grid.GridPosition(targetPosition.row, targetPosition.col + 1);
//                     break;
//                 case 3:
//                     if (targetPosition.row < grid._maxRow - 1 && targetPosition.col < grid._maxCol - 1)
//                         targetPosition = new Grid.GridPosition(targetPosition.row + 1, targetPosition.col + 1);
//                     break;
//                 case 2:
//                     if (targetPosition.row < grid._maxRow - 1)
//                         targetPosition = new Grid.GridPosition(targetPosition.row + 1, targetPosition.col);
//                     break;
//                 case 1:
//                     if (targetPosition.row < grid._maxRow - 1 && targetPosition.col > 0)
//                         targetPosition = new Grid.GridPosition(targetPosition.row + 1, targetPosition.col - 1);
//                     break;
//                 case 4:
//                     if (targetPosition.col > 0)
//                         targetPosition = new Grid.GridPosition(targetPosition.row, targetPosition.col - 1);
//                     break;
//                 case 7:
//                     if (targetPosition.row > 0 && targetPosition.col > 0)
//                         targetPosition = new Grid.GridPosition(targetPosition.row - 1, targetPosition.col - 1);
//                     break;
//                 default:
//                     WriteLine("Invalid direction!");
//                     return;
//             }
//             
//         }
//         
//     }

            #endregion
            
            
    private static Grid.GridPosition GetTargetPosition(int direction, Grid.GridPosition targetPosition, Grid grid)
    {
        return direction switch
        {
            8 when targetPosition.row > 0 => new Grid.GridPosition(targetPosition.row - 1, targetPosition.col),
            9 when targetPosition.row > 0 && targetPosition.col < grid._maxCol - 1 => new Grid.GridPosition(
                targetPosition.row - 1, targetPosition.col + 1),
            6 when targetPosition.col < grid._maxCol - 1 => new Grid.GridPosition(targetPosition.row,
                targetPosition.col + 1),
            3 when targetPosition.row < grid._maxRow - 1 && targetPosition.col < grid._maxCol - 1 =>
                new Grid.GridPosition(targetPosition.row + 1, targetPosition.col + 1),
            2 when targetPosition.row < grid._maxRow - 1 => new Grid.GridPosition(targetPosition.row + 1,
                targetPosition.col),
            1 when targetPosition.row < grid._maxRow - 1 && targetPosition.col > 0 => new Grid.GridPosition(
                targetPosition.row + 1, targetPosition.col - 1),
            4 when targetPosition.col > 0 => new Grid.GridPosition(targetPosition.row, targetPosition.col - 1),
            7 when targetPosition.row > 0 && targetPosition.col > 0 => new Grid.GridPosition(targetPosition.row - 1,
                targetPosition.col - 1),
            _ => targetPosition // If direction is invalid, keep the same target position
        };
    }
    

    public static int ParseToInteger(string prompt, int lowerBound = 1, int upperBound = 100)
    {
        int outputInteger;
        do
        {
            WriteLine(prompt);
            string inputString = ReadLine();

            if (int.TryParse(inputString, out outputInteger)
                && outputInteger >= lowerBound && outputInteger <= upperBound)
            {
                ForegroundColor = ConsoleColor.Green;
                WriteLine($"[{outputInteger}]");
                ResetColor();
                break;
            }
            else
            {
                ForegroundColor = ConsoleColor.Red;
                WriteLine($"[{inputString}]");
                ResetColor();
            }
        } while (true);

        return outputInteger;
    }

    public static bool InteractionInquiry(Actor actor)
    {
        var options = new Dictionary<int, (string, ConsoleColor, bool)>
        {
            { 1, ("Yes", ConsoleColor.Green, true) },
            { 2, ("No", ConsoleColor.Red, false) }
        };

        return MenuSelection($"Interact with {actor.Name}?", options);
    }


    
    public static T MenuSelection<T>(string prompt, Dictionary<int, (string itemDescription, ConsoleColor color, T result)> options)
    {
        WriteWithColor(prompt, ConsoleColor.Yellow);

        foreach (var option in options)
        {
            WriteLine();
            WriteWithColor($"({option.Key}) - {option.Value.itemDescription}", option.Value.color);
        }

        WriteLine();
        int input = ParseToInteger($"Enter a choice ({string.Join(", ", options.Keys)}):", options.Keys.Min(),
            options.Keys.Max());

        if (options.TryGetValue(input, out var selectedOption))
        {
            WriteWithColor($"({input}) - {selectedOption.itemDescription}", selectedOption.color);
            Clear();
            return selectedOption.result;
        }
        
        throw new InvalidOperationException("Invalid");
    }
}