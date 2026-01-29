using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour, IPlayerAnimator
{
    [SerializeField] private Animator _animator;

    private const string IsRunning = nameof(IsRunning);

    private void OnValidate()
    {
        _animator ??= GetComponent<Animator>();
    }

    public void StartRunning() => _animator.SetBool(IsRunning, true);
    public void StopRunning() => _animator.SetBool(IsRunning, false);
}