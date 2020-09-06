using UnityEngine;

public class Bullet
{
	/// <summary> Starting Position of the bullet. </summary>
	public Vector2 _pos { get; private set; }
	/// <summary> The rotation of the Bullet. (We use a float because only 1 axis has to be rotated.) </summary>
	public float _rotation { get; private set; }
	/// <summary> The speed at which the bullet travels. </summary>
	public float _bulletSpeed { get; private set; }
	/// <summary> The Sprite of the bullet. </summary>
	public Sprite _sprite { get; private set; }

	/// <summary> Reference to the Rigibody2D component. </summary>
	public Rigidbody2D _rb2d { get; private set; }
	/// <summary> Reference to the SpriteRenderer Component. </summary>
	public SpriteRenderer _spriteRenderer { get; private set; }

	public Bullet(Vector2 pos = default, float rotation = 0f, float bulletSpeed = 300f, Sprite sprite = default)
	{
		_pos = pos;
		_rotation = rotation;
		_bulletSpeed = bulletSpeed;
		_sprite = sprite;

		GameObject projectileGO = new GameObject();
		projectileGO.name = "Bullet";

		Rigidbody2D rb2d = projectileGO.AddComponent<Rigidbody2D>();
		_rb2d = rb2d;
		_rb2d.SetRotation(rotation);
		_rb2d.AddForce(_rb2d.transform.up * bulletSpeed);

		SpriteRenderer spriteRenderer = projectileGO.AddComponent<SpriteRenderer>();
		_spriteRenderer = spriteRenderer;
	}
}
