using UnityEngine;

public class PlayerMover : MonoBehaviour, IMovable
{
    [SerializeField] private float _moveSpeed = 10;
    [SerializeField] private float _rotateSpeed = 15;

    public Transform Transform => transform;

    public float MoveSpeed => _moveSpeed;
    public float RotateSpeed => _rotateSpeed;

    private IMovePattern _movePattern;
    private IPlayerAnimator _animator;

    private void Awake()
    {
        _animator = GetComponent<PlayerAnimator>();
        _movePattern = new PlayerMovePattern(this, _animator);
    }

    private void OnEnable()
    {
        _movePattern.Start();
    }

    private void OnDisable()
    {
        _movePattern.Stop();
    }

    private void Update()
    {
        _movePattern.Update();
    }
}