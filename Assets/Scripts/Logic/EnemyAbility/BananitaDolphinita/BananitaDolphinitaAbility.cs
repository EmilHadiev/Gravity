using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using Zenject;

public class BananitaDolphinitaAbility : MonoBehaviour
{
    [SerializeField] private BananaPeel _bananaPeelPrefab;
    [SerializeField] private int _delay = 5;
    [SerializeField] private int _maxPeels = 3;

    [Inject] private readonly IFactory _factory;

    private CancellationTokenSource _cts;
    private int _currentPeels;


    private void OnEnable()
    {
        _cts?.Cancel();
        _cts = new CancellationTokenSource();
        _currentPeels = 0;
        CreatePeels().Forget();
    }

    private void OnDisable()
    {
        _cts?.Cancel();
    }

    private async UniTask CreatePeels()
    {
        try
        {
            while (true)
            {
                await UniTask.Delay(_delay * 1000, cancellationToken: _cts.Token);
                CreatePrefab();
            }
        }
        catch (OperationCanceledException)
        {

        }

    }

    private void CreatePrefab()
    {
        var prefab = _factory.Create(_bananaPeelPrefab.gameObject);
        prefab.transform.position = transform.position;
        ++_currentPeels;

        if (_currentPeels == _maxPeels)
            _cts?.Cancel();
    }
}