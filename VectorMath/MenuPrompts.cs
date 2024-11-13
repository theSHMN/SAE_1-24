namespace VectorMath;

public class MenuPrompts
{
    public void PrintMenu()
    { 
        Clear();
        
        WriteLine($@"{ShowTitel()}

Selection overview:

(1) - Randomized Vectors:
    - Create two random vectors within set bounds (lower and upper)
    - Run and display all possible calculation

(2) - Create Vector1 for single-vector operation:
    - Multiplication (with scalar)    
    - Length
    - Square Magnitude

(3) - Create two vectors (Vector1 and Vector2) for vector math:
    - Add both vectors
    - Subtract both vectors
    - Distance between vectors (static method)
    - Distance between vectors (non-static method)

(4) - Quit

ENTER to proceed...
");
    }
    
   
    public void TitelPrompt()
    {
        HelperFunctions.WriteWithColor(ShowTitel(), ConsoleColor.Red);
        WriteLine("ENTER to proceed... ");
        ReadKey();
    }

    public string ShowTitel()
    {
        return "[Vector Math App]";
    }







}