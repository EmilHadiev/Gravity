public class EnemyDieLogic : DieLogic
{
    protected override void OnDie()
    {
        gameObject.SetActive(false);
    }
}