using UnityEngine;

public class CustomGravity : MonoBehaviour
{
    [Header("Raycast Settings")]
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _rayDistance = 5f;
    [SerializeField] private float _rayOffset = 1.5f;
    [SerializeField] private float _sphereRadius = 0.3f; // Радиус сферы

    private RaycastHit _hit;
    private Vector3 _lastDebugOrigin;
    private Vector3 _lastDebugTarget;

    /// <summary>
    /// Находит точку на земле, используя SphereCast для большей точности
    /// </summary>
    public Vector3 GetGroundPosition(Vector3 targetPos)
    {
        // Начальная точка луча чуть выше целевой позиции
        Vector3 rayOrigin = targetPos + Vector3.up * _rayOffset;

        // Для дебага сохраняем точки
        _lastDebugOrigin = rayOrigin;

        // SphereCast имитирует падение сферы вниз
        if (Physics.SphereCast(rayOrigin, _sphereRadius, Vector3.down, out _hit, _rayDistance, _groundLayer))
        {
            _lastDebugTarget = _hit.point;
            return _hit.point;
        }

        _lastDebugTarget = targetPos;
        return targetPos;
    }

    // Отрисовка в эдиторе
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        // Рисуем начальную сферу
        Gizmos.DrawWireSphere(transform.position + Vector3.up * _rayOffset, _sphereRadius);

        // Рисуем линию "падения"
        Gizmos.DrawLine(transform.position + Vector3.up * _rayOffset, transform.position + Vector3.down * (_rayDistance - _rayOffset));

        // Рисуем сферу в точке попадания (если она была)
        if (_hit.point != Vector3.zero)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_hit.point, _sphereRadius);
        }
    }
}