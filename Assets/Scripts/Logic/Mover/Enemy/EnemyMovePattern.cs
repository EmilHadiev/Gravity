using UnityEngine;

public class EnemyMovePattern : IMovePattern
{
    private readonly IMovable _enemy;
    private readonly Transform _player;
    private readonly IEnemyAnimator _animator;

    private bool _isWorking;

    public EnemyMovePattern(IMovable movable, Transform player, IEnemyAnimator animator)
    {
        _enemy = movable;
        _player = player;
        _animator = animator;
    }

    public void Start()
    {
        _isWorking = true;
        _animator.StartRunning();
    }

    public void Stop()
    {
        _isWorking = false;
        _animator.StopRunning();
    }

    public void Update()
    {
        if (_isWorking == false)
            return;

        if (_player == null)
            return;

        Move();
        LookAt();
    }

    private void Move()
    {
        _enemy.Transform.position = Vector3.MoveTowards(_enemy.Transform.position, _player.position, _enemy.MoveSpeed * Time.deltaTime);
    }

    private void LookAt() => _enemy.Transform.LookAt(_player);
}