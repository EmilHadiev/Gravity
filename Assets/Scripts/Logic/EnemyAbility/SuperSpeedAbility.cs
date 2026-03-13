using UnityEngine;

public class SuperSpeedAbility : EnemyAbilityActivator
{
    [SerializeField] private float _speedMultiplyer = 2;
    [SerializeField] private Transform _rotateObject;
    [SerializeField] private float _rotateObjectSpeed = 30f;
    [SerializeField] private float _maxSpeed = 10f;

    private void Start()
    {
        var data = GetComponent<IEnemy>().Data;
        data.MoveSpeed *= _speedMultiplyer;

        if (data.MoveSpeed > _maxSpeed)
            data.MoveSpeed = _maxSpeed;

        Debug.Log($"“≈ ”Ÿ¿ﬂ — Œ–Œ—“‹: {data.MoveSpeed}");
    }

    public override void Activate()
    {
        if (_rotateObject != null)
        {
            _rotateObject.Rotate(new Vector3(90, 0, 0) * Time.deltaTime * _rotateObjectSpeed);
        }
    }

    private void Update()
    {
        Activate();
    }
}