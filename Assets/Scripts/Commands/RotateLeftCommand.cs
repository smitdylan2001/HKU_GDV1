using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLeftCommand : IGameObjectCommand
{
	private float _rotationPower = 150;
	float Rotation = 1f;

	public void Execute(GameObject origin)
	{
		origin.transform.Rotate(Vector3.forward, _rotationPower * Time.deltaTime);
		//rb.MoveRotation(rb.rotation -= Rotation * _rotationPower * Time.deltaTime);
	}
}
