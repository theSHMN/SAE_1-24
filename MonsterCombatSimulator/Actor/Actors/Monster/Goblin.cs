namespace MonsterCombatSimulator.Monster.Monster;

public class Goblin : Actor
{
    public Goblin(string name, char symbol, ConsoleColor color, float healthPoints, float attackPoints, float defensePoints, SpeedLevel speed, ActorType type, List<ActorType> validTargetTypes) : base(name, symbol, color, healthPoints, attackPoints, defensePoints, speed, type,validTargetTypes)
    {
    }

   

   
}