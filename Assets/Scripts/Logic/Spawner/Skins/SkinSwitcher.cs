using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(TriggerObserver))]
[RequireComponent(typeof(MaterialColorChanger))]
public class SkinSwitcher : MonoBehaviour
{
    [SerializeField] private TriggerObserver _observer;
    [SerializeField] private MaterialColorChanger _colorChanger;

    private ItemData _data;

    [Inject] private readonly IFactory _factory;

    public event Action<ItemData> PlayerEntered;
    public event Action PlayerExited;

    private void OnValidate()
    {
        _observer ??= GetComponent<TriggerObserver>();
        _colorChanger = GetComponent<MaterialColorChanger>();
    }

    private void OnEnable()
    {
        _observer.Entered += OnPlayerEntered;
        _observer.Exited += OnPlayerExited;
    }

    private void OnDisable()
    {
        _observer.Entered -= OnPlayerEntered;
        _observer.Exited -= OnPlayerExited;
    }

    public void SetData(ItemData data)
    {
        _data = data;
        ChangeColor();
        CreateSkinView();
    }

    private void CreateSkinView()
    {
        var skinCreator = new SkinViewCreator(_factory, transform);
        skinCreator.CreateSkinView(_data.ItemName + AssetProvider.PlayerViewPrefix).Forget();
    }

    private void ChangeColor()
    {
        if (_data.IsPurchase)
        {
            _colorChanger.SetColor(color: _colorChanger.Gold);
        }
    }

    private void OnPlayerEntered(Collider collider)
    {
        PlayerEntered?.Invoke(_data);
    }

    private void OnPlayerExited(Collider collider)
    {
        PlayerExited?.Invoke();
    }

    public bool TrySetSkin(string skinName, Action ChangeSkin)
    {
        if (skinName == _data.ItemName)
        {
            ChangeSkin?.Invoke();
            return true;
        }

        return false;
    }
}