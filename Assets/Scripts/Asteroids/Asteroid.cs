using UnityEngine;

public class Asteroid : ICollideable, IDamageable<int>, IPoolable, IScoreable<int>
{
	/// <summary> Reference to the Asteroid GameObject. </summary>
	public GameObject ThisAsteroid { get; private set; }
	/// <summary> The Size of the Asteroid. </summary>
	public float Size { get; private set; }
	/// <summary> Returns if the Asteroids is Active or not. </summary>
	public bool Active { get; set; }
	/// <summary> ICollideable HasCollided Implementation. </summary>
	public bool HasCollided { get; set; }

	/// <summary> Which Layers to check for collision. </summary>
	private LayerMask _collisionMask;
	/// <summary> In which direction the Asteroid is moving. </summary>
	private Vector3 _movementDirection;
	/// <summary> Reference to the SpriteRenderer component. </summary>
	private SpriteRenderer _spriteRenderer;
	/// <summary> Reference to the BoxCollider2D Component. </summary>
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
	/// Update Method. Called once per fixed update
	/// </summary>
	public void UpdateAsteroid()
	{
		Move();
		CheckPosition();
	}


	/// <summary>
	/// Damage implementation, destroys asteroid
	/// </summary>
	public void Damage(int damageTaken)
	{
		Destroy(); //Once powerups are added this might need a check for health
	}

	/// <summary>
	/// Asteroids movement implementation using transform.translation
	/// </summary>
	private void Move()
	{
		ThisAsteroid.transform.Translate(_movementDirection.x, _movementDirection.y, 0);
	}

	/// <summary>
	/// Check if asteroids are off screen and wrap them if they are
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
	/// Asteroids destruction implementation. Will invoke on ui update, adds 1 point and invoke on asteroid destroyed
	/// </summary>
	private void Destroy()
	{
		EventManager<int>.InvokeEvent(EventType.ON_UI_UPDATE, 1);
		EventManager<Asteroid>.InvokeEvent(EventType.ON_ASTEROID_DESTROYED, this);
	}

	/// <summary>
	/// Excecuted when asteroid is reactivated. It will set all values to the desired valued
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
	/// Done when Asteroid is deactivated. Right now only disables the object
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

	/// <summary>
	/// ICollideable OnCollision Implementation.
	/// </summary>
	public void OnCollision()
	{
		Debug.Log(ThisAsteroid.name + " Collided!");
		Damage(1);
	}

	/// <summary>
	/// Adds 1 point to the score
	/// </summary>
	public void AddScore(int scoreToAdd)
	{
		ScoreManager.UpdateScore(scoreToAdd);
	}
}
