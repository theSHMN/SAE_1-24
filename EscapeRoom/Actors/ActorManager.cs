namespace EscapeRoom.Actors;

public class ActorManager
{
    // Central list of all actors
    private List<Actor> _allActors;
    private Dictionary< Grid.GridPosition,Actor> _actorPositions;
    private Grid _grid;

    public ActorManager(Grid grid)
    {
        _allActors = new List<Actor>();
        _actorPositions = new Dictionary< Grid.GridPosition,Actor>();
        _grid = grid;
    }

    public void AddActor(Actor actor)
    {
        Grid.GridPosition? position = AssignRandomPosition();
        if (position != null)
        {
            _actorPositions[position] = actor;
            _allActors.Add(actor);
        }
        else
        {
            WriteLine("Error: No available positions on grid.");
        }
    }

    private Grid.GridPosition AssignRandomPosition()
    {
        List<Grid.GridPosition> allAvailablePositions = new List<Grid.GridPosition>();

        for (int row = 0; row < _grid._maxRow; row++)
        {
            for (int col = 0; col < _grid._maxCol; col++)
            {
                Grid.GridPosition position = new Grid.GridPosition(row, col);
                if (!_actorPositions.ContainsKey(position))
                {
                    allAvailablePositions.Add(position);
                }
            }
        }

        if (allAvailablePositions.Count == 0) return null;

        int randomIndex = HelperFunctions.GetRandom(0, allAvailablePositions.Count);
        return allAvailablePositions[randomIndex];

    }
 
    public List<Actor> GetAllActors() => _allActors;
    public Dictionary<Grid.GridPosition,Actor> GetActorPositions() => _actorPositions;
    
    
    // Position of one actor.
    public Grid.GridPosition? GetActorPosition(Actor actor)
    {
        foreach (var kvp in _actorPositions)
        {
            if (kvp.Value == actor)
            {
                
                return kvp.Key; // Return the position of the actor
            }
        }

        return null; // Return null if the actor is not found
    }

    public bool UpdateActorPosition(Actor actor, Grid.GridPosition newPosition)
    {
        Grid.GridPosition? currentPosition = GetActorPosition(actor);

        if (_actorPositions.ContainsKey(newPosition))
            return false;

        if (currentPosition != null) 
            _actorPositions.Remove(currentPosition);

        _actorPositions[newPosition] = actor;
        return true;

    }




}