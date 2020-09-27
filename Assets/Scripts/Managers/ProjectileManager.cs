public class ProjectileManager
{
	public static ObjectPool<Bullet> BULLET_POOL;

	/// <summary>
	/// Adds the object pool and adds needed listeners
	/// </summary>
	public ProjectileManager()
	{
		BULLET_POOL = new ObjectPool<Bullet>();

		EventManager<Bullet>.AddListener(EventType.ON_ASTEROID_DESTROYED, DestroyProjectile);
		EventManager.AddListener(EventType.ON_LOGIC_UPDATE, Update);
	}

	/// <summary>
	/// Updates all projectiles
	/// </summary>
	public void Update()
	{
		foreach(Bullet projectile in BULLET_POOL.ActivePool)
		{
			projectile.Update();
		}
	}

	/// <summary>
	/// Invoked when asteroid is destryed. The bullet will be used to deactivate it
	/// </summary>
	private void DestroyProjectile(Bullet bullet)
	{
		BULLET_POOL.ReturnObjectToInactive(bullet);
	}
}
