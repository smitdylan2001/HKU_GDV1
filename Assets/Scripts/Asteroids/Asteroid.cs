﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Asteroid : IRigidBody, IDamageable<int>, IPoolable
{
	//public System.Action<Asteroid> _onDestroy;
	public GameObject ThisAsteroid { get; private set; }
	public float Size { get; private set; }
	public bool Active { get; set; }

	private Vector3 _movementDirection;
	private SpriteRenderer _spriteRenderer;
	private BoxCollider2D _boxCollider2D;

	/// <summary>
	/// Create a new GameObject and give it all components to work as an asteroid
	/// </summary>
	public Asteroid()
	{
		ThisAsteroid = new GameObject
		{
			name = "Asteroid"
		};

		ThisAsteroid.AddComponent<SpriteRenderer>();
		_spriteRenderer = ThisAsteroid.GetComponent<SpriteRenderer>();
		_spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/Asteroid");

		ThisAsteroid.AddComponent<BoxCollider2D>();
		_boxCollider2D = ThisAsteroid.GetComponent<BoxCollider2D>();
		_boxCollider2D.isTrigger = true;
		_boxCollider2D.size = new Vector2(Size, Size);
	}

	/// <summary>
	/// IRigidBody Implementation
	/// </summary>
	public void PhysicsUpdate()
	{
		Move();
	}

	/// <summary>
	/// Damage implementation
	/// </summary>
	public void Damage(int damageTaken)
	{
		Destroy(); //Once powerups are added this might need a check for health
	}

	/// <summary>
	/// Asteroids movement implementation
	/// </summary>
	private void Move()
	{
		ThisAsteroid.transform.position = new Vector3(ThisAsteroid.transform.position.x + _movementDirection.x, ThisAsteroid.transform.position.y + _movementDirection.y, ThisAsteroid.transform.position.z);
	}

	/// <summary>
	/// Asteroids destruction implementation
	/// </summary>
	private void Destroy()
	{
		EventManager<Asteroid>.InvokeEvent(EventType.ON_ASTEROID_DESTROYED, this);
	}

	/// <summary>
	/// Done when Asteroid is activated
	/// </summary>
	public void OnActivate(float size, Vector3 startPos, float direction, float speed)
	{
		Size = size;

		ThisAsteroid.transform.position = startPos;
		ThisAsteroid.transform.localScale = new Vector3(size, size);

		_movementDirection = new Vector3(Mathf.Cos(direction) * speed, Mathf.Sin(direction) * speed); //TODO test if this works: add calculations from angle to vector and multiply by speed
		ThisAsteroid.SetActive(true);
	}

	/// <summary>
	/// Done when Asteroid is deactivated
	/// </summary>
	public void OnDisable()
	{
		ThisAsteroid.SetActive(false);
	}
}
