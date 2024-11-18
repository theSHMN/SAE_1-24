using System;
using DefaultNamespace;
using MonsterCombatSimulator.Manager;
using MonsterCombatSimulator.Monster;
using MonsterCombatSimulator.Monster.Monster;
using MonsterCombatSimulator.Monster.Player;
using MonsterCombatSimulator.Movement;
using MonsterCombatSimulator.ScreensPrompts;

namespace MonsterCombatSimulator;

class Program
{
    static void Main(string[] args)
    {
        Clear();
        GameManager gameManager = new GameManager(20, 20, 1000);
        
        //Screens.TitleText(5000,1000);
        
        gameManager.InitializeGame();
        
        
    }
}