namespace VectorMath;

class Program
{
    public static float scalar;
    public static bool inputIsNotZero;

    static void Main()
    {
        
        //initialize vector 
        Vector v1 = new Vector("Vector1");
        Vector v2 = new Vector("Vector2");

        HelperFunctions.DisplayVectors(v1);
        HelperFunctions.DisplayVectors(v2);

        Input.InputCoordinates(ref v1);
        Input.InputCoordinates(ref v2);
        float scalar = Input.ParseInputToNumber("Enter a non-zero value as a scalar for multiplication.",-100f,100f,isScalar:true);
        
        
        // Input X Y Z for V1 and V2
        // Input scalar
        // Input Method
        // add method to program and return vector

        // Validate Input value


        Vector vectorSum = v1 + v2;
        HelperFunctions.WriteWithColor(HelperFunctions.DisplayVectors(vectorSum), ConsoleColor.Magenta);

        Vector vectorDifference = v1 - v2;
        HelperFunctions.WriteWithColor(HelperFunctions.DisplayVectors(vectorDifference), ConsoleColor.Magenta);

        Vector vectorResult = v1 * scalar;
        HelperFunctions.WriteWithColor(HelperFunctions.DisplayVectors(vectorResult), ConsoleColor.Magenta);

        // Static
        Write($"Distance of {v1.Name} and {v2.Name} (static method): ");
        HelperFunctions.WriteWithColor(Vector.Distance(v1, v2).ToString(), ConsoleColor.Magenta);
        //WriteLine(Vector.Distance(v1, v2));
        // Non-static
        Write($"Distance of {v1.Name} and {v2.Name} (non-static method): ");
        HelperFunctions.WriteWithColor(v1.Distance(v2).ToString(), ConsoleColor.Magenta);
        //WriteLine($"{v1.Distance(v2)}");

        HelperFunctions.WriteWithColor($"Length of '{v1.Name}' = {v1.Length()}", ConsoleColor.Magenta);

        HelperFunctions.WriteWithColor($"Square magnitude of '{v1.Name}' = {v1.SquareMagnitude()}",
            ConsoleColor.Magenta);
    }
}