public class EnemyRunState : IEnemyState
{
    private readonly IEnemyMovable _mover;

    public EnemyRunState(IEnemyMovable movable)
    {
        _mover = movable;
    }

    public void Enter() => _mover.StartMove();

    public void Exit() => _mover.StopMove();
}