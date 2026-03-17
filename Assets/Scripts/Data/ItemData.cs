using UnityEngine;

public abstract class ItemData : ScriptableObject, IPurchasable
{
    [field: SerializeField] public bool IsPurchase { get; set; }
    [field: SerializeField] public int Price { get; set; }
    public abstract string ItemName { get; }
}