using UnityEngine;

[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(EnemyDamageView))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(EnemyAnimator))]
[RequireComponent(typeof(EnemyMover))]
public class Enemy : MonoBehaviour, IEnemy
{
    [SerializeField] private EnemyData _data;
    [SerializeField] private EnemyAnimator _animator;
    [SerializeField] private EnemyMover _mover;

    public EnemyData Data => _data;
    public IEnemyAnimator Animator => _animator;

    private void OnValidate()
    {
        _data ??= GetComponent<EnemyData>();
        _animator ??= GetComponent<EnemyAnimator>();
        _mover ??= GetComponent<EnemyMover>();
    }
}