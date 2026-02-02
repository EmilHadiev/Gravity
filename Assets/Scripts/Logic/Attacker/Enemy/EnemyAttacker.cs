using UnityEngine;

public class EnemyAttacker : MonoBehaviour, IAttackable
{
    [SerializeField] private TriggerObserver _observer;

    private IEnemyAnimator _animator;
    private IEnemyStateMachine _stateMachine;
    private EnemyAttackLogic _attackLogic;

    private void OnValidate()
    {
        _observer ??= GetComponentInParent<TriggerObserver>();
    }

    private void Awake()
    {
        IEnemy enemy = GetComponentInParent<Enemy>();
        _animator = enemy.Animator;
        _stateMachine = enemy.StateMachine;
        _attackLogic = new EnemyAttackLogic(enemy.Data, transform);
    }

    private void OnEnable()
    {
        _observer.Entered += OnPlayerEntered;
        _observer.Exited += OnPlayerExited;
    }

    private void OnDisable()
    {
        _observer.Entered -= OnPlayerEntered;
        _observer.Exited -= OnPlayerExited;
    }

    public void Activate()
    {
        _animator.StartAttacking();
    }

    public void Deactivate()
    {
        _animator.StopAttacking();
    }

    private void OnPlayerEntered(Collider collider)
    {
        _stateMachine.SwitchState<EnemyAttackState>();
    }

    private void OnPlayerExited(Collider collider)
    {
        _stateMachine.SwitchState<EnemyRunState>();
    }

    #region call from animation
    private void Attacked()
    {
        _attackLogic.Attack();
    }
    #endregion
}