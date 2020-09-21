using System.Collections.Generic;
using System.Linq;

public static class CollisionManager
{
	public static List<ICollideable> Collideables { get; private set; }

	private static List<ICollideable> _onCollisionColliders;

	public static void Init()
	{
		Collideables = new List<ICollideable>();
		_onCollisionColliders = new List<ICollideable>();
	}

	public static void Update()
	{
		foreach(ICollideable collideable in Collideables.ToList())
		{
			if(collideable.IsColliding())
			{
				_onCollisionColliders.Add(collideable);
			}
		}

		foreach(ICollideable onCollisionCollideable in _onCollisionColliders.ToList())
		{
			onCollisionCollideable.OnCollision();
		}

		_onCollisionColliders.Clear();
	}
}

//TODO
// Make it so that the actual collision check happens here, and individual classes can call this class and check for their collision.
// Right now this works as expected, but it's not that great making every ICollideable in the scene check collision individually.
