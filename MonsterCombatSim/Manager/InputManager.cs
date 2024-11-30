using MonsterCombatSim.Movement;

namespace MonsterCombatSim.Manager;

public class InputManager
{
    private List<ConsoleKey> _availableKeys;
    private Dictionary<ConsoleKey, MoveAction?> _keyActionBindings = new();
    private Thread _inputThread;
    private bool _isListening;

    private readonly Object _lock = new();

    public event Action<MoveAction> OnActionTriggerd;


    public InputManager()
    {
        _keyActionBindings = new Dictionary<ConsoleKey, MoveAction?>();
        BindKeyToAction();
    }

    private void BindKeyToAction()
    {
        _keyActionBindings = new Dictionary<ConsoleKey, MoveAction?>
        {
            { ConsoleKey.D8, MoveAction.Up },
            { ConsoleKey.D9, MoveAction.UpRight },
            { ConsoleKey.D6, MoveAction.Right },
            { ConsoleKey.D3, MoveAction.DownRight },
            { ConsoleKey.D2, MoveAction.Down },
            { ConsoleKey.D1, MoveAction.DownLeft },
            { ConsoleKey.D4, MoveAction.Left },
            { ConsoleKey.D7, MoveAction.UpLeft },
            
            { ConsoleKey.UpArrow, MoveAction.Up },
            { ConsoleKey.RightArrow, MoveAction.Right },
            { ConsoleKey.DownArrow, MoveAction.Down },
            { ConsoleKey.LeftArrow, MoveAction.Left },

            
            { ConsoleKey.Enter, MoveAction.Confirm }
        };
    }

    public void StartListening()
    {
        if (!_isListening)
        {
            _isListening = true;
            _inputThread = new Thread(ListenForKeyPresses);
            _inputThread.Start();
        }
    }

    public void StopListening()
    {
        lock (_lock)
        {
            _isListening = false;
        }

        _inputThread?.Join();
    }

    private void ListenForKeyPresses()
    {
        while (true)
        {
            lock (_lock)
            {
                if (!_isListening) break;
            }

            if (KeyAvailable)
            {
                ConsoleKey key = ReadKey(true).Key;

                lock (_lock)
                {
                    if (_keyActionBindings.ContainsKey(key))
                    {
                        MoveAction? action = _keyActionBindings[key];
                        if (action.HasValue)
                        {
                            Debug.DebugMessage($"Key: {key} pressed. Action triggerd: {action.Value}.");
                            OnActionTriggerd?.Invoke(action.Value);
                        }
                        else
                        {
                            Debug.DebugMessage($"Key: {key} pressed. No action assigned!");
                        }
                        
                    }
                }
            }
        }
    }
}