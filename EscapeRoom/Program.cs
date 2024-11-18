namespace EscapeRoom;

class Program
{
    
    
    static void Main(string[] args)
    {

        
        Clear();
        GameManager gameManager = new GameManager();
        gameManager.InitialiseGame();
        gameManager.StartGameLoop();
    }
}