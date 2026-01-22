using UnityEngine;

public class PlayerMover : MonoBehaviour, IMovable
{
    [SerializeField] private float _moveSpeed = 10;

    public Transform Transform => transform;

    public float MoveSpeed => _moveSpeed;

    private IMovePattern _movePattern;

    private void Awake()
    {
        _movePattern = new PlayerMovePattern(this);
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