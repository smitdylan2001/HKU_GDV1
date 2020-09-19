using System.Collections.Generic;
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
	/// <summary> The BoxCollider2D Component of the Bullet. </summary>
	public BoxCollider2D BoxCollider2D { get; private set; }

	/// <summary> The player gameobject for other classes to get</summary>
	public GameObject PlayerGO { get; private set; }


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
		PlayerGO = new GameObject();
		PlayerGO.name = "Player";
		PlayerGO.transform.localScale = new Vector3(Size, Size, Size);

		Rigidbody2D rb2d = PlayerGO.AddComponent<Rigidbody2D>();
		Rb2d = rb2d;

		BoxCollider2D boxCollider2D = PlayerGO.AddComponent<BoxCollider2D>();
		BoxCollider2D = boxCollider2D;
		BoxCollider2D.size = new Vector2(Size, Size);
		BoxCollider2D.isTrigger = true;

		SpriteRenderer spriteRenderer = PlayerGO.AddComponent<SpriteRenderer>();
		SpriteRenderer = spriteRenderer;
		SpriteRenderer.sprite = sprite;
	}
	/// <summary>
	/// IRigidBody Implementation.
	/// </summary>
	public void PhysicsUpdate()
	{

	}
	/// <summary>
	/// IPlayable OnInput Implemention.
	/// </summary>
	public void LogicUpdate()
	{
	}

	/// <summary>
	/// ICollideable OnCollision Implementation.
	/// </summary>
	public void OnCollision()
	{
		Collider2D[] collisions = Physics2D.OverlapCircleAll(Rb2d.gameObject.transform.position, Size);

		foreach (Collider2D collider in collisions)
		{
			if (collider != this.BoxCollider2D)
			{
				//Debug.Log(Rb2d.transform.name + " Hit " + collider.name);
				collider.GetComponent<IDamageable<int>>()?.Damage(1);
				Damage(1);
			}
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
}
