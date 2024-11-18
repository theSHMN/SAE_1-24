using EscapeRoom.Actors;
using EscapeRoom.Actors.MiniGames;
using EscapeRoom.Actors.Player;
using EscapeRoom.Actors.Trap;

namespace EscapeRoom
{
    public class GameManager
    {
        private ActorManager _actorManager;
        private GridManager _gridManager;
        private RoundManager _roundManager;
        private Grid _grid;
        private Player _player;
        private List<Actor> _actors;
        private bool _doorAdded = false;

        public GameManager()
        {
        }

        public void InitialiseGame()
        {
            var options = new Dictionary<int, (string, ConsoleColor, int)>
            {
                { 1, ("5x5", ConsoleColor.Gray,5) },
                { 2, ("7x7", ConsoleColor.DarkGreen,7) },
                { 3, ("10x10", ConsoleColor.DarkCyan,10) }
            };

            int gridSize = Input.MenuSelection<int>($"Choose a grid size", options);
            
            WriteLine($"Grid size selected: {gridSize}x{gridSize}");
            
            _player = new Player("Player", 'X', ConsoleColor.Green);
            _grid = new Grid(gridSize, gridSize);
            _actorManager = new ActorManager(_grid);
            _gridManager = new GridManager(_actorManager, _grid);
            _roundManager = new RoundManager();
            
            _actorManager.AddActor(_player);

            int mealstormSpawnCount = gridSize switch
            {
                5 => 2,
                7 => 3,
                10 => 6,
                _ => 2
            };
            
            InstantiateActors(mealstormSpawnCount);
        }

        public void InstantiateActors(int mealstormSpawnCount)
        {
            _actors = new List<Actor>()
            {
                new Hangman("Hangman Game", 'H', ConsoleColor.Cyan),
                new NumberGuessingGame("Number Guessing Game", 'N', ConsoleColor.Cyan),
                new RockPaperScissors("Rock Paper Scissors Game", 'R', ConsoleColor.Cyan),
                new WordScramble("Word Scramble", 'W', ConsoleColor.Cyan),
            };
            
            foreach (var actor in _actors)
            {
                _actorManager.AddActor(actor);
            }
            
            for (int i = 0; i < mealstormSpawnCount; i++)
            {
                var mealstorm = new Mealstorm($"Mealstorm{i +1}", 'M', ConsoleColor.Red, _actorManager);
                _actors.Add(mealstorm);
                _actorManager.AddActor(mealstorm);
            }
            
        }


        public void StartGameLoop()
        {
            Grid.GridPosition targetPosition = _actorManager.GetActorPosition(_player) ?? new Grid.GridPosition(0, 0);

            while (true)
            {
                _grid.DisplayGrid(_actorManager.GetActorPositions(), targetPosition);
                PrintStatsOfActors(_actorManager);
                _roundManager.DisplayCurrentRound();


                bool moveConfirmed = Input.DirectionalInput(_player, _actorManager, _grid, ref targetPosition);
                WriteLine();
                Clear();

                if (moveConfirmed)
                {
                    _actorManager.UpdateActorPosition(_player, targetPosition);
                    MoveMealstormsRandomly();

                    List<Actor> adjacentActors = _gridManager.GetAdjacentActors();
                    if (adjacentActors.Count > 0)
                    {
                        Actor currentAdjacentActor = adjacentActors[0];
                        bool wantsToInteract = Input.InteractionInquiry(currentAdjacentActor);

                        if (wantsToInteract)
                        {
                            currentAdjacentActor.Behaviour();
                        }
                    }

                    _roundManager.IncrementRoundCounter();
                }

                WinCondition();
            }
        }


        private void MoveMealstormsRandomly()
        {
            foreach (var mealstorm in _actors.OfType<Mealstorm>())
            {
                _actorManager.MoveActorRandomly(mealstorm.Name,3);
                
            }
        }

        public void WinCondition()
        {
            // LINGQ method to filter ofType 
            bool allGamesComplete = _actors.OfType<Hangman>().Any(hangman => hangman.IsComplete) &&
                                    _actors.OfType<NumberGuessingGame>()
                                        .Any(numberGuessingGame => numberGuessingGame.IsComplete) &&
                                    _actors.OfType<RockPaperScissors>()
                                        .Any(rockPaperScissors => rockPaperScissors.IsComplete) &&
                                    _actors.OfType<WordScramble>().Any(wordScramble => wordScramble.IsComplete);

            if (allGamesComplete && !_doorAdded)
            {
                Door door = new Door("Door", 'D', ConsoleColor.DarkRed);
                _actorManager.AddActor(door);
                _doorAdded = true;
            }
        }

    }
}