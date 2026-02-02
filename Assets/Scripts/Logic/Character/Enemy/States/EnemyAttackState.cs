public class EnemyAttackState : IEnemyState
{
    private readonly IAttackable _attackable;

    public EnemyAttackState(IAttackable attackable)
    {
        _attackable = attackable;
    }

    public void Enter() => _attackable.Activate();

    public void Exit() => _attackable.Deactivate();
}