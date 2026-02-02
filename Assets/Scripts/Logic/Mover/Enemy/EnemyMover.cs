using UnityEngine;

public class EnemyMover : MonoBehaviour, IEnemyMovable
{
    private IEnemyAnimator _animator;
    private EnemyData _data;
    private IMovePattern _movePattern;

    private void Start()
    {
        IEnemy enemy = GetComponent<Enemy>();
        _animator = enemy.Animator;
        _data = enemy.Data;

        Player player = GameObject.FindObjectOfType<Player>();

        _movePattern = new EnemyMovePattern(this, player.transform, _animator);
        enemy.StateMachine.SwitchState<EnemyRunState>();
    }

    public Transform Transform => transform;

    public float MoveSpeed => _data.MoveSpeed;

    public float RotateSpeed => default;

    private void Update()
    {
        _movePattern.Update();
    }

    public void StartMove()
    {
        _movePattern.Start();
    }

    public void StopMove()
    {
        _movePattern.Stop();
    }
}