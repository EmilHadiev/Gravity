using TMPro;
using UnityEngine;
using Zenject;

public class CoinsStorageView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    [Inject] private readonly ICoinStorage _coinStorage;

    private void OnValidate()
    {
        _text ??= GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        _coinStorage.MoneyChanged += OnCoinsChanged;
        OnCoinsChanged(_coinStorage.Money);
    }

    private void OnDisable()
    {
        _coinStorage.MoneyChanged -= OnCoinsChanged;
    }

    private void OnCoinsChanged(int coins)
    {
        _text.text = $"{coins}$";
    }
}