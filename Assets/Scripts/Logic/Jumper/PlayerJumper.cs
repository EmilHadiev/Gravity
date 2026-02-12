using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerJumper : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private float _jumpForce;

    private const string Ground = "Ground";
    private LayerMask _groundMask;

    private IPlayerAnimator _animator;
    private RaycastHit[] _ground;

    private void OnValidate()
    {
        _rigidBody ??= GetComponent<Rigidbody>();
    }

    private void Awake()
    {
        var player = GetComponent<IPlayer>();
        _animator = player.Animator;

        _ground = new RaycastHit[1];
        _groundMask = LayerMask.GetMask(Ground);
    }

    private void Update()
    {
        if (IsGrounded() == false)
            return;

        if (Input.GetKeyUp(KeyCode.Space))
            Jump();
    }

    private void Jump()
    {
        _rigidBody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        _animator.Jump();
    }

    private bool IsGrounded()
    {
        Ray ray = new Ray(transform.position + Vector3.up * 0.1f, Vector3.down);
        return Physics.RaycastNonAlloc(ray, _ground, 1.1f, _groundMask) > 0;
    }
}