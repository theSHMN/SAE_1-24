using DefaultNamespace;
using MonsterCombatSimulator.Monster;
using MonsterCombatSimulator.Monster.Player;

namespace MonsterCombatSimulator.Movement;

public class ActorMovement
{
    private ActorManager _actorManager;
    private Grid _grid;
    private GameTimer _gameTimer;

    public ActorMovement(ActorManager actorManager, Grid grid, GameTimer gametimer)
    {
        _actorManager = actorManager;
        _grid = grid;
        _gameTimer = gametimer; 

        gametimer.OnTick += OnTick;
    }

    private void OnTick()
    {
        foreach (var actor in _actorManager.GetAllActors())
        {
            
            if(actor is Player || actor.IsInactive) continue;
            
            MoveActorRandomly(actor);
        }
    }

    public readonly Dictionary<ConsoleKey, (int rowOffset, int colOffset)> DirectionMapping = new()
    {
        { ConsoleKey.D8, (-1, 0) }, // up
        { ConsoleKey.D2, (1, 0) }, // down
        { ConsoleKey.D4, (0, -1) }, // left
        { ConsoleKey.D6, (0, 1) }, // right
        { ConsoleKey.D7, (-1, -1) }, // up-left
        { ConsoleKey.D9, (-1, 1) }, // up-right
        { ConsoleKey.D1, (1, -1) }, // down-left
        { ConsoleKey.D3, (1, 1) }, // down-right
    };

    public Grid.GridPosition GetNewPosition(Grid.GridPosition currentPosition, ConsoleKey key)
    {
        if (DirectionMapping.TryGetValue(key, out var offset))
        {
            return new Grid.GridPosition
            (
                currentPosition.row + offset.rowOffset,
                currentPosition.col + offset.colOffset
            );
        }

        return currentPosition;
    }

    public Grid.GridPosition GetRandomPosition(Grid.GridPosition currentPosition)
    {
        var directions = DirectionMapping.Values.ToList();
        Grid.GridPosition newPosition;

        do
        {
            var randomDirection = GetRandomItem(directions);
            newPosition = new Grid.GridPosition
            (
                currentPosition.row + randomDirection.rowOffset,
                currentPosition.col + randomDirection.colOffset
            );
        } while (!_grid.IsValidWithinGrid(newPosition));

        return newPosition;
    }

    public void MoveActorRandomly(Actor actor)
    {
        Grid.GridPosition? currenPosition = _actorManager.GetActorPosition(actor);

        if (currenPosition == null)
        {
            WriteLine($"{actor.Name} has no position!");
            return;
        }

        Grid.GridPosition newPosition = GetRandomPosition(currenPosition);

        if (_actorManager.UpdateActorPosition(actor, newPosition))
        {
            WriteLine($"{actor.Name} move to position ({newPosition.row}/{newPosition.col})");
        }
        else
        {
            WriteLine($"Error {actor.Name} not moved!");
        }
    }

    public void MovePlayer(Actor actor, ConsoleKey key)
    {
        Grid.GridPosition? currentPosition = _actorManager.GetActorPosition(actor);
        if (currentPosition == null)
        {
            WriteLine("No player position found!");
            return;
        }

        Grid.GridPosition newPosition = GetNewPosition(currentPosition, key);

        if (_grid.IsValidWithinGrid(newPosition) && _actorManager.UpdateActorPosition(actor, newPosition))
        {
            WriteLine($"{actor.Name} move to position ({newPosition.row}/{newPosition.col})");
            
        }
        else
        {
            WriteLine($"Error {actor.Name} not moved!");
        }
    }
}