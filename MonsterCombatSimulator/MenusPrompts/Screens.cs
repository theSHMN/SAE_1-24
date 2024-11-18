namespace MonsterCombatSimulator.ScreensPrompts;

public class Screens
{
    public static void TitleText(int lengthInMs, int colorChangeInMS)
    {
        ConsoleColor[] colors =
        {
            ConsoleColor.Red, ConsoleColor.Yellow, ConsoleColor.DarkRed, ConsoleColor.DarkYellow, ConsoleColor.DarkGray
        };
        int colorIndex = 0;
        int iterations = lengthInMs / colorChangeInMS;

        for (int i = 0; i < iterations; i++)
        {
            Clear();
            WriteWithColor(@"
  ▄▄▄▄   ▓█████  ███▄    █ ▓█████ ▄▄▄     ▄▄▄█████▓ ██░ ██ 
  ▓█████▄ ▓█   ▀  ██ ▀█   █ ▓█   ▀▒████▄   ▓  ██▒ ▓▒▓██░ ██▒
  ▒██▒ ▄██▒███   ▓██  ▀█ ██▒▒███  ▒██  ▀█▄ ▒ ▓██░ ▒░▒██▀▀██░
  ▒██░█▀  ▒▓█  ▄ ▓██▒  ▐▌██▒▒▓█  ▄░██▄▄▄▄██░ ▓██▓ ░ ░▓█ ░██ 
  ░▓█  ▀█▓░▒████▒▒██░   ▓██░░▒████▒▓█   ▓██▒ ▒██▒ ░ ░▓█▒░██▓
  ░▒▓███▀▒░░ ▒░ ░░ ▒░   ▒ ▒ ░░ ▒░ ░▒▒   ▓▒█░ ▒ ░░    ▒ ░░▒░▒
  ▒░▒   ░  ░ ░  ░░ ░░   ░ ▒░ ░ ░  ░ ▒   ▒▒ ░   ░     ▒ ░▒░ ░
   ░    ░    ░      ░   ░ ░    ░    ░   ▒    ░       ░  ░░ ░
   ░         ░  ░         ░    ░  ░     ░  ░         ░  ░  ░
                 ░                                                   
", colors[colorIndex]);

            colorIndex = (colorIndex + 1) % colors.Length;
            Thread.Sleep(colorChangeInMS);
        }
    }
}