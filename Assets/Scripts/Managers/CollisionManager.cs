using System.Collections.Generic;
using System.Linq;

/// <summary>
/// The CollisionManager is responsible for handling the collisions between all the ICollideable within the scene.
/// </summary>
public static class CollisionManager
{
	/// <summary> List that holds all the ICollideable within the scene. </summary>
	public static List<ICollideable> COLLIDEABLES;

	/// <summary> 
	/// A seperate list that holds all the ICollideables that return true on their 'IsColliding' methods. 
	/// These ICollideables will then be called after all the collisions have happened as to negate possible errors.
	/// </summary>
	private static List<ICollideable> ON_COLLISION_COLLIDEABLES;

	/// <summary> Initialize the Collision Manager Properties and Methods. </summary>
	public static void Init()
	{
		COLLIDEABLES = new List<ICollideable>();
		ON_COLLISION_COLLIDEABLES = new List<ICollideable>();
	}

	/// <summary> Update function that get's called every frame within the Game Manager. </summary>
	public static void Update()
	{
		// Get all the Collisions that happen within the current frame.
		foreach(ICollideable collideable in COLLIDEABLES.ToList())
		{
			if(collideable.IsColliding())
			{
				ON_COLLISION_COLLIDEABLES.Add(collideable);
			}
		}

		// Call the OnCollision methods for all the collisions that happened.
		if(ON_COLLISION_COLLIDEABLES.Count > 0)
		{
			foreach(ICollideable onCollisionCollideable in ON_COLLISION_COLLIDEABLES.ToList())
			{
				onCollisionCollideable.OnCollision();
			}
			ON_COLLISION_COLLIDEABLES.Clear();
		}
	}
}

//TODO
// Make it so that the actual collision check happens here, and individual classes can call this class and check for their collision.
// Right now this works as expected, but it's not that great making every ICollideable in the scene check collision individually.
