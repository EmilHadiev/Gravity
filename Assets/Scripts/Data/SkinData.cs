using UnityEngine;

[CreateAssetMenu(menuName = "Data/SkinData", fileName = "skin")]
public class SkinData : ItemData
{
    [field: SerializeField] public AssetProvider.Player Skin { get; private set; }

    public override string ItemName => Skin.ToString();
}