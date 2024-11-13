namespace VectorMath;

public class Input
{
    // public static float ParseInputToFloat(string prompt, int lowerBound = Int32.MinValue,
    //     int upperBoound = Int32.MaxValue, bool isScalar = false)
    // {
    //     float outputFloat;
    //     do
    //     {
    //         Write(prompt);
    //         string input = ReadLine();
    //         if (float.TryParse(input, out outputFloat))
    //         {
    //             if (outputFloat == 0 && isScalar)
    //             {
    //                 HelperFunctions.WriteWithColor($"Scalar cant be zero.", ConsoleColor.Red);
    //             }
    //             else if (outputFloat >= lowerBound && outputFloat <= upperBoound)
    //             {
    //                 HelperFunctions.WriteWithColor($"Valid!", ConsoleColor.Green);
    //                 break;
    //             }
    //             else
    //             {
    //                 HelperFunctions.WriteWithColor($"Input must be in the range of {lowerBound} and {upperBoound}!",
    //                     ConsoleColor.Red);
    //             }
    //         }
    //         else
    //         {
    //             HelperFunctions.WriteWithColor($"Invalid. Enter valid number (float).", ConsoleColor.Red);
    //         }
    //     } while (true);
    //
    //     return outputFloat;
    // }

    // must be value type, allow , convertible between different types
    public static T ParseInputToNumber<T>(string prompt, T lowerBound = default, T upperBound = default,
        bool isScalar = false)
        where T : struct, IComparable, IConvertible
    {
        T outputValue;
        do
        {
            Write(prompt);
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
                    HelperFunctions.WriteWithColor($"Input must be in the range of {lowerBound} to {upperBound}", ConsoleColor.Red);
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
        
    public static void InputCoordinates(ref Vector vector)
    {
        vector.X = ParseInputToNumber<float>("Enter |> X <| parameters Vector = ",-100f,100f,false);
        vector.Y = ParseInputToNumber<float>("Enter |> Y <| parameters Vector = ",-100f,100f,false);
        vector.Z = ParseInputToNumber<float>("Enter |> Z <| parameters Vector = ",-100f,100f,false);
    }
}