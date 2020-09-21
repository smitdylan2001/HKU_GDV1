using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AsteroidsManager
{
	public System.Action _asteroidDestroyed;

	private ObjectPool<Asteroid> _asteroidPool;

	/// <summary>
	/// Make asteroids list, load sprite and add event
	/// </summary>
	public AsteroidsManager()
	{
		_asteroidPool = new ObjectPool<Asteroid>();
		SpawnAsteroid(Random.Range(15, 23));
		EventManager<Asteroid>.AddListener(EventType.ON_ASTEROID_DESTROYED, OnAsteroidDestroyed);
	}

	public void PhysicsUpdate()
	{
		foreach(Asteroid asteroid in _asteroidPool._activePool)
		{
			asteroid.UpdateAsteroid();
		}
	}

	/// <summary>
	/// Function called if asteroid is destroyed
	/// </summary>
	private void OnAsteroidDestroyed(Asteroid asteroid)
	{
		_asteroidPool.ReturnObjectToInactive(asteroid);
		
		if(asteroid.Size > 0.3)
		{
			SpawnAsteroid(2, asteroid.Size / 2, asteroid.ThisAsteroid.transform.position);
		}

		CollisionManager.Collideables.Remove(asteroid);
	}

	/// <summary>
	/// Function that can be called to spawn any number if asteroids with a set size
	/// </summary>
	private void SpawnAsteroid(int amount)
	{
		for(int i = 0; i < amount; i++)
		{
			Asteroid asteroid = _asteroidPool.RequestItem();
			CollisionManager.Collideables.Add(asteroid);
		}
	}

	/// <summary>
	/// Function that can be called to spawn any number if asteroids with a set size and start position
	/// </summary>
	private void SpawnAsteroid(int amount, float size, Vector3 startPos)
	{
		if(_asteroidPool._activePool.Count < 35)
		{
			for(int i = 0; i < amount; i++)
			{
				Asteroid asteroid = _asteroidPool.RequestItem(size, startPos, Random.Range(0, 360));
				CollisionManager.Collideables.Add(asteroid);
			}
		}
	}
}
