namespace VectorMath;

public class HelperFunctions
{
    public static float Randomizer(int lowerBound, int upperBound)
    {
        Random random = new Random();
        // Need to scale because .NextDouble returns value between 0.0 . 1.0
        float randomFloat = (float)(random.NextDouble() * (upperBound - lowerBound) + lowerBound);
        return MathF.Round(randomFloat,2);
    } 

    public static string DisplayVectors(Vector vector)
    {
        return $"Vector {vector.Name}: X = {vector.X}: _y = {vector._y}: _z = {vector._z}";
    }
    
    public static void WriteWithColor(string text, ConsoleColor color)
    {
        ForegroundColor = color;
        WriteLine(text);
        ResetColor();
    }
}