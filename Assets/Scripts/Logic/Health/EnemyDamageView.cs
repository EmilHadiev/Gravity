using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CustomGravity))]
public class EnemyDamageView : MonoBehaviour
{
    [SerializeField] private CustomGravity _gravity;

    [Header("Settings")]
    [SerializeField] private float _jumpPower = 1.5f;
    [SerializeField] private float _punchDistance = 2f;
    [SerializeField] private float _duration = 0.3f;

    [Header("Shake Settings")]
    [SerializeField] private float _shakeStrength = 15f;
    [SerializeField] private int _shakeVibrate = 10;

    private Tween _activeMoveTween;
    private Tween _activeShakeTween;

    private IDamagable _damagable;

    private void OnValidate()
    {
        _gravity ??= GetComponent<CustomGravity>();
    }

    private void Awake()
    {
        _damagable = GetComponent<IDamagable>();
    }

    private void OnEnable() => _damagable.DamageApllied += OnDamageAppllied;
    private void OnDisable() => _damagable.DamageApllied -= OnDamageAppllied;

    private void OnDamageAppllied(float obj) => ApplyKnockback();

    public void ApplyKnockback()
    {
        _activeMoveTween?.Kill();
        _activeShakeTween?.Kill();

        transform.localRotation = Quaternion.identity;

        // Рассчитываем вектор отлета
        Vector3 knockbackDir = -transform.forward * _punchDistance;
        Vector3 targetPos = transform.position + knockbackDir;

        // Получаем безопасную точку на земле через сферу
        Vector3 groundPos = _gravity.GetGroundPosition(targetPos);

        // Запускаем прыжок
        _activeMoveTween = transform.DOJump(groundPos, _jumpPower, 1, _duration)
            .SetEase(Ease.OutQuad);

        _activeShakeTween = transform.DOShakeRotation(_duration, _shakeStrength, _shakeVibrate);
    }

    private void OnDestroy() => KillTweens();

    private void KillTweens()
    {
        _activeMoveTween?.Kill();
        _activeShakeTween?.Kill();
    }
}