using MonsterCombatSim.Manager;

namespace MonsterCombatSim;


// The Grid is being instantiated with a certain size and is then passed to the GridManager
// which uses the dimensions to create a randomized list of all possible GridPosition 

// The ActorManager adds every Actor to a list and has a method that take two lists,
// one for actors and one for GridPositions and combines it to a dictionary

//


class Program
{
    static void Main(string[] args)
    {

        GameManager gameManager = new GameManager();
        gameManager.RunGame();


    }
}