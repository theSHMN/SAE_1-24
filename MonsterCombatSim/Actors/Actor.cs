namespace MonsterCombatSim;

    public enum ActorType
    {
        Player,
        Monster,
        Undead,
        Boss,
        NPC,
        ItemType
    }

    public enum ActorState
    {
        Inactive,
        Armed,
        Disarmed,
        Frozen,
        Haste,
        Dead,
        ItemState
    }
    public enum SpeedLevel
    {
        None,
        Slow,
        Medium,
        Fast,
        Buffed
    }

    public class Actor
    {
        public string Name { get; protected set; }
        public char Symbol { get; protected set; }
        public ConsoleColor Color { get; protected internal set; }

        public float HealthPoints { get; protected internal set; }
        public float AttackPoints { get; protected set; }
        public float DefensePoints { get; protected internal set; }

        public ActorType Type { get; set; }

        private ActorState _state;

        public ActorState State
        {
            get => _state;
            set
            {
                _state = value;
                UpdateSymbolOnStateChange();
            }
        }

        public SpeedLevel Speed { get; set; }
        private List<ActorType>? _validTargetTypes { get; set; }
        private int _lastAttackTime { get; set; }
        public bool IsColliding { get; protected internal set; }

        private void UpdateSymbolOnStateChange()
        {
            Symbol = _state switch
            {
                ActorState.Armed => Symbol,
                ActorState.Disarmed => '?',
                ActorState.Frozen => '-',
                ActorState.Haste => '!',
                ActorState.Dead => '+',
                ActorState.ItemState => '=',
                _ => ' '
            };
        }

        public Actor(
            string name = "Default Actor", 
            char symbol = 'X', 
            ConsoleColor color = ConsoleColor.Cyan, 
            float healthPoints = 100f, 
            float attackPoints = 20f,
            float defensePoints = 10f, 
            ActorType type = ActorType.NPC, 
            ActorState state = ActorState.Armed,
            SpeedLevel speed = SpeedLevel.Slow,
            List<ActorType>? validTargetTypes = null)
        {
            Name = name;
            Symbol = symbol;
            Color = color;
            HealthPoints = healthPoints;
            AttackPoints = attackPoints;
            DefensePoints = defensePoints;
            Type = type;
            State = state;
            Speed = speed;
            _validTargetTypes = validTargetTypes;
            IsColliding = false;


        }

        public float SpeedLevelMultiplier()
        {
            return Speed switch
            {
                SpeedLevel.Slow => 0.25f,
                SpeedLevel.Medium => 1f,
                SpeedLevel.Fast => 1.5f,
                SpeedLevel.Buffed => 5f,
                _ => 1f
            };
        }

        public List<ActorType> AssignValidTargetTypes()
        {
            return Type switch
            {
                ActorType.Player => new List<ActorType>
                    { ActorType.Monster, ActorType.Undead, ActorType.Boss, ActorType.ItemType },
                ActorType.Monster => new List<ActorType> { ActorType.Player },
                ActorType.Undead => new List<ActorType> { ActorType.Player },
                ActorType.Boss => new List<ActorType> { ActorType.Player },
                ActorType.NPC => new List<ActorType>(),
                _ => new List<ActorType>()
            };
        }

        public virtual void Attack(List<Actor> targetActors)
        {
            IEnumerable<Actor> targetsToAttack;

            // Attack only 1 actor if Actortype.Player
            if (Type == ActorType.Player)
            {
                targetsToAttack = targetActors.Take(1).Where(t => !(t._state == ActorState.Inactive) && CanAttackActorType(t));
            }
            else
            {
                targetsToAttack = targetActors.Where(t => !(t._state == ActorState.ItemState) && CanAttackActorType(t));
            }

            // Perform attack on determined targets
            foreach (var targetActor in targetsToAttack)
            {
                float damage = Math.Max(0, AttackPoints - targetActor.DefensePoints);
                targetActor.HealthPoints -= damage;

                WriteWithColor($"{Name} attacks {targetActor.Name} for {damage} damage!", ConsoleColor.Red);

                if (targetActor.HealthPoints <= 0)
                {
                    targetActor.State = ActorState.Inactive;
                    WriteWithColor($"{targetActor.Name} has been defeated!", ConsoleColor.DarkRed);
                }

                // For Player, exit after attacking one target
                if (Type == ActorType.Player) break;
            }
        }
        
        public bool CanAttackOnTick(int tickInterval)
        {
            float attackInterval = 1000f / SpeedLevelMultiplier();
            return (Environment.TickCount - _lastAttackTime) >= attackInterval;
        }

        public bool CanAttackActorType(Actor targetActor)
        {
            return _validTargetTypes.Contains(targetActor.Type);
        }

        public void ResetAttackTimer() => _lastAttackTime = Environment.TickCount;

    }



    