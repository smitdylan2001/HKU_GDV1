/// <summary>
/// IDamgeable gets used by classes that have a Health member. I.E. Player and Asteroid.
/// </summary>
/// <typeparam name="T"> Generic type. You can use multiple types. I.E. Integer, Float, Double, etc. </typeparam>
public interface IDamageable<T>
{
	void Damage(T damageTaken);
}
