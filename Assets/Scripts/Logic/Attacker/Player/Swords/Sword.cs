using UnityEngine;

[RequireComponent(typeof(SwordTrial))]
public class Sword : MonoBehaviour
{
    [SerializeField] private SwordTrial _swordTrial;

    private void OnValidate()
    {
        _swordTrial ??= GetComponent<SwordTrial>();
    }

    public void SetColor(Color color)
    {
        _swordTrial.SetColor(color);
    }

    public void TrialToggle(bool isOn) => _swordTrial.TrialToggle(isOn);
}