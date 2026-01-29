using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour, IEnemyAnimator
{
    [SerializeField] private Animator _animator;

    private const string IsRunning = nameof(IsRunning);
    private const string IsAttacking = nameof(IsAttacking);

    private void OnValidate()
    {
        _animator ??= GetComponent<Animator>();
    }

    public void StartAttacking() => _animator.SetBool(IsAttacking, true);
    public void StopAttacking() => _animator.SetBool(IsAttacking, false);

    public void StartRunning() => _animator.SetBool(IsRunning, true);
    public void StopRunning() => _animator.SetBool(IsRunning, false);    
}