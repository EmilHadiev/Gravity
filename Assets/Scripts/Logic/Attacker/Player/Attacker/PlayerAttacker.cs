using UnityEngine;
using Zenject;

public class PlayerAttacker : MonoBehaviour, ISwordSwitcher
{
    [SerializeField] private SwordSpawnPoint _swordPlace;

    [Inject] private readonly IInput _mobileInput;

    private IFactory _factory;
    private IPlayerSoundContainer _playerSound;
    private SwordData[] _swordsData;
    private PlayerData _data;
    private PlayerAttackLogic _attackLogic;
    private IPlayerAnimator _animator;
    private void Awake()
    {
        var player = GetComponent<IPlayer>();
        _data = player.Data;
        _animator = player.Animator;
        _attackLogic = new PlayerAttackLogic(transform, _swordPlace, _factory, _playerSound, _data, _swordsData);
    }

    private void OnEnable()
    {
        _mobileInput.Attacked += Attack;
    }

    private void OnDisable()
    {
        _mobileInput.Attacked -= Attack;
    }

    [Inject]
    private void Constructor(IFactory factory, IPlayerSoundContainer playerSoundContainer, SwordData[] swords)
    {
        _factory = factory;
        _playerSound = playerSoundContainer;
        _swordsData = swords;
    }

    private void Attack()
    {
        _animator.StopRunning();
        _animator.Attack();
    }

    private void AttackStarted()
    {
        _attackLogic.Attack();
    }

    private void AttackEnded()
    {
        _attackLogic.StopAttack();
    }

    public void Switch(AssetProvider.Swords newSword)
    {
        _attackLogic.SwitchSword(newSword);
    }
}