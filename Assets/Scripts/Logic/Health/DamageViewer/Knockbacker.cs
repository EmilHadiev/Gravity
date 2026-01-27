using DG.Tweening;
using UnityEngine;

public class Knockbacker
{
    private readonly Transform _transform;
    private readonly CustomGravity _gravity;
    private readonly EnemyData _data;

    private Tween _activeMoveTween;
    private Tween _activeShakeTween;

    public Knockbacker(Transform transform, CustomGravity gravity, EnemyData data)
    {
        _transform = transform;
        _gravity = gravity;
        _data = data;
    }

    public void ApplyKnockback()
    {
        _activeMoveTween?.Kill();
        _activeShakeTween?.Kill();

        _transform.localRotation = Quaternion.identity;

        // Рассчитываем вектор отлета
        Vector3 knockbackDir = -_transform.forward * _data.PunchDistance;
        Vector3 targetPos = _transform.position + knockbackDir;

        // Получаем безопасную точку на земле через сферу
        Vector3 groundPos = _gravity.GetGroundPosition(targetPos);

        // Запускаем прыжок
        _activeMoveTween = _transform.DOJump(groundPos, _data.JumpPower, 1, _data.Duration)
            .SetEase(Ease.OutQuad);

        _activeShakeTween = _transform.DOShakeRotation(_data.Duration, _data.ShakeStrength, _data.ShakeVibrate);
    }

    public void KillTweens()
    {
        _activeMoveTween?.Kill();
        _activeShakeTween?.Kill();
    }
}