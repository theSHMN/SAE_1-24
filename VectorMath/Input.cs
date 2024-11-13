namespace VectorMath;

public class Input
{
    Operations _operations = new Operations();
    public bool isQuit = false;
    
    // needs to be value type, allow comparison to bounds or scalar, must be convertible to other types 
    public static T ParseInputToNumber<T>(string prompt, T lowerBound = default, T upperBound = default,
        bool isScalar = false)
        where T : struct, IComparable, IConvertible
    {
        T outputValue;
        do
        {
            Clear();
            WriteLine(prompt);
            WriteLine();
            Write("Your input: ");
            string input = ReadLine();

            if (TryParse(input, out outputValue))
            {
                // convert 0 to T
                if (outputValue.CompareTo(Convert.ChangeType(0, typeof(T))) == 0 && isScalar)
                {
                    HelperFunctions.WriteWithColor($"Scalar can not be zero.", ConsoleColor.Red);
                }
                else if (outputValue.CompareTo(lowerBound) >= 0 && outputValue.CompareTo(upperBound) <= 0)
                {
                    HelperFunctions.WriteWithColor($"Valid!", ConsoleColor.Green);
                    break;
                }
                else
                {
                    HelperFunctions.WriteWithColor($"Input must be in the range of {lowerBound} to {upperBound}",
                        ConsoleColor.Red);
                }
            }
            else
            {
                HelperFunctions.WriteWithColor("Invalid. Enter valid number.", ConsoleColor.Red);
            }
        } while (true);

        return outputValue;
    }

    // return true if input converted to T 
    private static bool TryParse<T>(string input, out T result) where T : IConvertible
    {
        try
        {
            result = (T)Convert.ChangeType(input, typeof(T));
            return true;
        }
        catch
        {
            result = default;
            return false;
        }
    }

   
    
    public void SelectMenuItemWithInput()
    {
        
        int index = ParseInputToNumber<int>(@"[Vector Math App]

(1) - Randomize and run calculations on both vectors.
(2) - Create 1 vector and run all viable calculations.
(3) - Create 2 Vectors and run all viable calculations.
(4) - Quit", 1, 4);
        WriteLine();


        switch (index)
        {
            case 1:
                WriteLine("(1) - Randomize and run calculations on both vectors");
                _operations.CalculateRandomVectors();
                break;
            case 2:
                WriteLine("(2) - Create 1 vector and run all viable calculations.");
                _operations.CalculateSingleVector();
                break;
            case 3:
                WriteLine("(3) - Create 2 Vectors and run all viable calculations.");
                _operations.CalculateTwoVectors();
                break;
            case 4:
                WriteLine("(4) - Quit");
                isQuit = true;
                break;
        }
    }

    public static Vector InputCoordinates(string name)
    {
            // ref to the vector instead of new instance
            return new Vector
            (
                name,
                ParseInputToNumber<float>($"{name}: Enter X - parameter = ", -100f, 100f, false),
                ParseInputToNumber<float>($"{name}: Enter Y - parameter = ", -100f, 100f, false),
                ParseInputToNumber<float>($"{name}: Enter Z - parameter = ", -100f, 100f, false)
            );
    }

    public static Vector RandomVector(int lowerBounds, int upperBounds, string name)
    {
        return new Vector
        (
            name,
            HelperFunctions.Randomizer(lowerBounds, upperBounds),
            HelperFunctions.Randomizer(lowerBounds, upperBounds),
            HelperFunctions.Randomizer(lowerBounds, upperBounds)
        );
    }
    
}