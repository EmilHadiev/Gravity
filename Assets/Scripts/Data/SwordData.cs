using UnityEngine;

[CreateAssetMenu(menuName = "Data/SwordData", fileName = "sword")]
public class SwordData : ScriptableObject, IPurchasable
{
    [field: SerializeField] public AssetProvider.Swords Sword { get; private set; }
    [field: SerializeField] public bool IsPurchase { get; set; }

    public float Damage;
    public float PunchDistance = 2f;
}