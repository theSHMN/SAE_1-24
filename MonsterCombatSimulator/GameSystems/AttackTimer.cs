using DefaultNamespace;
using MonsterCombatSimulator.Monster;

namespace MonsterCombatSimulator;

public class AttackTimer
{
    private ActorManager _actorManager;
    private Timer _timer;
    private int _tickInterval;

    public AttackTimer(ActorManager actorManager, int tickInterval = 10)
    {
        _actorManager = actorManager;
        _tickInterval = tickInterval;
    }

    public void Start()
    {
        Stop();
        _timer = new Timer(OnTick, null, 0, _tickInterval);
    }

    public void Stop()
    {
        _timer?.Dispose();
        _timer = null;
    }

    private void OnTick(object state)
    {
        var processedActors = new HashSet<Actor>(); // Track already processed actors to avoid redundant attacks.

        foreach (var actor in _actorManager.GetAllActors())
        {
            if (actor.IsInactive || processedActors.Contains(actor)) continue;

            var adjacentActors = _actorManager.AdjacentActors(actor)
                .Where(target => !target.IsInactive)
                .ToList();

            if (adjacentActors.Any() && actor.CanAttackOnTick(_tickInterval))
            {
                actor.Attack(adjacentActors); // Attack all adjacent actors.
                actor.ResetAttackTimer();

                // Add all attacked actors to the processed list.
                foreach (var target in adjacentActors)
                {
                    processedActors.Add(target);
                    // Allow retaliation from the target, if possible.
                    if (target.CanAttackOnTick(_tickInterval))
                    {
                        target.Attack(new List<Actor> { actor });
                        target.ResetAttackTimer();
                        processedActors.Add(actor);
                    }
                }
            }

            // Mark the current actor as processed.
            processedActors.Add(actor);
        }
    }
    // private void OnTick(object state)
    // {
    //     foreach (var actor in _actorManager.GetAllActors())
    //     {
    //         if (actor.IsInactive) continue;
    //
    //         var adjacentActors = _actorManager.AdjacentActors(actor);
    //
    //         foreach (var target in adjacentActors)
    //         {
    //             if (actor.CanAttackOnTick(_tickInterval))
    //             {
    //                 WriteLine($"{actor.Name} attacks {target.Name}");
    //                 actor.Attack(target);
    //                 
    //                 actor.ResetAttackTimer();
    //             }
    //
    //             if (target.CanAttackOnTick(_tickInterval))
    //             {
    //                 WriteLine($"{target.Name} attacks {actor.Name}");
    //                 target.Attack(actor);
    //                 
    //                 target.ResetAttackTimer();
    //             }
    //         }
    //     }
    // }
}