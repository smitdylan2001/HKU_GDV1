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
	/// <summary> ICollideable HasCollided Implementation. </summary>
	public bool HasCollided { get; set; }

	/// <summary> Which Layers to check for collision. </summary>
	public LayerMask collisionMask { get; private set; }
	/// <summary> The Sprite of the bullet. </summary>
	public Sprite Sprite { get; private set; }
	/// <summary> Reference to the Rigibody2D component. </summary>
	public SpriteRenderer SpriteRenderer { get; private set; }
	/// <summary> The BoxCollider2D Component of the Bulletss. </summary>
	public BoxCollider2D BoxCollider2D { get; private set; }
	/// <summary> Reference to the Bullet GameObject. </summary>
	public GameObject BulletGO { get; private set; }

	public Bullet(Vector2 pos = new Vector2(), float size = 0.25f, Quaternion rotation = new Quaternion(), float bulletSpeed = 300f)
	{
		Pos = pos;
		Size = size;
		Rotation = rotation;
		BulletSpeed = bulletSpeed;

		Sprite sprite = Resources.Load<Sprite>("Sprites/Bullet");

		BulletGO = new GameObject();
		BulletGO.transform.position = pos;
		BulletGO.transform.rotation = rotation;
		BulletGO.transform.localScale = new Vector3(Size, Size, Size);
		BulletGO.name = "Bullet";

		BoxCollider2D boxCollider2D = BulletGO.AddComponent<BoxCollider2D>();
		BoxCollider2D = boxCollider2D;
		BoxCollider2D.isTrigger = true;
		BoxCollider2D.size = new Vector2(Size, Size);

		SpriteRenderer spriteRenderer = BulletGO.AddComponent<SpriteRenderer>();
		SpriteRenderer = spriteRenderer;
		SpriteRenderer.sprite = sprite;

		collisionMask = ~LayerMask.GetMask("Projectile", "Player");
		BulletGO.layer = LayerMask.NameToLayer("Projectile");

		HasCollided = false;

		CollisionManager.Collideables.Add(this);
	}

	/// <summary>
	/// ICollideable IsColliding Implementation.
	/// </summary>
	public bool IsColliding()
	{
		Collider2D[] collisions = Physics2D.OverlapCircleAll(BulletGO.gameObject.transform.position, Size, collisionMask);

		if(!HasCollided)
		{
			foreach(Collider2D collider in collisions)
			{
				if(collider != this.BoxCollider2D)
				{
					HasCollided = true;
					return true;
				}
			}
		}

		HasCollided = false;
		return false;
	}

	public void OnCollision()
	{
		//TODO:
		// Destroy Bullet on collision.
	}

	public GameObject GetOwner()
	{
		return BulletGO;
	}

}
