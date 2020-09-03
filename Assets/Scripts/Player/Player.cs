using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
	/// <summary> The Health member of the player. </summary>
	public int _health { get; private set; }
	/// <summary> The Rotation member of the player </summary>
	public float _rotation { get; private set; }

	/// <summary>
	/// Constructor of the Player Class.
	/// </summary>
	/// <param name="health"> Starting Health of the player. </param>
	/// <param name="rotation"> Starting Rotation of the player. </param>
	public Player(int health, float rotation)
	{
		_health = health;
		_rotation = rotation;
	}

	public void Thrust(KeyCode thrustKey)
	{
		//TODO
		// Implement thrusting code. (hehe)
	}

	public void Rotate(KeyCode rotationKey)
	{
		//TODO
		// Implement Rotation.
	}
}
