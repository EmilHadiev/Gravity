public interface IEnemy
{
    EnemyData Data { get; }
    IEnemyAnimator Animator { get; }
}