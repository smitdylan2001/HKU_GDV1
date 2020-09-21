public class ProjectileManager
{
	public static ObjectPool<Bullet> _bulletPool;

	public ProjectileManager()
	{
		_bulletPool = new ObjectPool<Bullet>();

		EventManager<Bullet>.AddListener(EventType.ON_ASTEROID_DESTROYED, DestroyProjectile);
		EventManager.AddListener(EventType.ON_PHYSICS_UPDATE, Update);
	}

	public void Update()
	{
		foreach(Bullet projectile in _bulletPool._activePool)
		{
			projectile.Update();
		}
	}

	private void DestroyProjectile(Bullet bullet)
	{
		_bulletPool.ReturnObjectToInactive(bullet);
	}
}
