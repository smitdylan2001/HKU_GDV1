﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsManager
{
    public static List<Asteroid> _asteroidsList { get; private set; }
    private static Sprite _sprite;
    public System.Action _asteroidDestroyed;


    /// <summary> Make asteroids list, load sprite and add event </summary>
    AsteroidsManager()
    {
        _asteroidsList = new List<Asteroid>();
        _sprite = (Sprite)Resources.Load("Sprites\asteroid.png");

        _asteroidDestroyed += OnAsteroidDestroyed;
        EventManager.AddListener(EventType.ON_ASTEROID_DESTROYED, _asteroidDestroyed);
    }

    /// <summary> Function called if asteroid is destroyed </summary>
    void OnAsteroidDestroyed()
    {
        //FIXME SpawnAsteroid(2, _size / 2, _asteroid.transform.position, Random.Range(0, 360), Random.Range(1, 5));
    }

    /// <summary> Function that can be called to spawn any number if asteroids with a set size, start position, direction and speed </summary>
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
