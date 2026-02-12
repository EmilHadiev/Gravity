using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour, IPlayerAnimator
{
    [SerializeField] private Animator _animator;

    private const string IsRunning = nameof(IsRunning);
    private const string IsAttacking = nameof(IsAttacking);
    private const string JumpTrigger = nameof(JumpTrigger);

    private void OnValidate()
    {
        _animator ??= GetComponent<Animator>();
    }

    public void StartRunning() => _animator.SetBool(IsRunning, true);
    public void StopRunning() => _animator.SetBool(IsRunning, false);

    public void StartAttacking() => _animator.SetBool(IsAttacking, true);
    public void StopAttacking() => _animator.SetBool(IsAttacking, false);

    public void Jump() => _animator.SetTrigger(JumpTrigger);
}