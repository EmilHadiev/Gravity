using UnityEngine;

[CreateAssetMenu(menuName = "Data/SwordData", fileName = "sword")]
public class SwordData : ItemData
{
    [field: SerializeField] public AssetProvider.Swords Sword { get; private set; }
    [field: SerializeField] public Color Color { get; private set; }

    public float Damage;
    public float PushDistance = 2f;

    public override string ItemName => Sword.ToString();
}
