namespace VectorMath
{
    class Program
    {
        public static readonly int lowerBounds = -100;
        public static readonly int upperBounds = 100;
        public static readonly float scalarLowerBounds = -100f;
        public static readonly float scalarUpperBounds = 100f;

        static void Main()
        {
            Input input = new Input();
            MenuPrompts menuPrompts = new MenuPrompts();


            Clear();
            menuPrompts.TitelPrompt();
            menuPrompts.PrintMenu();
            ReadLine();

            do
            {
                input.SelectMenuItemWithInput();
            } while (!input.isQuit);
            
            WriteLine();
            WriteLine("App has ended.");
           
        }
    }
}