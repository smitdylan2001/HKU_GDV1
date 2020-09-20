using System.Collections.Generic;

public static class CollisionManager
{
	public static List<ICollideable> Collideables { get; private set; }

	public static void Init()
	{
		Collideables = new List<ICollideable>();
	}

	public static void Update()
	{
		foreach(ICollideable collideable in Collideables)
		{
			if(collideable.IsColliding())
			{
				collideable.OnCollision();
			}
		}
	}
}
