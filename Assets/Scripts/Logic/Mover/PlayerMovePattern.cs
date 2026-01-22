using UnityEngine;

public class PlayerMovePattern : IMovePattern
{
    private readonly IMovable _player;
    private readonly IPlayerAnimator _animator;

    private bool _isWorking;

    public PlayerMovePattern(IMovable movable, IPlayerAnimator animator)
    {
        _player = movable;
        _animator = animator;
    }

    public void Start() => _isWorking = true;
    public void Stop() => _isWorking = false;

    public void Update()
    {
        if (_isWorking == false)
            return;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            Move(direction);
            Rotate(direction);
            TryPerformAnimation(true);
        }
        else
        {
            TryPerformAnimation(false);
        }
    }

    private void Move(Vector3 direction)
    {
        _player.Transform.Translate(direction * _player.MoveSpeed * Time.deltaTime, Space.World);
    }

    private void Rotate(Vector3 direction)
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        _player.Transform.rotation = Quaternion.Slerp(
            _player.Transform.rotation,
            targetRotation,
            _player.RotateSpeed * Time.deltaTime
        );
    }

    private void TryPerformAnimation(bool isOn)
    {
        if (isOn)
            _animator.StartRunning();
        else
            _animator.StopRunning();
    }
}