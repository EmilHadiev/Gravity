using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SwordSwitchContainer : MonoBehaviour, ISwordSwitchContainer
{
    [SerializeField] private SwordSwitcher _template;
    [SerializeField] private float _additionalX = 6f;

    [Inject] private readonly IFactory _factory;
    [Inject] private readonly SwordData[] _swordData;

    public event Action<ItemData> PlayerEntered;
    public event Action PlayerExited;

    private ItemData _currentItemData;

    private readonly List<SwordSwitcher> _switches = new List<SwordSwitcher>();

    private void Start()
    {
        CreateTemplates().Forget();
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _switches.Count; i++)
        {
            _switches[i].PlayerEntered -= OnPlayerEntered;
            _switches[i].PlayerExited -= OnPlayerExited;
        }
    }

    private async UniTaskVoid CreateTemplates()
    {
        for (int i = 0; i < _swordData.Length; i++)
        {
            var prefab = _factory.Create(_template.gameObject);
            SetPosition(prefab.transform, i);
            var swordView = prefab.GetComponent<SwordSwitcher>();
            swordView.ShowSwordInfo(_swordData[i]);
            _switches.Add(swordView);

            swordView.PlayerEntered += OnPlayerEntered;
            swordView.PlayerExited += OnPlayerExited;

            await UniTask.DelayFrame(10);
        }
    }

    private void SetPosition(Transform swordPrefab, int mult)
    {
        swordPrefab.parent = transform;
        var pos = transform.position;
        Vector3 newPos = new Vector3(_additionalX * mult, pos.y, pos.z);
        swordPrefab.transform.localPosition = newPos;
    }

    private void OnPlayerEntered(ItemData data)
    {
        _currentItemData = data;
        PlayerEntered?.Invoke(data);
    }

    private void OnPlayerExited()
    {
        PlayerExited?.Invoke();
    }

    public void TrySwitchSword()
    {
        for (int i = 0; i < _switches.Count; i++)
        {
            if (_switches[i].TryChangeSword(_currentItemData.ItemName))
                break;
        }
    }
}