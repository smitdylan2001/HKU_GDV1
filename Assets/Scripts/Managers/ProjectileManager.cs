using System.Collections.Generic;
using System.Linq;

public static class ProjectileManager
{
	public static List<IProjectile> Projectiles { get; private set; }

	public static void Init()
	{
		Projectiles = new List<IProjectile>();
	}

	public static void Update()
	{
		foreach(IProjectile projectile in Projectiles.ToList())
		{
			projectile.Update();
		}
	}
}
