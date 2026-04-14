using System;
using UnityEngine;
using Zenject;

public class EnemyMover : MonoBehaviour, IEnemyMovable
{
    [Inject] private readonly IPlayerSpawner _playerSpawner;

    private IEnemyAnimator _animator;
    private EnemyData _data;
    private IMovePattern _movePattern;

    public Transform Transform => transform;
    public float MoveSpeed => _data.MoveSpeed;
    public float RotateSpeed => default;

    private void OnEnable()
    {
        _playerSpawner.PlayerSpawned += OnPlayerSpawned;
    }

    private void OnDisable()
    {
        _playerSpawner.PlayerSpawned -= OnPlayerSpawned;
    }

    private void OnPlayerSpawned(IPlayer player)
    {
        Debug.Log("Игрок заспавнен!");
    }

    private void Start()
    {
        IEnemy enemy = GetComponent<Enemy>();
        _animator = enemy.Animator;
        _data = enemy.Data;

        IPlayer player = _playerSpawner.Player;

        _movePattern = new EnemyMovePattern(this, player.Mover.Transform, _animator);
        enemy.StateMachine.SwitchState<EnemyRunState>();
    }

    private void Update()
    {
        _movePattern?.Update();
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