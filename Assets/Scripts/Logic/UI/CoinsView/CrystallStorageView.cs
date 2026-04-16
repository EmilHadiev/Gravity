using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CrystallStorageView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Button _addCrystalls;

    [Inject] private readonly ICrystallStorage _gemsStorage;
    [Inject] private readonly IShopWindowStateMachine _shopWindowStateMachine;

    private void OnValidate()
    {
        _text ??= GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        _addCrystalls.onClick.AddListener(OnButtonClicked);

        _gemsStorage.MoneyChanged += OnCrystallsChanged;
        OnCrystallsChanged(_gemsStorage.Money);
    }

    private void OnDisable()
    {
        _gemsStorage.MoneyChanged -= OnCrystallsChanged;
        _addCrystalls.onClick.RemoveListener(OnButtonClicked);
    }

    private void OnCrystallsChanged(int coins)
    {
        _text.text = $"{coins}";
    }

    private void OnButtonClicked()
    {
        _shopWindowStateMachine.Switch<PaymentWindow>();
    }
}