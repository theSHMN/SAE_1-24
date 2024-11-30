namespace MonsterCombatSim.Timer;

using System;
using System.Timers;

public class GameTick
{
        private readonly Timer _timer;

    
        public event Action OnTick;
        
        public GameTick()
        {
            _timer = new Timer(1000); // 1 second interval
            _timer.Elapsed += TimerElapsed;
            _timer.AutoReset = true; // Ensures the timer keeps ticking
        }
        
        public void Start()
        {
            if (!_timer.Enabled)
            {
                _timer.Start();
                WriteLine("Game timer started.");
            }
        }

        
        public void Stop()
        {
            if (_timer.Enabled)
            {
                _timer.Stop();
                WriteLine("Game timer stopped.");
            }
        }
        
        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            OnTick?.Invoke(); // Raise the OnTick event
        }
    
}