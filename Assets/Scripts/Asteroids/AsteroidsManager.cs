using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsManager
{
    private static ObjectPool<Asteroid> _asteroidPool;
    public static List<Asteroid> AsteroidsList { get; private set; }

    public System.Action _asteroidDestroyed;

    private static Sprite _sprite;

    /// <summary> Make asteroids list, load sprite and add event </summary>
    public AsteroidsManager()
    {
        AsteroidsList = new List<Asteroid>();
        _sprite = Resources.Load<Sprite>("Sprites/Asteroid");

        _asteroidPool = new ObjectPool<Asteroid>();

        //_asteroidDestroyed += OnAsteroidDestroyed;

        //FIXME This has to add a listener and get the destroyed gameobject once it is destroyed (and pass that on to OnAsteroidDestroyed)
        //EventManager<GameObject>.AddListener(EventType.ON_ASTEROID_DESTROYED, _asteroidDestroyed);
        //GameObject destroyedAsteroid;

        SpawnAsteroid(Random.Range(8, 20), 1);
    }
    
    /// <summary> Function called if asteroid is destroyed </summary>
    void OnAsteroidDestroyed(Asteroid asteroid)
    {
        asteroid.Active = false;

        //TODO: Spawn 2 new smaller asteroids
    }

    /// <summary> Function that can be called to spawn any number if asteroids with a set size</summary>
    public void SpawnAsteroid(int amount, float size)
    {
        for (int i = 0; i < amount; i++) 
        {
            Asteroid asteroid = new Asteroid(size, new Vector2(Random.Range(-15, 15), Random.Range(-10, 10)), Random.Range(0, 360), Random.Range(1, 6), _sprite);
            EventManager<Asteroid>.AddListener(EventType.ON_ASTEROID_DESTROYED, OnAsteroidDestroyed);
            AsteroidsList.Add(asteroid);
        }
        Debug.Log("Asteroids spawned: " + AsteroidsList);
    }

    /// <summary> Function that can be called to spawn any number if asteroids with a set size and start position</summary>
    public void SpawnAsteroid(int amount, float size, Vector2 startPos)
    {
        for (int i = 0; i < amount; i++)
        {
            Asteroid asteroid = new Asteroid(size, startPos, Random.Range(0, 360), Random.Range(1, 6), _sprite);
            EventManager<Asteroid>.AddListener(EventType.ON_ASTEROID_DESTROYED, OnAsteroidDestroyed);
            AsteroidsList.Add(asteroid);
        }
        Debug.Log("Asteroids spawned: " + AsteroidsList);
    }

}
