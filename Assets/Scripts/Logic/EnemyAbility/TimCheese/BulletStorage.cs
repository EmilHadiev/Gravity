using System.Collections.Generic;

public class BulletStorage
{
    private readonly IFactory _factory;
    private const int MaxSize = 5;
    private List<Bullet> _bullets;
    private readonly Bullet _prefab;

    public BulletStorage(IFactory factory, Bullet prefab)
    {
        _factory = factory;
        _bullets = new List<Bullet>(MaxSize);
        _prefab = prefab;
        CreateBullets();
    }

    private void CreateBullets()
    {
        for (int i = 0; i < MaxSize; i++)
        {
            var bullet = _factory.Create(_prefab.gameObject);
            bullet.gameObject.SetActive(false);
            _bullets.Add(bullet.GetComponent<Bullet>());
        }
    }

    public bool GetBullet(out Bullet bullet)
    {
        for (int i = 0; i < _bullets.Count; i++)
        {
            if (_bullets[i].gameObject.activeInHierarchy == false)
            {
                bullet = _bullets[i];
                return true;
            }
        }

        bullet = null;
        return false;
    }
}