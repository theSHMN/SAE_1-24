    namespace MonsterCombatSim;

    public class Debug
    {
        public static bool IsDebugEnabled { get; set; } = false;
        
        
        public static void DebugMessage(string text)
        {
            if (IsDebugEnabled)
            {
                
                WriteWithColor("Debug: \n",ConsoleColor.Red);
                WriteWithColor($"{text} \n", ConsoleColor.Yellow);
                ResetColor();
            }
        }

        public static void DebugSleepThread(int milliSeconds)
        {
            if (IsDebugEnabled)
            {
                Thread.Sleep(milliSeconds);
            }
        }
        
        public static void DebugWrap(Action action)
        {
            if (IsDebugEnabled)
            {
                Write("Debug");
                action.Invoke();
            }
        }
    }