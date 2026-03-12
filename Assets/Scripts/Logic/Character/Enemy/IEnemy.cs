public interface IEnemy
{
    EnemyData Data { get; }
    IHealth Health { get; }
    IEnemyAnimator Animator { get; }
    IEnemyStateMachine StateMachine { get; }
}