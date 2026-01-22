using System.Collections;
using UnityEngine;

public class CameraFollower : MonoBehaviour, ICameraFollower
{
    [Header("Target Settings")]
    [SerializeField] private Transform _target; // Ссылка на Transform игрока
    [SerializeField] private Vector3 _offset = new Vector3(0, 5, -10); // Дистанция от игрока

    private void LateUpdate()
    {
        if (_target == null) 
            return;

        transform.position = _target.position + _offset;

        transform.LookAt(_target);
    }

    public void SetTarget(Transform target) => _target = target;
}