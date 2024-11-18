using System;
using DefaultNamespace;

namespace MonsterCombatSimulator.Monster
{

    public enum ActorType
    {
        Player,
        LowTierMonster,
        MidTierMonster,
        HighTierMonster,
        Boss,
        Neutral
    }

    public enum SpeedLevel
    {
        slow,
        medum,
        fast,
        buffed 
    }

    public abstract class Actor
    {
        public string Name { get; protected set; }
        public char Symbol { get; protected set; }
        public ConsoleColor Color { get; protected set; }
        public float HealthPoints { get; protected set; }
        public float AttackPoints { get; protected set; }
        public float DefensePoints { get; protected set; }
        public SpeedLevel Speed { get; protected set; }
        public ActorType Type { get; protected set; }
        private List<ActorType> ValidTargetTypes { get; set; }
        private int _lastAttackTime { get; set; }
        public bool IsColliding { get; protected internal set; }
        public bool IsInactive { get; protected internal set; }


        public Actor(string name, char symbol, ConsoleColor color, float healthPoints, float attackPoints,
            float defensePoints, SpeedLevel speed, ActorType type, List<ActorType> validTargetTypes)
        {
            Name = name;
            Symbol = symbol;
            Color = color;
            HealthPoints = healthPoints;
            AttackPoints = attackPoints;
            DefensePoints = defensePoints;
            Speed = speed;
            Type = type;
            ValidTargetTypes = AssignValidTargetTypes(); 
            _lastAttackTime = 0;
            IsColliding = false;
            IsInactive = false;
        }

        public float SpeedLevelMultiplier()
        {
            return Speed switch
            {
                SpeedLevel.slow => 0.25f,
                SpeedLevel.medum => 1f,
                SpeedLevel.fast => 1.5f,
                SpeedLevel.buffed => 3f,
                _ => 1f
            };
        }

        public List<ActorType> AssignValidTargetTypes()
        {
            return Type switch
            {
                ActorType.Player => new List<ActorType>
                    { ActorType.LowTierMonster, ActorType.MidTierMonster, ActorType.HighTierMonster, ActorType.Boss },
                ActorType.LowTierMonster => new List<ActorType> { ActorType.Player },
                ActorType.MidTierMonster => new List<ActorType> { ActorType.Player },
                ActorType.HighTierMonster => new List<ActorType> { ActorType.Player },
                ActorType.Boss => new List<ActorType> { ActorType.Player },
                ActorType.Neutral => new List<ActorType>(),
                _ => new List<ActorType>()
            };

        }

    public virtual void StartInteraction(Actor targetActor)
        {
            WriteLine($"{Name} interacts with {targetActor.Name}");
        }
        
        public virtual void Attack(List<Actor> targetActors)
        {
            foreach (var targetActor in targetActors)
            {
                if (targetActor.IsInactive || !CanAttackActorType(targetActor)) continue;

                float damage = Math.Max(0, AttackPoints - targetActor.DefensePoints);
                targetActor.HealthPoints -= damage;

                //WriteLine($"{Name} attacks {targetActor.Name} for {damage}");

                if (targetActor.HealthPoints <= 0)
                {
                    targetActor.IsInactive = true;
                    //riteLine($"{targetActor.Name} has been defeated");
                }
            }
        }

        public bool CanAttackOnTick(int tickInterval)
        {
            float attackInterval = 1000f / SpeedLevelMultiplier();
            return (Environment.TickCount - _lastAttackTime) >= attackInterval;
        }

        public bool CanAttackActorType(Actor targetActor)
        {
            return ValidTargetTypes.Contains(targetActor.Type);
        }

        public void ResetAttackTimer() => _lastAttackTime = Environment.TickCount;
    }
}