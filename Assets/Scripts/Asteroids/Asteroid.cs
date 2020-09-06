using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Asteroid : IRigidBody, IDamageable
{
    private float _size { get; set; }
    private Vector2 _movementDirection { get; set; }
    private GameObject _asteroid { get; set; }
    private Rigidbody2D _rigidbody { get; set; }

    /// <summary>
    /// Create a new GameObject and give it all components to work as an asteroid
    /// </summary>
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

    /// <summary>
	/// IRigidBody Implementation
	/// </summary>
    public void PhysicsUpdate()
    {
        //update physics
        Move(); 
    }

    /// <summary>
	/// Damage implementation
	/// </summary>
    public void Damage(int damageTaken)
    {
        //call on destroyed event
    }

    /// <summary>
	/// Asteroids movement implementation
	/// </summary>
    private void Move()
    {
        _rigidbody.AddForce(_movementDirection);
        //addforce
    }

    //TODO how to call this properly
    private void ON_DESTROYED()
    {
        Debug.Log("Delete Object");
        Debug.Log("Spawn 2 smaller");
        AsteroidsSpawner.SpawnAsteroid(2, _size / 2, _asteroid.transform.position, Random.Range(0, 360), Random.Range(1, 5));
    }
}
