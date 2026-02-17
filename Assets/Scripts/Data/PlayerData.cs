using UnityEngine;

[CreateAssetMenu(menuName = "Data/Player", fileName = "PlayerData")]
public class PlayerData : ScriptableObject
{
    [field: SerializeField] public float MoveSpeed { get; set; } = 10;
    [field: SerializeField] public float RotateSpeed { get; set; } = 15;
    [field: SerializeField] public float AttackSpeed { get; set; } = 100;
    [field: SerializeField] public float Health { get; set; } = 100;
    [field: SerializeField] public int SwordsCount { get; set; } = 1;
    [field: SerializeField] public int MaxAttackTargets { get; set; } = 10;
    [field: SerializeField] public float AttackRange { get; set; } = 2f;
    [field: SerializeField] public float AttackRadius { get; set; } = 3f;
    [field: SerializeField] public AssetProvider.Swords Swords { get; set; }
    [field: SerializeField] public AssetProvider.Player Player { get; set; }
}