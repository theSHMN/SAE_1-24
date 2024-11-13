namespace VectorMath;

public class Operations
{
    float scalar;

    

    public void CalculateRandomVectors()
    {
        Vector randomVector1 = Input.RandomVector(Program.lowerBounds, Program.upperBounds, "Random-Vector 1");
        Vector randomVector2 = Input.RandomVector(Program.lowerBounds, Program.upperBounds, "Random-Vector 2");
        scalar = HelperFunctions.Randomizer(Program.lowerBounds, Program.upperBounds);

        CallOperations(randomVector1, randomVector2);
    }

    public void CalculateSingleVector()
    {
        Vector singleVector = Input.InputCoordinates("Vector 1");
        scalar = Input.ParseInputToNumber
        ("Enter a non-zero value as a scalar for multiplication.", Program.scalarLowerBounds, Program.scalarUpperBounds, isScalar: true);
        CallOperations(singleVector);
    }

    public void CalculateTwoVectors()
    {
        Vector v1 = Input.InputCoordinates("Vector 1");
        Vector v2 = Input.InputCoordinates("Vector 2");
        scalar = Input.ParseInputToNumber("Enter a non-zero value as a scalar for multiplication.", Program.scalarLowerBounds, Program.scalarUpperBounds,
            isScalar: true);

        CallOperations(v1, v2);
    }

    private void CallOperations(Vector v1, Vector? v2 = null)
    {
        if (v2 != null)
        {
            HelperFunctions.WriteWithColor($"Add {v1.Name} and {v2.Value.Name}: ", ConsoleColor.Magenta);
            Vector vectorSum = v1 + v2.Value;
            HelperFunctions.WriteWithColor(HelperFunctions.DisplayVectors(vectorSum), ConsoleColor.Yellow);
            
            HelperFunctions.WriteWithColor($"Subtract {v1.Name} and {v2.Value.Name}: ", ConsoleColor.Magenta);
            Vector vectorDifference = v1 - v2.Value;
            HelperFunctions.WriteWithColor(HelperFunctions.DisplayVectors(vectorDifference), ConsoleColor.Yellow);
            
            HelperFunctions.WriteWithColor($"Distance (static method) between {v1.Name} and {v2.Value.Name}: ", ConsoleColor.Magenta);
            HelperFunctions.WriteWithColor(Vector.Distance(v1, v2.Value).ToString(), ConsoleColor.Yellow);
            
            HelperFunctions.WriteWithColor($"Distance (non-static method) between {v1.Name} and {v2.Value.Name}: ", ConsoleColor.Magenta);
            HelperFunctions.WriteWithColor(v1.Distance(v2.Value).ToString(), ConsoleColor.Yellow);
            
        }
        HelperFunctions.WriteWithColor($"Multiplication of {v1.Name} with scalar: ", ConsoleColor.Magenta);
        Vector vectorProduct = v1 * scalar;
        HelperFunctions.WriteWithColor(HelperFunctions.DisplayVectors(vectorProduct), ConsoleColor.Yellow);

        HelperFunctions.WriteWithColor($"Length of {v1.Name}: ", ConsoleColor.Magenta);
        HelperFunctions.WriteWithColor($"{v1.Length()}", ConsoleColor.Yellow);
        
        HelperFunctions.WriteWithColor($"Square magnitude of {v1.Name}: ", ConsoleColor.Magenta);
        HelperFunctions.WriteWithColor($"{v1.SquareMagnitude()}", ConsoleColor.Yellow);

        WriteLine();
        HelperFunctions.WriteWithColor("Press ENTER to return to selection...", ConsoleColor.DarkCyan);
        ReadLine();
    }
    
}