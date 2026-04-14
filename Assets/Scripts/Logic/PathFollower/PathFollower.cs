using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PathFollower : MonoBehaviour, IPathFollower
{
    [SerializeField] private LineRenderer _line;
    [SerializeField] private Transform _startPoint;

    [Header("Animation Settings")]
    [SerializeField] private float _scrollSpeed = 2.0f; // Скорость движения стрелок
    [SerializeField] private float _arrowsDensity = 1.0f; // Плотность стрелок (чем меньше, тем чаще)

    private Transform _endPoint;
    private float offset;

    private void OnValidate()
    {
        _line ??= GetComponent<LineRenderer>();
        _startPoint ??= GetComponent<Transform>();
    }

    private void Start()
    {
        _line.textureMode = LineTextureMode.Tile;

        // Немного скругляем углы и концы линии
        _line.numCornerVertices = 4;
        _line.numCapVertices = 4;
    }

    private void Update()
    {
        if (_startPoint == null || _endPoint == null)
            return;

        // 1. Устанавливаем позиции начала и конца
        _line.SetPosition(0, _startPoint.position);
        _line.SetPosition(1, _endPoint.position);

        // 2. Считаем дистанцию для корректного тайлинга (повторения текстуры)
        float distance = Vector3.Distance(_startPoint.position, _endPoint.position);

        // Применяем масштаб текстуры, чтобы стрелки всегда были одного размера
        // Отрицательный X может понадобиться, если стрелки смотрят не в ту сторону
        _line.material.mainTextureScale = new Vector2(distance / _arrowsDensity, 1f);

        // 3. Анимируем смещение (Offset)
        offset -= Time.deltaTime * _scrollSpeed;
        _line.material.mainTextureOffset = new Vector2(offset, 0);
    }

    public void SetTarget(Transform target)
    {
        _endPoint = target;
    }
}