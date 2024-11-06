using EscapeRoom.Actors;

namespace EscapeRoom;

public static class Input
{
    // public static int InputDirection(string inputPrompt)
    // {
    //     int outputInteger;
    //
    //     do
    //     {
    //         Write(inputPrompt);
    //         string inputString = ReadLine();
    //
    //         if (int.TryParse(inputString, out outputInteger) && outputInteger >= 1 && outputInteger <= 9)
    //         {
    //             return outputInteger;
    //         }
    //
    //
    //         WriteLine("Invalid!");
    //
    //
    //         //Thread.Sleep(500);
    //     } while (true);
    // }


    public static int MarkDirectionWithNumpad (string prompt)
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
    public static void DirectionalInput(Actor playerActor, ActorManager actorManager, Grid grid)
    {
        //int direction = InputDirection("Use Numpad to move: ");
        Grid.GridPosition? currentPosition = actorManager.GetActorPosition(playerActor);

        if (currentPosition == null)
        {
            WriteLine("Error: Actor position not found!");
            return;
        }

        Grid.GridPosition targetPosition = currentPosition;

        while (true)
        {
            grid.DisplayGrid(actorManager.GetActorPositions(), targetPosition);
            WriteLine();
            int direction = MarkDirectionWithNumpad("Use Numpad to mark direction and (Numpad 0) to confirm: ");
            WriteLine();
            Clear();
            

            if (direction == 0)
            {
                actorManager.UpdateActorPosition(playerActor, targetPosition);
                break;
            }
            

            targetPosition = direction switch
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

/*
            //A record is immutable > new instance
            switch (direction)
            {
                case 8:
                    if (targetPosition.row > 0)
                        targetPosition = new Grid.GridPosition(targetPosition.row - 1, targetPosition.col);
                    break;
                case 9:
                    if (targetPosition.row > 0 && targetPosition.col < grid._maxCol - 1)
                        targetPosition = new Grid.GridPosition(targetPosition.row - 1, targetPosition.col + 1);
                    break;
                case 6:
                    if (targetPosition.col < grid._maxCol - 1)
                        targetPosition = new Grid.GridPosition(targetPosition.row, targetPosition.col + 1);
                    break;
                case 3:
                    if (targetPosition.row < grid._maxRow - 1 && targetPosition.col < grid._maxCol - 1)
                        targetPosition = new Grid.GridPosition(targetPosition.row + 1, targetPosition.col + 1);
                    break;
                case 2:
                    if (targetPosition.row < grid._maxRow - 1)
                        targetPosition = new Grid.GridPosition(targetPosition.row + 1, targetPosition.col);
                    break;
                case 1:
                    if (targetPosition.row < grid._maxRow - 1 && targetPosition.col > 0)
                        targetPosition = new Grid.GridPosition(targetPosition.row + 1, targetPosition.col - 1);
                    break;
                case 4:
                    if (targetPosition.col > 0)
                        targetPosition = new Grid.GridPosition(targetPosition.row, targetPosition.col - 1);
                    break;
                case 7:
                    if (targetPosition.row > 0 && targetPosition.col > 0)
                        targetPosition = new Grid.GridPosition(targetPosition.row - 1, targetPosition.col - 1);
                    break;
                default:
                    WriteLine("Invalid direction!");
                    return;
            }
            */
        }
        
        //actorManager.UpdateActorPosition(playerActor, targetPosition);
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
}