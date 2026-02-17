using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour, IPlayerAnimator
{
    [SerializeField] private Animator _animator;

    private const string IsRunning = nameof(IsRunning);
    private const string JumpTrigger = nameof(JumpTrigger);
    private const string AttackTrigger = nameof(AttackTrigger);

    private void OnValidate()
    {
        _animator ??= GetComponent<Animator>();
    }

    public void StartRunning() => _animator.SetBool(IsRunning, true);
    public void StopRunning() => _animator.SetBool(IsRunning, false);

    public void Jump() => _animator.SetTrigger(JumpTrigger);

    public void Attack() => _animator.SetTrigger(AttackTrigger);
}