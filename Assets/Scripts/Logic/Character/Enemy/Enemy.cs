using UnityEngine;

[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(EnemyDamageView))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(EnemyAnimator))]
[RequireComponent(typeof(EnemyMover))]
[RequireComponent(typeof(EnemyAttacker))]
public class Enemy : MonoBehaviour, IEnemy
{
    [SerializeField] private EnemyData _data;
    [SerializeField] private EnemyAnimator _animator;
    [SerializeField] private EnemyMover _mover;
    [SerializeField] private EnemyAttacker _attacker;

    public EnemyData Data => _data;
    public IEnemyAnimator Animator => _animator;

    public IEnemyStateMachine StateMachine { get; private set; }

    private void OnValidate()
    {
        _data ??= GetComponent<EnemyData>();
        _animator ??= GetComponent<EnemyAnimator>();
        _mover ??= GetComponent<EnemyMover>();
        _attacker ??= GetComponent<EnemyAttacker>();
    }

    private void Awake()
    {
        StateMachine = new EnemyStateMachine(_attacker, _mover);
    }
}