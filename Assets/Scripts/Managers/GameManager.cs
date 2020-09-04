using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This is the only monobehaviour in the game
/// </summary>

public enum GameState
{
	IS_PLAYING = 0,
	IS_PAUSED = 1,
	IS_GAMEOVER = 2
}

public class GameManager : MonoBehaviour
{
	public System.Action _startGame;
	public System.Action _physicsUpdate;

	/// <summary> This is the state the game is currently in </summary>
	public static GameState CURRENT_GAME_STATE { get; set; }

	public List<IRigidBody> rigidBodies = new List<IRigidBody>();

	/// <summary> Player Instance Reference. </summary>
	public Player _player { get; private set; }

	void Start()
	{
		Debug.Log("UnityEngine start");
		PopulateGameStartEvent();
		PopulateGamePhysicsEvent();
		EventManager.InvokeEvent(EventType.ON_GAME_START);
	}

	void FixedUpdate()
	{
		Debug.Log("UnityEngine fixedupdate");
		EventManager.InvokeEvent(EventType.ON_PHYSICS_UPDATE);
	}

	private void PopulateGameStartEvent()
	{
		Debug.Log("populate start");
		_startGame += CreatePlayer;
		EventManager.AddListener(EventType.ON_GAME_START, _startGame);
	}

	private void PopulateGamePhysicsEvent()
	{
		Debug.Log("populate physics");
		_physicsUpdate += TestMethod;
		EventManager.AddListener(EventType.ON_PHYSICS_UPDATE, _physicsUpdate);
	}

	private void TestMethod()
	{
		Debug.Log("Physics updated...");

	}

	private void CreatePlayer()
	{
		Debug.Log("Player Created!");
		_player = new Player();
	}
}
