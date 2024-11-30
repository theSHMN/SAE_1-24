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

    
    public List<Actor> GetAdjacentActors()
    {
        List<Actor> adjacentActors = new List<Actor>();
        List<Actor> actors = _actorManager.GetAllActors();
        
        Actor player = _actorManager.GetActorByName("Classes");
        Grid.GridPosition? playerPosition = _actorManager.GetActorPosition(player);

        if (playerPosition == null) return adjacentActors;

        foreach (var actor in actors)
        {
            actor.IsColliding = false;
        }

        //Grid.GridPosition? playerPosition = _actorManager.GetActorPosition(player);
        
        // if (playerPosition == null)
        // {
        //     WriteLine("NO PLAYER FOUND!");
        //     return;
        // }
        // else
        // {
        //     WriteLine($"Misc Found {player.Name}");
        // }

        foreach (var actor in actors)
        {
            if (actor == player) continue; 
            
            Grid.GridPosition? otherActorPosition = _actorManager.GetActorPosition(actor);
            if(otherActorPosition == null) continue;
            
                if (AreAdjacent(playerPosition, otherActorPosition))
                {
                    adjacentActors.Add(actor);
                    player.IsColliding = true;
                    actor.IsColliding = true;
                    WriteLine();
                }
        }
        return adjacentActors;
    }

    private bool AreAdjacent(Grid.GridPosition pos1, Grid.GridPosition pos2)
    {
        //Vertical, horizontal, diagonal
        return (Math.Abs(pos1.row - pos2.row) == 1 && pos1.col == pos2.col) ||
               (Math.Abs(pos1.col - pos2.col) == 1 && pos1.row == pos2.row) ||
               (Math.Abs(pos1.row - pos2.row) == 1 && Math.Abs(pos1.col - pos2.col) == 1);
    }

  
    
    
    // private void Interact(Actors actor1, Actors actor2)
    // {
    //     
    //     if (actor1 is MiniGame miniGame1)
    //     {
    //         miniGame1.Start();
    //     }
    //     else if (actor2 is MiniGame miniGame2)
    //     {
    //         miniGame2.Start();
    //     }
    //
    //     
}