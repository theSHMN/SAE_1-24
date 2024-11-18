namespace MonsterCombatSimulator.Input;

public class Input
{
    public static char DisplayMenu(Dictionary<char, string> menuItems)
    {
        Clear();
        WriteWithColor("Choose option: ", ConsoleColor.Yellow);

        foreach (var item in menuItems)
        {
            WriteWithColor($"{item.Key} - {item.Value}",ConsoleColor.DarkGray);
        }

        return GetInput(menuItems);
    }

    public static char GetInput(Dictionary<char, string> validatedKeys)
    {
        char key = '\0';
        bool isValidInput = false;

        while (!isValidInput)
        {
            ConsoleKeyInfo keyInfo = ReadKey(true);

            key = keyInfo.KeyChar;

            if (validatedKeys.ContainsKey(key))
            {
                isValidInput = true;
            }
            else
            {
                WriteWithColor("Invalid choice!", ConsoleColor.Red);
            }

        }

        return key;

    }
}