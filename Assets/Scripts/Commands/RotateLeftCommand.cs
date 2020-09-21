using UnityEngine;

/// <summary>
/// This command is used to Rotate the Player Left.
/// </summary>
public class RotateLeftCommand : IGameObjectCommand
{
	private float _rotationPower = 150;
	public void Execute(GameObject origin)
	{
		origin.transform.Rotate(Vector3.forward, _rotationPower * Time.deltaTime);
	}
}
