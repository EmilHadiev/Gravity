using UnityEngine;
using Zenject;

[RequireComponent(typeof(SwordTrial))]
public class Sword : MonoBehaviour
{
    [SerializeField] private SwordTrial _swordTrial;

    private void OnValidate()
    {
        _swordTrial ??= GetComponent<SwordTrial>();
    }
}