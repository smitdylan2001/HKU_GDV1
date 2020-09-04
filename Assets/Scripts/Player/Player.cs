using UnityEngine;

/// <summary>
/// The Player Class holds all the members and functionality for the Player Object.
/// It does not 
/// </summary>
public class Player
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
	/// <param name="health"> Starting Health of the player. </param>
	/// <param name="rotation"> Starting Rotation of the player. </param>
	public Player(int health, float rotation, float thrustPower, float rotationPower, Sprite sprite)
	{
		_health = health;
		_rotation = rotation;
		_thrustPower = thrustPower;
		_rotationPower = rotationPower;
		_sprite = sprite;

		GameObject playerGO = new GameObject();

		playerGO.name = "Player";

		playerGO.AddComponent<Rigidbody2D>();
		_rb2d = playerGO.GetComponent<Rigidbody2D>();

		playerGO.AddComponent<SpriteRenderer>();
		_spriteRenderer = playerGO.GetComponent<SpriteRenderer>();
	}

	/// <summary>
	/// Applies Forward Thrust to the player.
	/// </summary>
	public void Thrust()
	{
		_rb2d.AddForce(_rb2d.transform.up * _thrustPower * Time.deltaTime);
	}

	/// <summary>
	/// Rotates the Player on Input.
	/// </summary>
	/// <param name="rotationAxis"> The Rotation Axis. </param>
	public void Rotate(float rotationAxis)
	{
		_rb2d.MoveRotation(_rb2d.rotation -= rotationAxis * _rotationPower * Time.deltaTime);
	}
}
