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
        Actor player = _actorManager.GetActorByName("Player");

        foreach (var actor in actors)
        {
            actor.IsCollision = false;
        }

        Grid.GridPosition? playerPosition = _actorManager.GetActorPosition(player);
        // if (playerPosition == null)
        // {
        //     WriteLine("NO PLAYER FOUND!");
        //     return;
        // }
        // else
        // {
        //     WriteLine($"Player Found {player.Name}");
        // }

        foreach (var actor in actors)
        {
            if (actor == player) continue; 
            
            Grid.GridPosition? otherActorPosition = _actorManager.GetActorPosition(actor);
            if(otherActorPosition == null) continue;
            
            
                if (AreAdjacent(playerPosition, otherActorPosition))
                {
                    player.IsCollision = true;
                    actor.IsCollision = true;
                    HelperFunctions.WriteWithColor($"Player actor is near {actor.Name} actor.", ConsoleColor.Yellow);
                    WriteLine();
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
    
    
    // private void Interact(Actor actor1, Actor actor2)
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