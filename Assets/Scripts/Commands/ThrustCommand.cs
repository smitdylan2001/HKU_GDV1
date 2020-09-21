using UnityEngine;

/// <summary>
/// This class is used to move the player forward.
/// </summary>
public class ThrustCommand : IGameObjectCommand
{
	private float _thrustPower = 5f;

	public void Execute(GameObject origin)
	{
		origin.transform.Translate(Vector3.up * _thrustPower * Time.deltaTime);
	}
}
