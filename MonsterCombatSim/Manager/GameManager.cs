using MonsterCombatSim.Timer;

namespace MonsterCombatSim.Manager;

// Should update the Grid with the Actor positions depending on a tick

public class GameManager
{
        Grid grid = new (5,5);
        GridManager gridManager = new ();
        ActorManager actorManager = new ();
        InputManager inputManager = new ();
        GameTick gameTick = new ();
    
    public void RunGame()
    {
        
        Debug.DebugMessage(@"(1) - initialise:
Grid
GridManager
ActorManager
InputManager
GameTick");
        Debug.DebugMessage("\n");
        Debug.DebugSleepThread(2000);
        
        Debug.DebugMessage("(2) - Print GridPositions in order");
        Debug.DebugWrap(() =>
        {


            foreach (var positions in gridManager.GetGridPositions())
            {
                WriteLine($"Row: {positions.posRow} / Col: {positions.posCol}");
            }
        });
        Debug.DebugMessage("\n");
        Debug.DebugSleepThread(2000);
        
        Debug.DebugMessage("(3) - Add new instances of Actors to List and print the result");
        actorManager.AddActorToList(new Actor("Hero", 'H', ConsoleColor.Yellow, type: ActorType.Player));
        actorManager.AddActorToList(new Actor(state: ActorState.Disarmed));
        actorManager.AddActorToList(new Actor(state: ActorState.Frozen));
        actorManager.AddActorToList(new Actor(state: ActorState.ItemState));
        actorManager.AddActorToList(new Actor(state: ActorState.Haste));
        actorManager.AddActorToList(new Actor());

        Debug.DebugWrap(() =>
        {
            foreach (var actor in actorManager.GetAllActors())
            {
                WriteLine($"Actor Name: {actor.Name}, Char: {actor.Symbol}, Color: {actor.Color} ");
            }
        });

        Debug.DebugMessage("\n");
        Debug.DebugSleepThread(2000);
        
        Debug.DebugMessage("(4) - Assign Actors to random GridPositions and print the result");
        actorManager.AssignActorToGridPosition(actorManager.GetAllActors(),gridManager.GetGridPositions());
        foreach (var actor in actorManager.GetAllActorsAndPositionsOnGrid())
        {
            WriteLine($"Actor Name: {actor.Key.Name}, Symbol: {actor.Key.Symbol}, Color, {actor.Key.Color}, Row: {actor.Value.posRow}/ Col: {actor.Value.posCol}");
        }
        Debug.DebugMessage("\n");
        Debug.DebugSleepThread(2000);
        
        Debug.DebugMessage("(5) - Populate Grid with Actors");
        
        grid.DisplayGridWithActors(actorManager.GetAllActorsAndPositionsOnGrid());
        
        Debug.DebugMessage("\n");
        Debug.DebugSleepThread(2000);
        
        Debug.DebugMessage("(6) - Instantiate Input Manager and Start Listening to key presses and bind Keypress to action");
        inputManager.OnActionTriggerd += action =>
        {
            actorManager.MovePlayer(action);
        };
        inputManager.StartListening();
        Debug.DebugMessage("\n");
        Debug.DebugSleepThread(2000);
        
        Debug.DebugMessage("(7) - Invoke Display grid method on GameTick ");
        gameTick.OnTick += UpdateGridOnTick;
        Debug.DebugSleepThread(2000);
        gameTick.Start();
        


    }

    private void UpdateGridOnTick()
    {
        Clear();
        grid.DisplayGridWithActors(actorManager.GetAllActorsAndPositionsOnGrid());
    }
    
}