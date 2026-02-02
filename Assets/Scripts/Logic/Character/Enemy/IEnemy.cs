public interface IEnemy
{
    EnemyData Data { get; }
    IEnemyAnimator Animator { get; }
    IEnemyStateMachine StateMachine { get; }
}