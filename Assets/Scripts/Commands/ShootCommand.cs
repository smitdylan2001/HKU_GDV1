using UnityEngine;

/// <summary>
/// This class is used to shoot a Bullet from the Player position towards where the player is facing.
/// </summary>
public class ShootCommand : IGameObjectCommand
{
	public void Execute(GameObject origin)
	{
		if(ProjectileManager.BULLET_POOL._activePool.Count > 15)
		{
			ProjectileManager.BULLET_POOL.ReturnObjectToInactive(ProjectileManager.BULLET_POOL._activePool[0]);
		}
		ProjectileManager.BULLET_POOL.RequestItem(0.25f, origin.transform.position, origin.transform.rotation.eulerAngles.z);
	}
}
