using DefaultNamespace;
using MonsterCombatSimulator.Monster;
using MonsterCombatSimulator.Monster.Monster;
using MonsterCombatSimulator.Monster.Player;
using MonsterCombatSimulator.Movement;

namespace MonsterCombatSimulator.Manager;

public class GameManager
{
    private Grid _grid;
    private GameTimer _gameTimer;
    private AttackTimer _attackTimer;
    private ActorManager _actorManager;
    private ActorMovement _actorMovement;
    private Player _player;
    private bool _isPaused;
    

    public GameManager(int rows, int cols, int tickInterval)
    {
        _grid = new Grid(rows, cols);
        _gameTimer = new GameTimer(tickInterval);
        _actorManager = new ActorManager(_grid, _gameTimer);
        _actorMovement = new ActorMovement(_actorManager, _grid, _gameTimer);
        _attackTimer = new AttackTimer(_actorManager);
        
        InitializeActors();
    }

    public void InitializeActors()
    {
        _player = new Player("Hero",'X', ConsoleColor.Red, 1000f, 10f, 0f, SpeedLevel.fast, ActorType.Player, new List<ActorType>());
        _actorManager.AddActor(_player);

        Goblin goblin = new Goblin("Goblin",'G', ConsoleColor.Cyan, 300f, 10f, 0f, SpeedLevel.slow,ActorType.LowTierMonster,new List<ActorType>());
        _actorManager.AddActor(goblin);

        Skeleton skeleton = new Skeleton("Skeleton", 'S', ConsoleColor.Magenta, 100f, 10f, 0f,SpeedLevel.slow,ActorType.LowTierMonster,new List<ActorType>());
        _actorManager.AddActor(skeleton);
        
        Skeleton skeleton1 = new Skeleton("Skeleton", 'S', ConsoleColor.Magenta, 100f, 10f, 0f, SpeedLevel.slow,ActorType.LowTierMonster,new List<ActorType>());
        _actorManager.AddActor(skeleton1);
        
        Skeleton skeleton2 = new Skeleton("Skeleton", 'S', ConsoleColor.Magenta, 100f, 10f, 0f, SpeedLevel.slow,ActorType.LowTierMonster,new List<ActorType>());
        _actorManager.AddActor(skeleton2);
        
        Skeleton skeleton3 = new Skeleton("Skeleton", 'S', ConsoleColor.Magenta, 100f, 10f, 0f, SpeedLevel.slow,ActorType.LowTierMonster,new List<ActorType>());
        _actorManager.AddActor(skeleton3);
        
        
    }

    public void InitializeGame()
    {
        
        
        _gameTimer.OnTick += () =>
        {
            Clear();
            _actorManager.UpdateAdjacentActorsCollision();
            _grid.DisplayGrid(_actorManager.GetAllActorPositions());
            DisplayActorStats(_actorManager);
        };
        
        _attackTimer.Start();
        _gameTimer.StartRound();
        GameLoop();
    }

    private void GameLoop()
    {
        while (true)
        {
            if (_isPaused)
            {
                Thread.Sleep(100);
                if (KeyAvailable && ReadKey(true).Key == ConsoleKey.P)
                {
                    TooglePause();
                }
                continue;
            }
            
            if (KeyAvailable)
            {
                var key = ReadKey(true).Key;

                if (key == ConsoleKey.P)
                {
                    TooglePause();
                    continue;
                }
                
                _actorMovement.MovePlayer(_player, key);
                _actorManager.UpdateAdjacentActorsCollision();
                
                Clear();
                _grid.DisplayGrid(_actorManager.GetAllActorPositions());
                DisplayActorStats(_actorManager);
            }
            Thread.Sleep(100);
        }
    }

    public void PauseGame()
    {
        _attackTimer.Stop();
        _gameTimer.StopRound();
        _isPaused = true;
        //WriteLine("Game paused.");
    }

    public void ResumeGame()
    {
        _attackTimer.Start();
        _gameTimer.StartRound();
        _isPaused = false;
        //WriteLine("Game resumed.");
    }

    public void TooglePause()
    {
        if (_isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

   
}