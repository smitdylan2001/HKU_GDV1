public interface ICollideable
{
	bool HasCollided { get; set; }
	bool IsColliding();
	void OnCollision();
}
