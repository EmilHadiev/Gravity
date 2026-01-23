using UnityEngine;
using Zenject;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(PlayerAttacker))]
public class Player : MonoBehaviour, IPlayer
{
    [SerializeField] private PlayerAnimator _animator;

    private PlayerData _playerData;

    public PlayerData Data => _playerData;
    public IPlayerAnimator Animator => _animator;

    private void OnValidate()
    {
        _animator ??= GetComponent<PlayerAnimator>();
    }

    [Inject]
    private void Constructor(PlayerData playerData)
    {
        _playerData = playerData;
    }
}