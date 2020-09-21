using UnityEngine;

/// <summary>
/// The Player Class holds all the members and functionality for the Player Object.
/// </summary>
public class Player : ICollideable, IDamageable<int>
{
	/// <summary> ICollideable HasCollided Implementation. </summary>
	public bool HasCollided { get; set; }
	/// <summary> The player gameobject for other classes to get</summary>
	public GameObject PlayerGO { get; private set; }

	/// <summary> The Health member of the player. </summary>
	private int _health;
	/// <summary> Size of the Player. </summary>
	private float _size;
	/// <summary> Which Layers to check for collision. </summary>
	private LayerMask _collisionMask;
	/// <summary> The sprite of the Player Gameobject. </summary>
	private Sprite _sprite;
	/// <summary> The Rigidbody2D Component of the Player. </summary>
	private SpriteRenderer _spriteRenderer;
	/// <summary> The BoxCollider2D Component of the Bullet. </summary>
	private BoxCollider2D _boxCollider2D;

	/// <summary>
	/// Constructor of the Player Class.
	/// </summary>
	public Player(int health = 100, float size = 0.25f)
	{
		_health = health;
		_size = size;

		_sprite = Resources.Load<Sprite>("Sprites/Player");
		PlayerGO = new GameObject();
		PlayerGO.name = "Player";
		PlayerGO.transform.localScale = new Vector3(_size, _size, _size);

		BoxCollider2D boxCollider2D = PlayerGO.AddComponent<BoxCollider2D>();
		_boxCollider2D = boxCollider2D;
		_boxCollider2D.size = new Vector2(1, 1);
		_boxCollider2D.isTrigger = true;

		SpriteRenderer spriteRenderer = PlayerGO.AddComponent<SpriteRenderer>();
		_spriteRenderer = spriteRenderer;
		_spriteRenderer.sprite = _sprite;

		_collisionMask = ~LayerMask.GetMask("Player", "Projectile");
		PlayerGO.layer = LayerMask.NameToLayer("Player");

		HasCollided = false;
	}

	/// <summary>
	/// ICollideable IsColliding Implementation.
	/// </summary>
	public bool IsColliding()
	{
		Collider2D[] collisions = Physics2D.OverlapCircleAll(PlayerGO.transform.position, _size, _collisionMask);

		if(!HasCollided)
		{
			foreach(Collider2D collider in collisions)
			{
				if(collider != this._boxCollider2D)
				{
					HasCollided = true;
					return true;
				}
			}
		}

		HasCollided = false;
		return false;
	}

	/// <summary>
	/// IDamageable Damage implementation.
	/// </summary>
	/// <param name="damageTaken"> How much damage will be taken. </param>
	public void Damage(int damageTaken)
	{
		_health -= damageTaken;
	}

	/// <summary>
	/// ICollideable OnCollision Implementation.
	/// </summary>
	public void OnCollision()
	{
		Damage(1);
	}
}
