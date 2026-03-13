public class SuperExplosionEnemyAbility : ExplosionEnemyAbility
{
    protected override float GetDamage()
    {
        int additionalMultiPlyer = 10;
        return base.GetDamage() * additionalMultiPlyer;
    }
}