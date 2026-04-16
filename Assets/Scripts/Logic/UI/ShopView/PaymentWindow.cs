using TMPro;
using UnityEngine;
using Zenject;

public class PaymentWindow : MonoBehaviour, IShopWindowState
{
    [SerializeField] private GameObject _paymentCatalog;
    [SerializeField] private TMP_Text _failedText;

    [Inject] private readonly IPaymentService _paymentService;
    [Inject] private readonly IUISoundContainer _uiSoundContainer;
    [Inject] private readonly ISavable _saver;
    [Inject] private readonly IShopWindowStateMachine _shopStateMachine;

    private void OnEnable()
    {
        _paymentService.Paid += OnPaidFailed;
    }

    private void OnDisable()
    {
        _paymentService.Paid -= OnPaidFailed;
    }

    public void Enter()
    {
        gameObject.SetActive(true);
    }

    public void Exit()
    {
        gameObject.SetActive(false);
    }

    private void OnPaidFailed(bool status)
    {
        PaymentViewEnable(status);
    }

    private void PaymentViewEnable(bool isSuccess)
    {
        if (isSuccess)
        {
            _saver.Save();
            _paymentCatalog.SetActive(true);
            _failedText.gameObject.SetActive(false);
            _uiSoundContainer.Play(AssetProvider.Sounds.AddCoins.ToString());
            _shopStateMachine.Switch<EmptyShopState>();
        }
        else
        {
            _paymentCatalog.SetActive(false);
            _failedText.gameObject.SetActive(true);
        }
    }
}