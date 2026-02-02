using System;
using System.Collections.Generic;

public class EnemyStateMachine : IEnemyStateMachine
{
    private readonly Dictionary<Type, IEnemyState> _states;

    private IEnemyState _currentState;

    public EnemyStateMachine(IAttackable attacker, IEnemyMovable mover)
    {
        _states = new Dictionary<Type, IEnemyState>();
        _states.Add(typeof(EnemyAttackState), new EnemyAttackState(attacker));
        _states.Add(typeof(EnemyRunState), new EnemyRunState(mover));
    }

    public void SwitchState<T>() where T : IEnemyState
    {
        if (_states.TryGetValue(typeof(T), out IEnemyState value))
        {
            _currentState?.Exit();

            _currentState = value;
            _currentState.Enter();
        }
        else
        {
            throw new ArgumentException(nameof(T));
        }
    }
}