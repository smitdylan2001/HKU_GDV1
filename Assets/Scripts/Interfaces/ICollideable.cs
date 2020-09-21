/// <summary>
/// ICollideable is implemented by classes that should make use of collision. I.E. Player, Bullet and Asteroid.
/// </summary>
public interface ICollideable
{
	bool HasCollided { get; set; }
	bool IsColliding();
	void OnCollision();
}
