using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SwordsRotator : MonoBehaviour
{
    [SerializeField] private SwordSpawnPoint _spawnPoint;
    [SerializeField] private float _radius = 4f;
    [SerializeField] private float _yPosition = 3f;

    [Inject] private readonly IFactory _factory;

    private Sword _sword;
    private PlayerData _data;

    private int _countSwords;

    private List<SwordSpawnPoint> _spawnPoints;

    private void Awake()
    {
        IPlayer player = GetComponent<IPlayer>();
        _data = player.Data;
        _countSwords = _data.SwordsCount;

        _spawnPoints = new List<SwordSpawnPoint>();

        CreateSpawnPoints();
        CreateSwords().Forget();
    }

    private void CreateSpawnPoints()
    {
        if (_countSwords <= 0)
            return;

        for (int i = 0; i < _countSwords; i++)
        {
            float angle = i * Mathf.PI * 2f / _countSwords;

            float x = Mathf.Cos(angle) * _radius;
            float z = Mathf.Sin(angle) * _radius;
            Vector3 spawnPosition = new Vector3(x, _yPosition, z);

            float angleDegrees = -angle * Mathf.Rad2Deg;

            Quaternion rotation = Quaternion.Euler(0, angleDegrees + 90f, 0);

            SwordSpawnPoint newPoint = Instantiate(_spawnPoint, transform.position + spawnPosition, rotation, transform);

            _spawnPoints.Add(newPoint);
        }
    }

    private async UniTask CreateSwords()
    {
        for (int i = 0; i < _spawnPoints.Count; i++)
        {
            var prefab = await _factory.CreateAsync(_data.Swords.ToString());
            prefab.transform.SetParent(_spawnPoints[i].transform);
            prefab.transform.localPosition = Vector3.zero;
            prefab.transform.localRotation = Quaternion.identity;

            _sword = prefab.GetComponent<Sword>();
        }
    }

    private void Update()
    {
        if (_sword != null)
        {
            for (int i = 0; i < _spawnPoints.Count; i++)
                RotateObject(_spawnPoints[i].transform);
        }
    }

    private void RotateObject(Transform obj)
    {
        obj.RotateAround(transform.position, Vector3.up, _data.AttackSpeed * Time.deltaTime);
    }
}