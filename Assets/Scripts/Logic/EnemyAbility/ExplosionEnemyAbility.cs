using Zenject;

public class ExplosionEnemyAbility : EnemyAbilityActivator
{
    private EnemyExplosionLogic _enemyExplosionLogic;
    private IHealth _health;
    private EnemyData _data;
    private IParticleFactory _factory;
    private IEnemySoundContainer _soundContainer;

    private void Awake()
    {
        IEnemy enemy = GetComponent<Enemy>();
        _health = enemy.Health;
        EnemyData data = enemy.Data;
        _data = data;
        _enemyExplosionLogic = new EnemyExplosionLogic(_factory, _soundContainer, transform, GetDamage());
    }

    [Inject]
    private void Constructor(IEnemySoundContainer soundContainer, IParticleFactory factory)
    {
        _factory = factory;
        _soundContainer = soundContainer;
    }

    private void Attacked()
    {
        Activate();
    }

    public override void Activate()
    {
        _enemyExplosionLogic.BlowUp();
        _health.Die();
    }

    protected virtual float GetDamage()
    {
        int damageMultiplyer = 2;
        return _data.Damage * damageMultiplyer;
    }
}