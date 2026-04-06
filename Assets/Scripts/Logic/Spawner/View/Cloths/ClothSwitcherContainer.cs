using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ClothSwitcherContainer : MonoBehaviour, IClothSwitcherContainer
{
    [SerializeField] private float _additionalX = 10;
    [SerializeField] private ClothSwitcher _clothSwitcherPrefab;

    [Inject] private readonly IFactory _factory;
    [Inject] private readonly ClothData[] _clothesData;
    
    private readonly List<ClothSwitcher> _clothesSwitchers = new(3);

    private ItemData _currentData;

    public event Action<ItemData> PlayerEntered;
    public event Action PlayerExited;

    private void Start()
    {
        CreateTemplates().Forget();
    }

    public void TrySwitchItem()
    {
        
    }

    private async UniTask CreateTemplates()
    {
        int delay = 100;

        for (int i = 0; i < _clothesData.Length; i++)
        {
            await UniTask.Delay(delay);

            var skinSwitcher = _factory.Create(_clothSwitcherPrefab.gameObject);
            skinSwitcher.transform.parent = transform;
            skinSwitcher.transform.SetLocalPositionAndRotation
                (GetPosition(skinSwitcher.transform, i), transform.rotation);

            var switcher = skinSwitcher.GetComponent<ClothSwitcher>();
            switcher.SetData(_clothesData[i]);
            _clothesSwitchers.Add(switcher);

            switcher.PlayerEntered += OnPlayerEntered;
            switcher.PlayerExited += OnPlayerExited;
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _clothesSwitchers.Count; i++)
        {
            _clothesSwitchers[i].PlayerEntered -= OnPlayerEntered;
            _clothesSwitchers[i].PlayerExited -= OnPlayerExited;
        }
    }

    private Vector3 GetPosition(Transform obj, int i)
    {
        return new Vector3(obj.transform.position.x + (_additionalX * i), 0, 0);
    }

    private void OnPlayerEntered(ItemData data)
    {
        _currentData = data;
        PlayerEntered?.Invoke(_currentData);
    }

    private void OnPlayerExited()
    {
        PlayerExited?.Invoke();
    }
}