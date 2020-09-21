/// <summary>
/// Simple Generic type Interface to add score to the ScoreManager.
/// </summary>
/// <typeparam name="T"> Generic type. You can use multiple types. I.E. Integer, Float, Double, etc. </typeparam>
public interface IScoreable<T>
{
	void AddScore(T scoreToAdd);
}
