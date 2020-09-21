using System.Collections.Generic;
using System.Linq;

public static class CollisionManager
{
	public static List<ICollideable> Collideables { get; private set; }

	public static void Init()
	{
		Collideables = new List<ICollideable>();
	}

	public static void Update()
	{
		foreach(ICollideable collideable in Collideables.ToList())
		{
			if(collideable.IsColliding())
			{
				collideable.OnCollision();
			}
		}
	}
}

//TODO
// Make it so that the actual collision check happens here, and individual classes can call this class and check for their collision.
// Right now this works as expected, but it's not that great making every ICollideable in the scene check collision individually.
