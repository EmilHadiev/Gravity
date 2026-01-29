using UnityEngine;
using Zenject;

public class EnemyMover : MonoBehaviour, IMovable
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
        _movePattern.Start();
    }

    public Transform Transform => transform;

    public float MoveSpeed => _data.MoveSpeed;

    public float RotateSpeed => _data.RotateSpeed;

    private void Update()
    {
        _movePattern.Update();
    }
}