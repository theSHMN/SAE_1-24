using EscapeRoom.Actors;

namespace EscapeRoom;

public class GridManager
{
    
    private ActorManager _actorManager;
    private Grid _grid;

    public GridManager(ActorManager actorManager, Grid grid)
    {
        _actorManager = actorManager;
        _grid = grid;
    }
    
    public void CheckAdjacency()
    {
        List<Actor> actors = _actorManager.GetAllActors();

        for (int i = 0; i < actors.Count; i++)
        {
            Grid.GridPosition? position1 = _actorManager.GetActorPosition(actors[i]);
            if(position1 == null) continue; // Skip if position not found
            
            for (int j = i + 1; j < actors.Count; j++)
            {
                Grid.GridPosition? position2 = _actorManager.GetActorPosition(actors[j]);
                if(position2 == null) continue; // Skip if position not found
                    
                if (AreAdjacent(position1.Value, position2.Value))
                {
                    actors[i].IsCollision = true;
                    actors[j].IsCollision = true;
                }
            }
        }
        
    }

    private bool AreAdjacent(Grid.GridPosition pos1, Grid.GridPosition pos2)
    {
        //Vertical, horizontal, diagonal
        return (Math.Abs(pos1.row - pos2.row) == 1 && pos1.col == pos2.col) ||
               (Math.Abs(pos1.col - pos2.col) == 1 && pos1.row == pos2.row) ||
               (Math.Abs(pos1.row - pos2.row) == 1 && Math.Abs(pos1.col - pos2.col) == 1);

    }
    
 
}