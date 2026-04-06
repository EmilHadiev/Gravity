using TMPro;
using UnityEngine;

public class SwordInfoView : MonoBehaviour
{
    [SerializeField] private TMP_Text _damageText;
    [SerializeField] private TMP_Text _pushText;
    [SerializeField] private TMP_Text _priceText;

    public void SetData(SwordData swordData)
    {
        _damageText.text = swordData.Damage.ToString();
        _pushText.text = swordData.PushDistance.ToString()+"%";
        _priceText.text = $"{swordData.Price}$";
    }
}