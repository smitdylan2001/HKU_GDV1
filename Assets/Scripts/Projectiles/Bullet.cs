using UnityEngine;

public class Bullet
{
	/// <summary> Starting Position of the bullet. </summary>
	public Vector2 Pos { get; private set; }
	/// <summary> The rotation of the Bullet. (We use a float because only 1 axis has to be rotated.) </summary>
	public Quaternion Rotation { get; private set; }
	/// <summary> The speed at which the bullet travels. </summary>
	public float BulletSpeed { get; private set; }
	/// <summary> The Sprite of the bullet. </summary>
	public Sprite Sprite { get; private set; }

	/// <summary> Reference to the Rigibody2D component. </summary>
	public Rigidbody2D Rb2d { get; private set; }
	/// <summary> Reference to the SpriteRenderer Component. </summary>
	public SpriteRenderer SpriteRenderer { get; private set; }

	public Bullet(Vector2 pos, Quaternion rotation, float bulletSpeed)
	{
		Pos = pos;
		Rotation = rotation;
		BulletSpeed = bulletSpeed;

		Sprite sprite = Resources.Load<Sprite>("Sprites/Bullet");

		GameObject projectileGO = new GameObject();
		projectileGO.transform.position = pos;
		projectileGO.transform.rotation = rotation;
		projectileGO.name = "Bullet";

		Rigidbody2D rb2d = projectileGO.AddComponent<Rigidbody2D>();
		Rb2d = rb2d;
		Rb2d.AddForce(Rb2d.transform.up * bulletSpeed);

		SpriteRenderer spriteRenderer = projectileGO.AddComponent<SpriteRenderer>();
		SpriteRenderer = spriteRenderer;
		SpriteRenderer.sprite = sprite;
	}
}
