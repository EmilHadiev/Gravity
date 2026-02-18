using UnityEngine;
using Zenject;

public class PlayerMover : MonoBehaviour, IMovable
{
    [Inject] private readonly IInput _input;

    private IMovePattern _movePattern;
    private IPlayerAnimator _animator;
    private PlayerData _playerData;

    public Transform Transform => transform;

    public float MoveSpeed => _playerData.MoveSpeed;
    public float RotateSpeed => _playerData.RotateSpeed;

    private void Awake()
    {
        IPlayer player = GetComponent<IPlayer>();

        _playerData = player.Data;
        _animator = player.Animator;

        _movePattern = new PlayerMovePattern(this, _animator, _input);
    }

    private void OnEnable()
    {
        _movePattern.Start();
    }

    private void OnDisable()
    {
        _movePattern.Stop();
    }

    private void Update()
    {
        _movePattern.Update();
    }
}