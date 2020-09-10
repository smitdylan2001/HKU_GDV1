﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsManager
{
    public System.Action _asteroidDestroyed;

    private ObjectPool<Asteroid> _asteroidPool;
    private Sprite _sprite;

    /// <summary> Make asteroids list, load sprite and add event </summary>
    public AsteroidsManager()
    {
        _asteroidPool = new ObjectPool<Asteroid>();
        _sprite = Resources.Load<Sprite>("Sprites/Asteroid");
        SpawnAsteroid(Random.Range(8, 20), 1);

    }
    
    /// <summary> Function called if asteroid is destroyed </summary>
    void OnAsteroidDestroyed(Asteroid asteroid)
    {
        asteroid.Active = false;

        _asteroidPool.ReturnObjectToInactive(asteroid);

        SpawnAsteroid(2, asteroid.Size / 2, asteroid.ThisAsteroid.transform.position);
    }

    /// <summary> Function that can be called to spawn any number if asteroids with a set size</summary>
    public void SpawnAsteroid(int amount, float size)
    {
        for (int i = 0; i < amount; i++) 
        {
            Asteroid asteroid = _asteroidPool.RequestItem();
            EventManager<Asteroid>.AddListener(EventType.ON_ASTEROID_DESTROYED, OnAsteroidDestroyed); //TODO test
        }
    }

    /// <summary> Function that can be called to spawn any number if asteroids with a set size and start position</summary>
    public void SpawnAsteroid(int amount, float size, Vector3 startPos)
    {
        for (int i = 0; i < amount; i++)
        {
            Asteroid asteroid = _asteroidPool.RequestItem(size, startPos);
            EventManager<Asteroid>.AddListener(EventType.ON_ASTEROID_DESTROYED, OnAsteroidDestroyed);
        }
    }

}