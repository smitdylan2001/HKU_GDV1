using UnityEngine;

public class ShootCommand : IGameObjectCommand
{
	//Speed of the bullet
	float _bulletSpeed = 500f;

	/// <summary>
	/// Shoots a bullet from a gameobject
	/// </summary>
	/// <param name="origin"></param>
	public void Execute(GameObject origin)
    {
        new Bullet(origin.transform.position, 0.25f, origin.transform.rotation, _bulletSpeed);
    }
}
