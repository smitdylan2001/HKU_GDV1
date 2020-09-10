using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Asteroid : IRigidBody, IDamageable<int>, IPoolable
{
    public System.Action<Asteroid> _onDestroy;

    private float _size;
    private Vector2 _movementDirection;
    private GameObject _asteroid;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;

    public bool Active { get; set; }

    /// <summary> Create a new GameObject and give it all components to work as an asteroid </summary>
    public Asteroid(float size, Vector2 startPos, float direction, float speed, Sprite sprite)
    {
        _asteroid = new GameObject();
        _asteroid.name = "Asteroid";

        _asteroid.AddComponent<Rigidbody2D>();
        _rigidbody = _asteroid.GetComponent<Rigidbody2D>();

        _asteroid.AddComponent<SpriteRenderer>();
        _spriteRenderer = _asteroid.GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = sprite;
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
        EventManager<Asteroid>.InvokeEvent(EventType.ON_ASTEROID_DESTROYED, this);
    }

    public void OnActivate(float size, Vector2 startPos, float direction, float speed)
    {
        _size = size;

        _asteroid.transform.position = startPos;
        _asteroid.transform.localScale = new Vector2(size, size);

        _movementDirection = new Vector2(Mathf.Cos(direction) * speed, Mathf.Sin(direction) * speed); //TODO test if this works: add calculations from angle to vector and multiply by speed
        _asteroid.SetActive(true);
    }

    public void OnDisable()
    {
        _asteroid.SetActive(false);
    }
}
