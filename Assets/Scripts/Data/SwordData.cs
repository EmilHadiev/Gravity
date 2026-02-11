using UnityEngine;

[CreateAssetMenu(menuName = "Data/SwordData", fileName = "sword")]
public class SwordData : ScriptableObject, IPurchasable
{
    [field: SerializeField] public AssetProvider.Swords Sword { get; private set; }
    [field: SerializeField] public bool IsPurchase { get; set; }
    [field: SerializeField] public Color Color { get; private set; }

    public float Damage;
    public float PushDistance = 2f;
    public int Price = 0;
}