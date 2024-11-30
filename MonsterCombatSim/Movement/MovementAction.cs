namespace MonsterCombatSim.Movement;

public enum MoveAction
{
    Up,
    UpRight,
    Right,
    DownRight,
    Down,
    DownLeft,
    Left,
    UpLeft,
    Confirm
    
}
public class MovementAction
{

    public static List<MoveAction> GetMoveActions()
    {
        return Enum.GetValues(typeof(MoveAction)).Cast<MoveAction>().ToList();
    }
    
    
    public static bool MoveActor(ref Grid.GridPostion currentPosition, MoveAction action)
    {
        Grid.GridPostion newPosition = currentPosition;

        switch (action)
        {
            case MoveAction.Up:
                newPosition = new Grid.GridPostion(currentPosition.posRow - 1, currentPosition.posCol);
                Debug.DebugMessage("MoveAction-UP");
                break;
            case MoveAction.UpRight:
                newPosition = new Grid.GridPostion(currentPosition.posRow - 1, currentPosition.posCol + 1);
                Debug.DebugMessage("MoveAction-UP/RIGHT");
                break;
            case MoveAction.Right:
                newPosition = new Grid.GridPostion(currentPosition.posRow, currentPosition.posCol + 1);
                Debug.DebugMessage("MoveAction-RIGHT");
                break;
            case MoveAction.DownRight:
                newPosition = new Grid.GridPostion(currentPosition.posRow + 1, currentPosition.posCol + 1);
                Debug.DebugMessage("MoveAction-DOWN/RIGHT");
                break;
            case MoveAction.Down:
                newPosition = new Grid.GridPostion(currentPosition.posRow + 1, currentPosition.posCol);
                Debug.DebugMessage("MoveAction-DOWN");
                break;
            case MoveAction.DownLeft:
                newPosition = new Grid.GridPostion(currentPosition.posRow + 1, currentPosition.posCol - 1);
                Debug.DebugMessage("MoveAction-DOWN/LEFT");
                break;
            case MoveAction.Left:
                newPosition = new Grid.GridPostion(currentPosition.posRow, currentPosition.posCol - 1);
                Debug.DebugMessage("MoveAction-LEFT");
                break;
            case MoveAction.UpLeft:
                newPosition = new Grid.GridPostion(currentPosition.posRow - 1, currentPosition.posCol - 1);
                Debug.DebugMessage("MoveAction-UP/LEFT");
                break;
            default:
                return false;
        }

        if (Grid.CheckIfPositionWithinGrid(newPosition))
        {
            currentPosition = newPosition;
            return true;
        }

        return false;
    }
    
    
    public static bool MoveActorThroughActions(ref Grid.GridPostion currentPosition, List<MoveAction> actions)
    {
        foreach (MoveAction action in actions)
        {
            // Call the action to move the actor
            bool moved = MoveActor(ref currentPosition, action);

            // Check if the actor is still within the grid
            if (!moved)
            {
                return false; // If out of bounds, stop
            }
        }
        return true; // Successfully moved through all actions
    }
}