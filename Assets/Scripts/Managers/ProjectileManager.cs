using System.Diagnostics;

public class ProjectileManager
{
	public static ObjectPool<Bullet> _bulletPool;

	/// <summary>
	/// Adds the object pool and adds needed listeners
	/// </summary>
	public ProjectileManager()
	{
		_bulletPool = new ObjectPool<Bullet>();

		EventManager<Bullet>.AddListener(EventType.ON_ASTEROID_DESTROYED, DestroyProjectile);
		EventManager.AddListener(EventType.ON_LOGIC_UPDATE, Update);
		
	}

	/// <summary>
	/// Updates all projectiles
	/// </summary>
	public void Update()
	{
		foreach (Bullet projectile in _bulletPool._activePool) 
		{
			projectile.Update();
		}
	}

	/// <summary>
	/// Invoked when asteroid is destryed. The bullet will be used to deactivate it
	/// </summary>
	private void DestroyProjectile(Bullet bullet)
	{
		_bulletPool.ReturnObjectToInactive(bullet);
	}
}
