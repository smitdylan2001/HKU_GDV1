using UnityEngine;

public class ShootCommand : IGameObjectCommand
{
	/// <summary>
	/// Shoots a bullet from a gameobject
	/// </summary>
	/// <param name="origin"></param>
	public void Execute(GameObject origin)
    {
		if (ProjectileManager._bulletPool._activePool.Count > 15)
		{
			ProjectileManager._bulletPool.ReturnObjectToInactive(ProjectileManager._bulletPool._activePool[0]);
		}
		ProjectileManager._bulletPool.RequestItem(0.25f, origin.transform.position, origin.transform.rotation.eulerAngles.z);
	}
}
