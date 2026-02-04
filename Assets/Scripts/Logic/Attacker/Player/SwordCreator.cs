using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SwordCreator
{
    private readonly IFactory _factory;
    private readonly PlayerData _playerData;
    private readonly SwordSpawnPoint _spawnPointPrefab;
    private readonly SwordData[] _swordsData;
    private readonly Transform _player;

    private readonly int _countSwords;
    private readonly float _radius;
    private readonly float _yPosition;

    private List<SwordSpawnPoint> _spawnPoints;
    private Sword _currentSword;
    private string _currentSwordName;

    public SwordCreator(Transform player,IFactory factory, SwordSpawnPoint spawnPoint, PlayerData playerData, SwordData[] swordsData, float radius = 4f, float yPos = 3f)
    {
        _factory = factory;
        _spawnPointPrefab = spawnPoint;
        _playerData = playerData;
        _swordsData = swordsData;
        _player = player;

        _countSwords = _playerData.SwordsCount;
        _radius = radius;
        _yPosition = yPos;
    }

    public void Create()
    {
        _spawnPoints = new List<SwordSpawnPoint>(_countSwords);

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

            SwordSpawnPoint newPoint = GameObject.Instantiate(_spawnPointPrefab, _player.position + spawnPosition, rotation, _player);

            _spawnPoints.Add(newPoint);
        }
    }

    private async UniTask CreateSwords()
    {
        for (int i = 0; i < _spawnPoints.Count; i++)
        {
            var prefab = await _factory.CreateAsync(GetCurrentSwordName());
            prefab.transform.SetParent(_spawnPoints[i].transform);
            prefab.transform.localPosition = Vector3.zero;
            prefab.transform.localRotation = Quaternion.identity;

            _currentSword = prefab.GetComponent<Sword>();
            _currentSword.SetData(GetSwordData(_playerData.Swords));
        }

        _currentSwordName = GetCurrentSwordName();
    }

    private string GetCurrentSwordName()
    {
        return _playerData.Swords.ToString();
    }

    public void Rotate()
    {
        if (_currentSword != null)
        {
            for (int i = 0; i < _spawnPoints.Count; i++)
                RotateObject(_spawnPoints[i].transform);
        }
    }

    private void RotateObject(Transform obj)
    {
        obj.RotateAround(_player.position, Vector3.up, _playerData.AttackSpeed * Time.deltaTime);
    }

    public void DestroyAndSetNewSword(AssetProvider.Swords newSword)
    {
        _factory.ReleaseAsset(GetCurrentSwordName());

        for (int i = 0; i < _spawnPoints.Count; i++)
        {
            GameObject.Destroy(_spawnPoints[i].gameObject);
        }

        _playerData.Swords = newSword;

        ClearSpawnPoints();
        Create();
    }

    private void ClearSpawnPoints()
    {
        _spawnPoints?.Clear();
    }

    private SwordData GetSwordData(AssetProvider.Swords sword)
    {
        for (int i = 0; i < _swordsData.Length; i++)
        {
            if (_swordsData[i].Sword == sword)
                return  _swordsData[i];
        }

        throw new ArgumentException(nameof(sword));
    }
}