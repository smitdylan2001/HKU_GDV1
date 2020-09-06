﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Asteroid : IRigidBody, IDamageable
{
    public delegate void MyDelegate(Asteroid _asteroidClass);
    public static event MyDelegate OnDestroy;

    private float _size { get; set; }
    private Vector2 _movementDirection { get; set; }
    private GameObject _asteroid { get; set; }
    private Rigidbody2D _rigidbody { get; set; }

    /// <summary> Create a new GameObject and give it all components to work as an asteroid </summary>
    public Asteroid(float size, Vector2 startPos, float direction, float speed, Sprite sprite)
    {
        _size = size;

        _asteroid = new GameObject();
        _asteroid.name = "Asteroid";

        _asteroid.AddComponent<Rigidbody2D>();
        _rigidbody = _asteroid.GetComponent<Rigidbody2D>();

        _asteroid.AddComponent<SpriteRenderer>().sprite = sprite; //can be expanded if needed

        _asteroid.transform.localScale= new Vector3(size, size, size);

        _movementDirection = new Vector2(direction * speed, direction * speed); //FIXME add calculations from angle to vector and multiply by speed
    }

    /// <summary> IRigidBody Implementation </summary>
    public void PhysicsUpdate()
    {
        Move(); 
    }

    /// <summary> Damage implementation </summary>
    public void Damage(int damageTaken)
    {
        Destroy(); //Once powerups are added this might need a check for health
    }

    /// <summary> Asteroids movement implementation </summary>
    private void Move()
    {
        _rigidbody.AddForce(_movementDirection);
    }

    /// <summary> Asteroids destruction implementation </summary>
    private void Destroy()
    {
        //FIXME how to call an event and pass a variable with it 
        //EventManager<GameObject>.InvokeEvent(EventType.ON_ASTEROID_DESTROYED, _asteroid);


        //should not be used: AsteroidsSpawner.SpawnAsteroid(2, _size / 2, _asteroid.transform.position, Random.Range(0, 360), Random.Range(1, 5));
    }
}
