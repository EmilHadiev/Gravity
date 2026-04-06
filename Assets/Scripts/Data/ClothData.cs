using UnityEngine;

[CreateAssetMenu(menuName = "Data/ClothData", fileName = "cloth")]
public class ClothData : ItemData
{
    [field: SerializeField] public AssetProvider.Cloth Cloth { get; private set; }

    public override string ItemName => Cloth.ToString();
}
