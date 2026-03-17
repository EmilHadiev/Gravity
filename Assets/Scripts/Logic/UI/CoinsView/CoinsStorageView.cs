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
        _coinStorage.CoinsChanged += OnCoinsChanged;
        OnCoinsChanged(_coinStorage.Coins);
    }

    private void OnDisable()
    {
        _coinStorage.CoinsChanged -= OnCoinsChanged;
    }

    private void OnCoinsChanged(int coins)
    {
        _text.text = $"{coins}$";
    }
}