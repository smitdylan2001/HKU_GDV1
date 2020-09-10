using UnityEngine;

/// <summary>
/// The Player Class holds all the members and functionality for the Player Object.
/// </summary>
public class Player : IRigidBody, IPlayable, ICollideable, IDamageable<int>
{
	/// <summary> The Health member of the player. </summary>
	public int Health { get; private set; }
	/// <summary> Size of the Player. </summary>
	public float Size { get; private set; }
	/// <summary> The Rotation member of the player. </summary>
	public float Rotation { get; private set; }
	/// <summary> The Thrusting Power of the player. </summary>
	public float ThrustPower { get; private set; }
	/// <summary> The Rotation Power of the player. </summary>
	public float RotationPower { get; private set; }

	/// <summary> The sprite of the Player Gameobject. </summary>
	public Sprite Sprite { get; private set; }
	/// <summary> The Rigidbody2D Component of the Player. </summary>
	public Rigidbody2D Rb2d { get; private set; }
	/// <summary> The SpriteRenderer Component of the Player. </summary>
	public SpriteRenderer SpriteRenderer { get; private set; }
	/// <summary> The BoxCollider2D Component of the Player. </summary>
	public BoxCollider2D BoxCollider2D { get; private set; }

	/// <summary>
	/// Constructor of the Player Class.
	/// </summary>
	public Player(int health = 100, float size = 0.25f, float thrustPower = 50, float rotationPower = 150)
	{
		Health = health;
		Size = size;
		ThrustPower = thrustPower;
		RotationPower = rotationPower;

		Sprite sprite = Resources.Load<Sprite>("Sprites/Player");

		GameObject playerGO = new GameObject();
		playerGO.name = "Player";
		playerGO.transform.localScale = new Vector3(Size, Size, Size);

		Rigidbody2D rb2d = playerGO.AddComponent<Rigidbody2D>();
		Rb2d = rb2d;

		BoxCollider2D boxCollider2D = playerGO.AddComponent<BoxCollider2D>();
		BoxCollider2D = boxCollider2D;
		BoxCollider2D.size = new Vector2(Size, Size);
		BoxCollider2D.isTrigger = true;

		SpriteRenderer spriteRenderer = playerGO.AddComponent<SpriteRenderer>();
		SpriteRenderer = spriteRenderer;
		SpriteRenderer.sprite = sprite;
	}
	/// <summary>
	/// IRigidBody Implementation.
	/// </summary>
	public void PhysicsUpdate()
	{
		Thrust();
	}
	/// <summary>
	/// IPlayable OnInput Implemention.
	/// </summary>
	public void OnInput()
	{
		Rotate();
		Shoot();
	}

	/// <summary>
	/// ICollideable OnCollision Implementation.
	/// </summary>
	public void OnCollision()
	{
		Collider2D[] collisions = Physics2D.OverlapCircleAll(Rb2d.gameObject.transform.position, Size);

		foreach(Collider2D collider in collisions)
		{
			Debug.Log("Hit: " + collider.name);
			collider.GetComponent<IDamageable<int>>()?.Damage(1);
			Damage(1);
		}
	}

	/// <summary>
	/// IDamageable Damage implementation.
	/// </summary>
	/// <param name="damageTaken"> How much damage will be taken. </param>
	public void Damage(int damageTaken)
	{
		Health -= damageTaken;
	}

	/// <summary>
	/// Applies Forward Thrust to the player.
	/// </summary>
	private void Thrust()
	{
		//TODO Use a Custom Input manager instead of the built-in one.

		if(Input.GetKey(KeyCode.W)) Rb2d.AddForce(Rb2d.transform.up * ThrustPower * Time.deltaTime);
	}
	/// <summary>
	/// Rotates the Player on Input.
	/// </summary>
	private void Rotate()
	{
		//TODO Use a Custom Input manager instead of the built-in one.

		Rotation = Input.GetAxisRaw("Horizontal");
		Rb2d.MoveRotation(Rb2d.rotation -= Rotation * RotationPower * Time.deltaTime);
	}

	/// <summary>
	/// Shoots a projectile in the direction the player is facing.
	/// </summary>
	private void Shoot()
	{
		//TODO Use a Custom Input manager instead of the built-in one.
		if(Input.GetKeyDown(KeyCode.Space))
		{
			Bullet bullet = new Bullet(Rb2d.transform.position, Rb2d.transform.rotation, 300f);
		}
	}
}
