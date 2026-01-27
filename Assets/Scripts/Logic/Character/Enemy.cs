using UnityEngine;

[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(EnemyDamageView))]
[RequireComponent(typeof(CapsuleCollider))]
public class Enemy : MonoBehaviour, IEnemy
{
    [SerializeField] private EnemyData _data;

    public EnemyData Data => _data;
}