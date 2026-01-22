using UnityEngine;

public class PlayerMovePattern : IMovePattern
{
    private readonly IMovable _player;
    private bool _isWorking;

    public PlayerMovePattern(IMovable movable)
    {
        _player = movable;
    }

    public void Start() => _isWorking = true;
    public void Stop() => _isWorking = false;

    public void Update()
    {
        if (_isWorking == false) 
            return;

        // Получаем ввод (мгновенно -1, 0 или 1)
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            _player.Transform.Translate(direction * _player.MoveSpeed * Time.deltaTime, Space.World);
            _player.Transform.forward = direction;
        }
    }
}