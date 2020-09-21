using UnityEngine;

/// <summary>
/// IPoolable gets used by classes that need to be pooled. I.E. Asteroid and Bullet.
/// </summary>
public interface IPoolable
{
	bool Active { set; get; }
	void OnActivate(float size, Vector3 startPos, float direction, float speed);
	void OnDisable();
}
