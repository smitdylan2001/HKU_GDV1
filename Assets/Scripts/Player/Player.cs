using UnityEngine;

/// <summary>
/// The Player Class holds all the members and functionality for the Player Object.
/// </summary>
public class Player : IRigidBody, IPlayable
{
	/// <summary> The Health member of the player. </summary>
	public int _health { get; private set; }
	/// <summary> The Rotation member of the player. </summary>
	public float _rotation { get; private set; }
	/// <summary> The Thrusting Power of the player. </summary>
	public float _thrustPower { get; private set; }
	/// <summary> The Rotation Power of the player. </summary>
	public float _rotationPower { get; private set; }

	/// <summary> The sprite of the Player Gameobject. </summary>
	public Sprite _sprite { get; private set; }
	/// <summary> The Rigidbody2D Component of the Player. </summary>
	public Rigidbody2D _rb2d { get; private set; }
	/// <summary> The SpriteRenderer Component of the Player. </summary>
	public SpriteRenderer _spriteRenderer { get; private set; }

	/// <summary>
	/// Constructor of the Player Class.
	/// </summary>
	public Player(int health = 100, float thrustPower = 50, float rotationPower = 150, Sprite sprite = null)
	{
		_health = health;
		_thrustPower = thrustPower;
		_rotationPower = rotationPower;
		_sprite = sprite;

		GameObject playerGO = new GameObject();

		playerGO.name = "Player";

		Rigidbody2D rb2d = playerGO.AddComponent<Rigidbody2D>();
		_rb2d = rb2d;

		SpriteRenderer spriteRenderer = playerGO.AddComponent<SpriteRenderer>();
		_spriteRenderer = spriteRenderer;
	}
	/// <summary>
	/// IRigidBody Implementation.
	/// </summary>
	public void PhysicsUpdate()
	{
		Thrust();
	}
	/// <summary>
	/// IPlayable ONInput Implemention.
	/// </summary>
	public void OnInput()
	{
		Rotate();
		Shoot();
	}

	/// <summary>
	/// Applies Forward Thrust to the player.
	/// </summary>
	private void Thrust()
	{
		//TODO Use a Custom Input manager instead of the built-in one.

		if(Input.GetKey(KeyCode.W)) _rb2d.AddForce(_rb2d.transform.up * _thrustPower * Time.deltaTime);
	}
	/// <summary>
	/// Rotates the Player on Input.
	/// </summary>
	private void Rotate()
	{
		//TODO Use a Custom Input manager instead of the built-in one.

		_rotation = Input.GetAxisRaw("Horizontal");
		_rb2d.MoveRotation(_rb2d.rotation -= _rotation * _rotationPower * Time.deltaTime);
	}

	/// <summary>
	/// Shoots a projectile in the direction the player is facing.
	/// </summary>
	private void Shoot()
	{
		//TODO Use a Custom Input manager instead of the built-in one.
		if(Input.GetKeyDown(KeyCode.Space))
		{
			Bullet bullet = new Bullet(_rb2d.transform.position, _rb2d.rotation, 300f, null);
		}
	}
}
