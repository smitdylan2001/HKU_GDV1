using UnityEngine;

public class Asteroid : ICollideable, IDamageable<int>, IPoolable, IScoreable<int>
{
	//public System.Action<Asteroid> _onDestroy;
	public GameObject ThisAsteroid { get; private set; }
	public float Size { get; private set; }
	public bool Active { get; set; }
	/// <summary> ICollideable HasCollided Implementation. </summary>
	public bool HasCollided { get; set; }

	/// <summary> Which Layers to check for collision. </summary>
	private LayerMask _collisionMask;
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

		_collisionMask = ~LayerMask.GetMask("Asteroid");
		ThisAsteroid.layer = LayerMask.NameToLayer("Asteroid");

		HasCollided = false;
	}

	/// <summary>
	/// Update Method.
	/// </summary>
	public void UpdateAsteroid()
	{
		Move();
		CheckPosition();
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
		ThisAsteroid.transform.Translate(_movementDirection.x, _movementDirection.y, 0);
	}

	/// <summary>
	/// Check if asteroids are off screen and wrap them
	/// </summary>
	private void CheckPosition()
	{
		if(Screen.width < Camera.main.WorldToScreenPoint(ThisAsteroid.transform.position).x - 100)
		{
			ThisAsteroid.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(new Vector3(-100, 0, 0)).x, ThisAsteroid.transform.position.y, 0);
		}
		else if(0 > Camera.main.WorldToScreenPoint(ThisAsteroid.transform.position).x + 100)
		{
			ThisAsteroid.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x + 1, ThisAsteroid.transform.position.y, 0);
		}
		else if(Screen.height < Camera.main.WorldToScreenPoint(ThisAsteroid.transform.position).y - 100)
		{
			ThisAsteroid.transform.position = new Vector3(ThisAsteroid.transform.position.x, Camera.main.ScreenToWorldPoint(new Vector3(0, -100, 0)).y, 0);
		}
		else if(0 > Camera.main.WorldToScreenPoint(ThisAsteroid.transform.position).y + 100)
		{
			ThisAsteroid.transform.position = new Vector3(ThisAsteroid.transform.position.x, Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y + 1, 0);
		}
	}

	/// <summary>
	/// Asteroids destruction implementation
	/// </summary>
	private void Destroy()
	{
		EventManager<int>.InvokeEvent(EventType.ON_UI_UPDATE, 1);
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

		_movementDirection = new Vector3(Mathf.Cos(direction) * speed, Mathf.Sin(direction) * speed);
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
		Collider2D[] collisions = Physics2D.OverlapCircleAll(ThisAsteroid.transform.position, Size, _collisionMask);

		if(collisions.Length > 0)
		{
			foreach(Collider2D collider in collisions)
			{
				if(collider != this._boxCollider2D)
				{
					return true;
				}
			}
		}

		return false;
	}

	public void OnCollision()
	{
		Debug.Log(ThisAsteroid.name + " Collided!");
		Damage(1);
	}

	public void AddScore(int scoreToAdd)
	{
		ScoreManager.Score += scoreToAdd;
	}
}
