using UnityEngine;

[CreateAssetMenu(menuName = "Data/Enemy", fileName = "enemy_")]
public class EnemyData : ScriptableObject
{
    [Header("Stats")]
    public float Health;
    public float Damage;

    [Header("Damage view")]
    public float JumpPower = 1.5f;
    public float PunchDistance = 2f;
    public float Duration = 0.3f;
    public float ShakeStrength = 15f;
    public int ShakeVibrate = 10;
}