using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
public class PlayerJumper : MonoBehaviour
{
    [Header("Jump Settings")]
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private LandingLogic _landingLogic;
    [SerializeField] private float _jumpForce = 12f;
    [Range(1f, 10f)]
    [SerializeField] private float _upwardGravityMultiplier = 2.5f; // Ускорение взлета
    [Range(1f, 10f)]
    [SerializeField] private float _downwardGravityMultiplier = 4f;  // Ускорение падения

    [Header("Ground Detection")]
    [SerializeField] private float _groundCheckDistance = 0.2f;
    [SerializeField] private float _rayOffset = 0.1f;
    

    private const string GroundMask = "Ground";

    [Inject] private readonly IPlayerSoundContainer _playerSound;
    [Inject] private readonly IInput _input;

    private LayerMask _groundMask;

    private IPlayerAnimator _animator;
    private RaycastHit[] _groundResults = new RaycastHit[1];
    private bool _isJumpRequested;

    private float _defaultGravityY;

    private void OnValidate()
    {
        _rigidBody ??= GetComponent<Rigidbody>();
        _landingLogic ??= GetComponentInChildren<LandingLogic>();
    }

    private void Awake()
    {
        var player = GetComponent<IPlayer>();
        _animator = player?.Animator;
        _defaultGravityY = Physics.gravity.y;
        _groundMask = LayerMask.GetMask(GroundMask);
    }

    private void OnEnable()
    {
        _input.Jumped += TryJump;
    }

    private void OnDisable()
    {
        _input.Jumped -= TryJump;
    }

    private void FixedUpdate()
    {
        if (_isJumpRequested)
        {
            ExecuteJump();
        }

        ApplyCustomGravity();
    }

    private void TryJump()
    {
        if (IsGrounded() == false)
        {
            Debug.Log("Я НЕ НА ЗЕМЛЕ!!!");
            return;
        }    

        _isJumpRequested = true;
        _landingLogic.Activate();
        _playerSound.Play(AssetProvider.Sounds.Jump.ToString());
    }

    private void ExecuteJump()
    {
        Vector3 vel = _rigidBody.velocity;
        vel.y = _jumpForce;
        _rigidBody.velocity = vel;

        _animator?.Jump();
        _isJumpRequested = false;
    }

    private void ApplyCustomGravity()
    {
        float currentYVelocity = _rigidBody.velocity.y;

        // Если мы в воздухе
        if (currentYVelocity != 0 && !IsGrounded())
        {
            float multiplier = currentYVelocity > 0 ? _upwardGravityMultiplier : _downwardGravityMultiplier;
            float extraGravity = _defaultGravityY * (multiplier - 1) * Time.fixedDeltaTime;

            Vector3 vel = _rigidBody.velocity;
            vel.y += extraGravity;
            _rigidBody.velocity = vel;
        }
    }

    private bool IsGrounded()
    {
        return Physics.RaycastNonAlloc(transform.position + Vector3.up * 0.1f, Vector3.down, _groundResults, _groundCheckDistance, _groundMask) > 0;
    }

    // --- НОВЫЙ МЕТОД ДЛЯ ВИЗУАЛИЗАЦИИ ---
    private void OnDrawGizmos()
    {
        // Устанавливаем цвет луча: зеленый, если на земле, красный, если в воздухе
        // В редакторе (не во время игры) Awake не вызывается, поэтому проверяем маску вручную
        if (_groundMask == 0) _groundMask = LayerMask.GetMask(GroundMask);

        bool grounded = IsGrounded();
        Gizmos.color = grounded ? Color.green : Color.red;

        Vector3 origin = transform.position + Vector3.up * _rayOffset;
        Vector3 direction = Vector3.down * _groundCheckDistance;

        // Рисуем линию луча
        Gizmos.DrawRay(origin, direction);

        // Дополнительно рисуем маленькую сферу в точке, куда максимально дотягивается луч
        Gizmos.DrawSphere(origin + direction, 0.05f);
    }
}