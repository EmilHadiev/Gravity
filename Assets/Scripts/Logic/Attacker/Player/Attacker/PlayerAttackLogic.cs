using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class PlayerAttackLogic
{
    private readonly IFactory _factory;
    private readonly IPlayerSoundContainer _playerSound;
    private readonly PlayerData _playerData;
    private readonly SwordData _swordData;
    private readonly Collider[] _hits;
    private readonly Transform _player;
    private readonly LayerMask _enemyMask;
    private readonly SwordSpawnPoint _swordPlace;

    public PlayerAttackLogic(Transform player, SwordSpawnPoint swordSpawn, IFactory factory, IPlayerSoundContainer playerSoundContainer, 
        PlayerData playerData, SwordData[] swords)
    {
        _factory = factory;
        _swordPlace = swordSpawn;
        _playerSound = playerSoundContainer;
        _playerData = playerData;
        _swordData = GetSwordData(swords);
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

        if (targets == 0)
        {
            _playerSound.Play(AssetProvider.Sounds.AttackMiss.ToString());
            return;
        }

        _playerSound.PlayWhenFree(AssetProvider.Sounds.Attack.ToString());

        for (int i = 0; i < targets; i++)
            AttackTarget(_hits[i]);
    }

    public void SwitchSword(AssetProvider.Swords newSword)
    {
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
        Debug.Log(collider.name);
        if (collider == null)
            return;

        if (collider.TryGetComponent(out IDamagable damagable))
        {
            damagable.TakeDamage(_swordData.Damage);
        }

        if (collider.TryGetComponent(out IKnockable knockable))
            knockable.ApplyKnockBack(_swordData.PushDistance);
    }

    private SwordData GetSwordData(SwordData[] swords)
    {
        for (int i = 0; i < swords.Length; i++)
            if (swords[i].Sword == _playerData.Swords)
                return swords[i];

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
        var rotate = Quaternion.Euler(_swordPlace.SwordRotation);
        prefab.transform.SetLocalPositionAndRotation(_swordPlace.SwordPosition, rotate);
    }
}