using UnityEngine;
using Zenject;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(PlayerAttacker))]
[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(PlayerDieLogic))]
[RequireComponent(typeof(PlayerJumper))]
[RequireComponent(typeof(ClothCreator))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour, IPlayer
{
    [SerializeField] private PlayerAnimator _animator;
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private PathFollower _pathFollower;

    private PlayerData _playerData;

    public PlayerData Data => _playerData;
    public IPlayerAnimator Animator => _animator;
    public IMovable Mover => _playerMover;

    public IPathFollower Follower => _pathFollower;

    private void OnValidate()
    {
        _animator ??= GetComponent<PlayerAnimator>();
        _playerMover ??= GetComponent<PlayerMover>();
        _pathFollower ??= GetComponentInChildren<PathFollower>();
    }

    [Inject]
    private void Constructor(PlayerData playerData)
    {
        _playerData = playerData;
    }
}