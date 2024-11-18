using MonsterCombatSimulator;
using MonsterCombatSimulator.Monster;
using MonsterCombatSimulator.Monster.Player;
using Timer = System.Threading.Timer;

namespace DefaultNamespace;

public class ActorManager
{
    private List<Actor> _allActors;
    private Dictionary<Grid.GridPosition, Actor> _allActorPositions;
    private Grid _grid;
    private GameTimer _gameTimer;

    public ActorManager(Grid grid, GameTimer gameTimer)
    {
        _allActors = new List<Actor>();
        _allActorPositions = new Dictionary<Grid.GridPosition, Actor>();
        _grid = grid;
        _gameTimer = gameTimer;
    }

    public void AddActor(Actor actor)
    {
        Grid.GridPosition? assignablePosition = AssignRandomPosition();
        if (assignablePosition != null)
        {
            _allActors.Add(actor);
            _allActorPositions[assignablePosition] = actor;
        }
        else
        {
            WriteWithColor("Actormanager, List - No positions available.", ConsoleColor.Red);
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
                if (!_allActorPositions.ContainsKey(position))
                {
                    allAvailablePositions.Add(position);
                }
            }
        }

        if (allAvailablePositions.Count == 0) return null;
        int randomIndex = GetRandom(0, allAvailablePositions.Count);
        return allAvailablePositions[randomIndex];
    }

    public List<Actor> GetAllActors() => _allActors;
    public Dictionary<Grid.GridPosition, Actor> GetAllActorPositions() => _allActorPositions;
    public Actor GetActorByName(string name) => _allActors.FirstOrDefault(actor => actor.Name == name);

    public Grid.GridPosition? GetActorPosition(Actor actor)
    {
        foreach (var kvp in _allActorPositions)
        {
            if (kvp.Value == actor)
            {
                return kvp.Key;
            }
        }

        return null;
    }

    public bool UpdateActorPosition(Actor actor, Grid.GridPosition newPosition)
    {
        Grid.GridPosition? currentPosition = GetActorPosition(actor);

        if (_allActorPositions.ContainsKey(newPosition))
            return false;

        if (currentPosition != null)
            _allActorPositions.Remove(currentPosition);

        _allActorPositions[newPosition] = actor;
        return true;
    }

    public void MoveActorRandomly(string actorName, int moveSteps = 1)
    {
        Actor movingActor = GetActorByName(actorName);
        if (movingActor == null) return;
        Grid.GridPosition? currentPosition = GetActorPosition(movingActor);
        if (currentPosition == null) return;


        List<Grid.GridPosition> potentialPositions = new List<Grid.GridPosition>();

        for (int rowOffset = -moveSteps; rowOffset <= moveSteps; rowOffset++)
        {
            for (int colOffset = -moveSteps; colOffset <= moveSteps; colOffset++)
            {
                if (rowOffset == 0 && colOffset == 0) continue;
                if (Math.Abs(rowOffset) + Math.Abs(colOffset) > moveSteps) continue;

                int newRow = currentPosition.row + rowOffset;
                int newCol = currentPosition.col + colOffset;

                if (newRow >= 0 && newCol >= 0
                                && newRow < _grid._maxRow && newCol < _grid._maxCol
                                && !_allActorPositions.ContainsKey(new Grid.GridPosition(newRow, newCol)))
                {
                    potentialPositions.Add(new Grid.GridPosition(newRow, newCol));
                }
            }
        }

        if (potentialPositions.Count > 0)
        {
            Grid.GridPosition newPosition = potentialPositions[GetRandom(0, potentialPositions.Count)];
            UpdateActorPosition(movingActor, newPosition);
        }
    }

    public List<Actor> AdjacentActors(Actor attacker)
    {
        List<Actor> adjacentActors = new List<Actor>();
        Grid.GridPosition? attackerPosition = GetActorPosition(attacker);

        if (attackerPosition == null)
            return adjacentActors;

        int startRow = attackerPosition.row - 1;
        int startCol = attackerPosition.col - 1;

        for (int rowOffset = 0; rowOffset <= 2; rowOffset++)
        {
            for (int colOffset = 0; colOffset <= 2; colOffset++)
            {
                if (rowOffset == 1 && colOffset == 1)
                    continue;

                int newRow = startRow + rowOffset;
                int newCol = startCol + colOffset;

                if (newRow >= 0 && newRow < _grid._maxRow && newCol >= 0 && newCol < _grid._maxCol)
                {
                    Grid.GridPosition adjacentPosition = new Grid.GridPosition(newRow, newCol);
                    if (_allActorPositions.ContainsKey(adjacentPosition))
                    {
                        adjacentActors.Add(_allActorPositions[adjacentPosition]);
                    }
                }
            }
        }
        return adjacentActors;
    }

    private void ResetCollisionStates()
    {
        foreach (var actor in GetAllActors())
        {
            actor.IsColliding = false;
        }

    }
    
    public void UpdateCollisionStates()
    {
        
        foreach (var actor in GetAllActors())
        {
            var adjacentActors = AdjacentActors(actor);

            foreach (var adjacentActor in adjacentActors)
            {
                if (adjacentActor is Player)
                {
                    actor.IsColliding = true;
                    break;
                }
            }

        }
    }
    
    public void UpdateAdjacentActorsCollision()
    {
        ResetCollisionStates();
        UpdateCollisionStates();
    }
    
}