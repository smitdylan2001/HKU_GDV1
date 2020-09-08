using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Asteroid : IRigidBody, IDamageable
{
    public delegate void MyDelegate(Asteroid _asteroidClass);
    public static event MyDelegate OnDestroy;

    private float _size;
    private Vector2 _movementDirection;
    private GameObject _asteroid;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;

    /// <summary> Create a new GameObject and give it all components to work as an asteroid </summary>
    public Asteroid(float size, Vector2 startPos, float direction, float speed, Sprite sprite)
    {
        _size = size;

        _asteroid = new GameObject();
        _asteroid.name = "Asteroid";

        _asteroid.AddComponent<Rigidbody2D>();
        _rigidbody = _asteroid.GetComponent<Rigidbody2D>();

        _asteroid.AddComponent<SpriteRenderer>();
        _spriteRenderer = _asteroid.GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = sprite;

        _asteroid.transform.position = startPos;
        _asteroid.transform.localScale = new Vector2(size, size);


        _movementDirection = new Vector2(Mathf.Cos(direction) * speed, Mathf.Sin(direction) * speed); //TODO test if this works: add calculations from angle to vector and multiply by speed
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

        //Temporary fix to make this working
        AsteroidsManager.SpawnAsteroid(2, _size / 2, _asteroid.transform.position);
    }
}
