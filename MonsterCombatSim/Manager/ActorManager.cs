using MonsterCombatSim.Movement;

namespace MonsterCombatSim.Manager;


public class ActorManager
{
    private List<Actor> _allActors;
    private Dictionary<Actor, Grid.GridPostion> _allActorsAndPositionsOnGrid;
    

    public ActorManager()
    {
        _allActors = new List<Actor>();
        _allActorsAndPositionsOnGrid = new Dictionary<Actor, Grid.GridPostion>();

    }

    public void AddActorToList(Actor actor)
    {
        _allActors.Add(actor);
    }

    public void AssignActorToGridPosition(List<Actor> allActors, List<Grid.GridPostion> allPositions)
    {
        if (allPositions.Count < allActors.Count)
        {
            throw new InvalidOperationException("Actors count exceeds count of all possible GridPositions");
        }
        _allActorsAndPositionsOnGrid.Clear();

        for (int i = 0; i < allActors.Count; i++)
        {
            _allActorsAndPositionsOnGrid[allActors[i]] = allPositions[i];
        }
    }

    public List<Actor> GetAllActors() => _allActors;

    public Dictionary<Actor, Grid.GridPostion> GetAllActorsAndPositionsOnGrid() => _allActorsAndPositionsOnGrid;

    
    
    public void MovePlayer(MoveAction action)
    {
        
        var playerEntry = _allActorsAndPositionsOnGrid.FirstOrDefault(entry => entry.Key.Type == ActorType.Player);
        if (playerEntry.Key == null)
        {
            Debug.DebugMessage("Player actor not found.");
            return;
        }
        
        var player = playerEntry.Key;
        var currentPosition = playerEntry.Value;

        
        if (MovementAction.MoveActor(ref currentPosition, action))
        {
            _allActorsAndPositionsOnGrid[player] = currentPosition;
            Debug.DebugMessage($"Player moved to {currentPosition.posRow}, {currentPosition.posCol}");
        }
        else
        {
            Debug.DebugMessage("Move action failed: Out of bounds or invalid action.");
        }
    }
}

