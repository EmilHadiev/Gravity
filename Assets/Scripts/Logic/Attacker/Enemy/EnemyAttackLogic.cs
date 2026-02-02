using System;
using UnityEngine;

public class EnemyAttackLogic 
{
    private readonly LayerMask _target;
    private readonly EnemyData _data;
    private readonly Transform _enemy;
    private readonly Collider[] _hits;

    public EnemyAttackLogic(EnemyData data, Transform currentEnemy)
    {
        _enemy = currentEnemy;
        _data = data;
        _hits = new Collider[_data.CountTargets];

        _target = LayerMask.GetMask(CustmomMasks.Player);
    }

    public void Attack()
    {
        ClearTargets();

        int countTargets = GetCountTargets();
        PhysicsDebug.DrawDebug(GetAttackPosition(), _data.AttackRadius);

        if (countTargets == 0)
            return;

        for (int i = 0; i < countTargets; i++)
        {
            if (_hits[i].TryGetComponent(out IHealth health))
                health.TakeDamage(_data.Damage);
        }
    }

    private int GetCountTargets()
    {
        return Physics.OverlapSphereNonAlloc(GetAttackPosition(), _data.AttackRadius, _hits, _target);
    }

    private Vector3 GetAttackPosition()
    {
        return _enemy.position + (_enemy.forward * _data.AttackDistance);
    }

    private void ClearTargets()
    {
        Array.Clear(_hits, 0, _hits.Length);
    }
}