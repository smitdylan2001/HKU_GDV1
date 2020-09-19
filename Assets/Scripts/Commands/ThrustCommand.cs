using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrustCommand : IGameObjectCommand
{
    //Movementspeed
	private float _thrustPower = 5f;
	/// <summary>
	/// Move gameobject forward
	/// </summary>
	/// <param name="origin"></param>
	public void Execute(GameObject origin)
	{
		origin.transform.Translate(Vector3.up * _thrustPower * Time.deltaTime);
	}
}
