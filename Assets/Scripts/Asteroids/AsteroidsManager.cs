using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AsteroidsManager
{
	public System.Action _asteroidDestroyed;

	private ObjectPool<Asteroid> _asteroidPool;
	private Sprite _sprite;

	/// <summary>
	/// Make asteroids list, load sprite and add event
	/// </summary>
	public AsteroidsManager()
	{
		_asteroidPool = new ObjectPool<Asteroid>();
		_sprite = Resources.Load<Sprite>("Sprites/Asteroid");
		SpawnAsteroid(Random.Range(15, 23), 1);
		EventManager<Asteroid>.AddListener(EventType.ON_ASTEROID_DESTROYED, OnAsteroidDestroyed); //TODO test
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
	void OnAsteroidDestroyed(Asteroid asteroid)
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
	public void SpawnAsteroid(int amount, float size)
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
	public void SpawnAsteroid(int amount, float size, Vector3 startPos)
	{
		if(_asteroidPool._activePool.Count < 35)
		{
			for(int i = 0; i < amount; i++)
			{
				Asteroid asteroid = _asteroidPool.RequestItem(size, startPos);
				CollisionManager.Collideables.Add(asteroid);
			}
		}
	}
}
