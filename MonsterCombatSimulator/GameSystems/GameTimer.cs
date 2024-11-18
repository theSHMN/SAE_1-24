using System.Diagnostics;

namespace MonsterCombatSimulator;

public class GameTimer
{
    private Stopwatch _stopwatch;
    private bool _isRunning;
    private Thread? _updateThread;
    public event Action? OnTick;
    
    public int TickInterval { get; }
    public GameTimer(int tickInterval)
    {
        _stopwatch = new Stopwatch();
        TickInterval = tickInterval;
    }

    public void StartRound()
    {
        if(_isRunning) return;
        
        _isRunning = true;
        _stopwatch.Start();

        _updateThread = new Thread(RunTimer);
        _updateThread.Start();
    }

    public void StopRound()
    {
        _isRunning = false;
        _stopwatch.Stop();
        _updateThread?.Join();
    }

    public void ResetRound()
    {
        _stopwatch.Reset();
        _isRunning = false;
    }

    public void RunTimer()
    {
        long lastTickTime = 0;

        while (_isRunning)
        {
            long elapsedMilliSeconds = _stopwatch.ElapsedMilliseconds;

            if (elapsedMilliSeconds - lastTickTime >= TickInterval)
            {
                lastTickTime = elapsedMilliSeconds;
                OnTick?.Invoke();
            }

            Thread.Sleep(10);
        }
    }
    
}