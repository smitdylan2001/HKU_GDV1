using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsSpawner
{
    public static List<Asteroid> _asteroidsList { get; private set; }
    private static Sprite _sprite;

    //TODO how to call this properly
    void ON_GAME_START()
    {
        _asteroidsList = new List<Asteroid>();
        _sprite = (Sprite)Resources.Load("Textures\asteroid.png");
    }

    /// <summary>
    /// Function that can be called to spawn any number if asteroids with a set size, start position, direction and speed
    /// </summary>
    public static void SpawnAsteroid(int amount, float size, Vector2 startPos, float direction, float speed)
    {
        for (int i = 0; i < amount; i++)
        {
            Asteroid asteroid = new Asteroid(size, startPos, direction, speed, _sprite);
            _asteroidsList.Add(asteroid);
        }
        Debug.Log("Asteroids spawned:" + _asteroidsList);
    }
}
