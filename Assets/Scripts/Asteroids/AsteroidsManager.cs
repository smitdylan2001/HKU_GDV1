using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsManager
{
    public static List<Asteroid> _asteroidsList { get; private set; }

    public System.Action _asteroidDestroyed;

    private static Sprite _sprite;

    /// <summary> Make asteroids list, load sprite and add event </summary>
    public AsteroidsManager()
    {
        _asteroidsList = new List<Asteroid>();
        _sprite = Resources.Load<Sprite>("Sprites/Asteroid");

        _asteroidDestroyed += OnAsteroidDestroyed;

        //FIXME This has to add a listener and get the destroyed gameobject once it is destroyed (and pass that on to OnAsteroidDestroyed)
        //EventManager<GameObject>.AddListener(EventType.ON_ASTEROID_DESTROYED, _asteroidDestroyed);
        //GameObject destroyedAsteroid;

        SpawnAsteroid(Random.Range(4, 10), 1);
    }
    
    /// <summary> Function called if asteroid is destroyed </summary>
    void OnAsteroidDestroyed()
    {
        //FIXME Get variables from gameobject mentioned above 
        //SpawnAsteroid(2, _size / 2, _asteroid.transform.position, Random.Range(0, 360), Random.Range(1, 5));
    }

    /// <summary> Function that can be called to spawn any number if asteroids with a set size</summary>
    public static void SpawnAsteroid(int amount, float size)
    {
        for (int i = 0; i < amount; i++) 
        {
            Asteroid asteroid = new Asteroid(size, new Vector2(UnityEngine.Random.Range(-20, 20), UnityEngine.Random.Range(-20, 20)), Random.Range(0, 360), Random.Range(1, 6), _sprite);
            _asteroidsList.Add(asteroid);
        }
        Debug.Log("Asteroids spawned: " + _asteroidsList);
    }

    /// <summary> Function that can be called to spawn any number if asteroids with a set size and start position</summary>
    public static void SpawnAsteroid(int amount, float size, Vector2 startPos)
    {
        for (int i = 0; i < amount; i++)
        {
            Asteroid asteroid = new Asteroid(size, startPos, Random.Range(0, 360), Random.Range(1, 6), _sprite);
            _asteroidsList.Add(asteroid);
        }
        Debug.Log("Asteroids spawned: " + _asteroidsList);
    }

}
