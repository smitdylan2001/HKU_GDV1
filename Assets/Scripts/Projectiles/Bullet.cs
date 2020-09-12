using System.Linq;
using UnityEditor.Experimental.AssetImporters;
using UnityEngine;

public class Bullet : ICollideable, IProjectile
{
	/// <summary> Starting Position of the bullet. </summary>
	public Vector2 Pos { get; private set; }
	/// <summary> Size of the Player. </summary>
	public float Size { get; private set; }
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
	/// <summary> The BoxCollider2D Component of the Bulletss. </summary>
	public BoxCollider2D BoxCollider2D { get; private set; }

	public Bullet(Vector2 pos = new Vector2(), float size = 0.25f, Quaternion rotation = new Quaternion(), float bulletSpeed = 300f)
	{
		Pos = pos;
		Size = size;
		Rotation = rotation;
		BulletSpeed = bulletSpeed;

		Sprite sprite = Resources.Load<Sprite>("Sprites/Bullet");

		GameObject projectileGO = new GameObject();
		projectileGO.transform.position = pos;
		projectileGO.transform.rotation = rotation;
		projectileGO.transform.localScale = new Vector3(Size, Size, Size);
		projectileGO.name = "Bullet";

		BoxCollider2D boxCollider2D = projectileGO.AddComponent<BoxCollider2D>();
		BoxCollider2D = boxCollider2D;
		BoxCollider2D.isTrigger = true;
		BoxCollider2D.size = new Vector2(Size, Size);

		Rigidbody2D rb2d = projectileGO.AddComponent<Rigidbody2D>();
		Rb2d = rb2d;
		Rb2d.transform.position = pos;
		Rb2d.AddForce(Rb2d.transform.up * BulletSpeed);

		SpriteRenderer spriteRenderer = projectileGO.AddComponent<SpriteRenderer>();
		SpriteRenderer = spriteRenderer;
		SpriteRenderer.sprite = sprite;
	}

	public void OnCollision()
	{
		Collider2D[] collisions = Physics2D.OverlapCircleAll(Rb2d.gameObject.transform.position, Size);

		foreach(Collider2D collider in collisions)
		{
			if(collider != this.BoxCollider2D)
			{
				Debug.Log("Hit: " + collider.name);
				collider.GetComponent<IDamageable<int>>()?.Damage(25);
			}
		}
	}

	public GameObject GetOwner()
	{
		return Rb2d.gameObject;
	}
}
