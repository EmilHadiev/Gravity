using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class PlayerAttackLogic
{
    private readonly IFactory _factory;
    private readonly IPlayerSoundContainer _playerSound;
    private readonly PlayerData _playerData;   
    private readonly Collider[] _hits;
    private readonly SwordData[] _swordsData;
    private readonly Transform _player;
    private readonly LayerMask _enemyMask;
    private readonly SwordSpawnPoint _swordPlace;

    private Sword _currentSword;
    private SwordData _swordData;

    public PlayerAttackLogic(Transform player, SwordSpawnPoint swordSpawn, IFactory factory, IPlayerSoundContainer playerSoundContainer, 
        PlayerData playerData, SwordData[] swords)
    {
        _factory = factory;
        _swordPlace = swordSpawn;
        _playerSound = playerSoundContainer;
        _swordsData = swords;
        _playerData = playerData;
        _swordData = GetSwordData();
        _enemyMask = LayerMask.GetMask(CustmomMasks.Enemy);
        _player = player;

        _hits = new Collider[_playerData.MaxAttackTargets];

        CreateSword(_playerData.Swords).Forget();
    }

    public void Attack()
    {
        ClearTargets();
        int targets = GetTargets();

        PhysicsDebug.DrawDebug(GetAttackPosition(), _playerData.AttackRadius, color: Color.blue);

        _currentSword.TrialToggle(true);

        if (targets == 0)
        {
            _playerSound.Play(AssetProvider.Sounds.AttackMiss.ToString());
            return;
        }

        _playerSound.PlayWhenFree(AssetProvider.Sounds.Attack.ToString());

        for (int i = 0; i < targets; i++)
            AttackTarget(_hits[i]);
    }

    public void StopAttack()
    {
        _currentSword.TrialToggle(false);
    }

    public void SwitchSword(AssetProvider.Swords newSword)
    {
        _factory.ReleaseAsset(_playerData.Swords.ToString());
        GameObject.Destroy(_currentSword.gameObject);

        _playerData.Swords = newSword;
        _swordData = GetSwordData();
        CreateSword(newSword).ToString();

        Debug.Log("А новый меч: " + newSword);
    }

    private int GetTargets()
    {
        return Physics.OverlapSphereNonAlloc(GetAttackPosition(), _playerData.AttackRadius, _hits, _enemyMask);
    }

    private Vector3 GetAttackPosition()
    {
        return _player.transform.position + _player.transform.forward * _playerData.AttackRange;
    }

    private void AttackTarget(Collider collider)
    {

        if (collider == null)
            return;

        if (collider.TryGetComponent(out IDamagable damagable))
        {
            damagable.TakeDamage(_swordData.Damage);
        }

        if (collider.TryGetComponent(out IKnockable knockable))
            knockable.ApplyKnockBack(_swordData.PushDistance);
    }

    private SwordData GetSwordData()
    {
        for (int i = 0; i < _swordsData.Length; i++)
            if (_swordsData[i].Sword == _playerData.Swords)
                return _swordsData[i];

        throw new ArgumentNullException();
    }

    private void ClearTargets()
    {
        Array.Clear(_hits, 0, _hits.Length);
    }

    private async UniTaskVoid CreateSword(AssetProvider.Swords swordName)
    {
        var prefab = await _factory.CreateAsync(swordName.ToString());
        prefab.transform.parent = _swordPlace.transform;

        _currentSword = prefab.GetComponent<Sword>();
        _currentSword.SetColor(_swordData.Color);
        StopAttack();

        var rotate = Quaternion.Euler(_swordPlace.SwordRotation);
        prefab.transform.SetLocalPositionAndRotation(_swordPlace.SwordPosition, rotate);
    }
}