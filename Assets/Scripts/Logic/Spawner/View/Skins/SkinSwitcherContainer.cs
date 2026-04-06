using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class SkinSwitcherContainer : MonoBehaviour, ISkinSwitcherContainer
{
    [SerializeField] private SkinSwitcher _skinSwitcherTemplate;
    [SerializeField] private float _additionalX;

    [Inject] private readonly IFactory _factory;
    [Inject] private readonly SkinData[] _skins;
    [Inject] private readonly PlayerData _playerData;

    private ItemData _currentData;

    private List<SkinSwitcher> _skinSwitchers = new(4);

    public event Action<ItemData> PlayerEntered;
    public event Action PlayerExited;

    private void Awake()
    {
        CreateTemplates().Forget();
    }

    private async UniTask CreateTemplates()
    {
        int delay = 100;

        for (int i = 0; i < _skins.Length; i++)
        {
            await UniTask.Delay(delay);

            var skinSwitcher = _factory.Create(_skinSwitcherTemplate.gameObject);
            skinSwitcher.transform.parent = transform;
            skinSwitcher.transform.SetLocalPositionAndRotation
                (GetPosition(skinSwitcher.transform, i), transform.rotation);

            var switcher = skinSwitcher.GetComponent<SkinSwitcher>();
            switcher.SetData(_skins[i]);
            _skinSwitchers.Add(switcher);

            switcher.PlayerEntered += OnPlayerEntered;
            switcher.PlayerExited += OnPlayerExited;
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _skinSwitchers.Count; i++)
        {
            _skinSwitchers[i].PlayerEntered -= OnPlayerEntered;
            _skinSwitchers[i].PlayerExited -= OnPlayerExited;
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

    public void TrySwitchSkin()
    {
        for (int i = 0; i < _skinSwitchers.Count; i++)
        {
            if (_skinSwitchers[i].TrySetSkin(_currentData.ItemName, ChangeSkin))
            {
                return;
            }
        }
    }

    private void ChangeSkin()
    {
        AssetProvider.Player[] skins = Enum.GetValues(typeof(AssetProvider.Player)).Cast<AssetProvider.Player>().ToArray();

        for (int i = 0; i < skins.Length; i++)
        {
            if (skins[i].ToString() == _currentData.ItemName)
            {
                _playerData.Player = skins[i];
                break;
            }

            Debug.Log(skins[i].ToString() + " " + _currentData.ItemName);
        }
    }
}