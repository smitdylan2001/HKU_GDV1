using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
	IS_PLAYING = 0,
	IS_PAUSED = 1,
	IS_GAMEOVER = 2
}

/// <summary>
/// This is the only monobehaviour in the game
/// </summary>
public class GameManager : MonoBehaviour
{
	public System.Action _startGame;
	public System.Action _logicUpdate;
	public System.Action _physicsUpdate;

	/// <summary> This is the state the game is currently in </summary>
	public static GameState CURRENT_GAME_STATE { get; set; }

	/// <summary> Player Instance Reference. </summary>
	public Player _player { get; private set; }

	void Start()
	{
		Debug.Log("UnityEngine start");
		PopulateGameStartEvent();

		EventManager.InvokeEvent(EventType.ON_GAME_START);

		// Populate physics and Logic last because we need all the start objects to be initialized first before we can call their functions.
		// a.k.a. Null pointer when this isn't placed as last on Start.
		PopulateGameLogicUpdateEvent();
		PopulateGamePhysicsEvent();
	}

	private void Update()
	{
		EventManager.InvokeEvent(EventType.ON_LOGIC_UPDATE);
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

	private void PopulateGameLogicUpdateEvent()
	{
		Debug.Log("populate Logic Update");

		_logicUpdate += _player.OnInput;

		EventManager.AddListener(EventType.ON_LOGIC_UPDATE, _logicUpdate);
	}

	private void PopulateGamePhysicsEvent()
	{
		Debug.Log("populate physics");

		_physicsUpdate += _player.PhysicsUpdate;

		EventManager.AddListener(EventType.ON_PHYSICS_UPDATE, _physicsUpdate);
	}

	private void CreatePlayer()
	{
		Debug.Log("Player Created!");
		
		_player = new Player();
	}
}
