using UnityEngine;

public class Asteroid : ICollideable, IDamageable<int>, IPoolable
{
	//public System.Action<Asteroid> _onDestroy;
	public GameObject ThisAsteroid { get; private set; }
	public float Size { get; private set; }
	public bool Active { get; set; }
	/// <summary> ICollideable HasCollided Implementation. </summary>
	public bool HasCollided { get; set; }

	/// <summary> Which Layers to check for collision. </summary>
	public LayerMask collisionMask { get; private set; }
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

		collisionMask = ~LayerMask.GetMask("Asteroid");
		ThisAsteroid.layer = LayerMask.NameToLayer("Asteroid");

		HasCollided = false;
    
		EventManager<Collider2D>.AddListener(EventType.ON_ASTEROID_HIT, AsteroidHit);
	}

	/// <summary>
	/// Update Method.
	/// </summary>
	public void Update()
	{
		Move();
	}

	/// <summary>
	/// Check if this asteroid is hit
	/// </summary>
	private void AsteroidHit(Collider2D collider)
	{
		Damage(1);
	}

	/// <summary>
	/// Damage implementation
	/// </summary>
	public void Damage(int damageTaken)
	{
		Debug.Log(ThisAsteroid.name + ": Got Damaged!");
		Destroy(); //Once powerups are added this might need a check for health
	}

	/// <summary>
	/// Asteroids movement implementation
	/// </summary>
	private void Move()
	{
		ThisAsteroid.transform.Translate(_movementDirection.x, _movementDirection.y, 0);
		//ThisAsteroid.transform.position = new Vector3(ThisAsteroid.transform.position.x + _movementDirection.x, ThisAsteroid.transform.position.y + _movementDirection.y, ThisAsteroid.transform.position.z);
	}

	/// <summary>
	/// Asteroids destruction implementation
	/// </summary>
	private void Destroy()
	{
		Debug.Log(ThisAsteroid.name + ": Got Destroyed!");
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
		_boxCollider2D.size = new Vector2(Size, Size);

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

	/// <summary>
	/// ICollideable IsColliding Implementation.
	/// </summary>
	public bool IsColliding()
	{
		Debug.Log(ThisAsteroid.name + " is checking for Collision.");
		Collider2D[] collisions = Physics2D.OverlapCircleAll(ThisAsteroid.transform.position, Size, collisionMask);

		if(!HasCollided)
		{
			foreach(Collider2D collider in collisions)
			{
				if(collider != this._boxCollider2D)
				{
					Debug.Log(ThisAsteroid.name + " has collided with: " + collider.name);
					HasCollided = true;
					return true;
				}
			}
		}

		HasCollided = false;
		return false;
	}

	public void OnCollision()
	{
		Debug.Log(ThisAsteroid.name + " OnCollision()");
		Damage(1);
	}
}
