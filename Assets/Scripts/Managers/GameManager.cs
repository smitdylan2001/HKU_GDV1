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

	/// <summary> Player Instance. </summary>
	public Player Player { get; private set; }

	/// <summary> AsteroidsManager Instance. </summary>
	public AsteroidsManager AsteroidsManager { get; private set; }

	/// <summary> The inputManager that handles all playerinput (i.e. moving or shooting) </summary>
	public InputManager inputManager { get; private set; }
	void Start()
	{
		PopulateGameStartEvent();
		EventManager.InvokeEvent(EventType.ON_GAME_START);
		SetAndPopulateInput();
		// Populate physics and Logic last because we need all the start objects to be initialized first before we can call their functions.
		// a.k.a. Null pointer when this isn't placed as last on Start.
		PopulateGameLogicUpdateEvent();
		PopulateGamePhysicsEvent();
	}

	private void Update()
	{
		EventManager.InvokeEvent(EventType.ON_LOGIC_UPDATE);
		inputManager.HandleInput();
	}

	void FixedUpdate()
	{

		EventManager.InvokeEvent(EventType.ON_PHYSICS_UPDATE);
	}

	private void PopulateGameStartEvent()
	{

		_startGame += CreatePlayer;
		_startGame += CreateAsteroidSpawner;

		EventManager.AddListener(EventType.ON_GAME_START, _startGame);
	}

	private void PopulateGameLogicUpdateEvent()
	{

		inputManager.HandleInput();
		_logicUpdate += Player.OnCollision;

		EventManager.AddListener(EventType.ON_LOGIC_UPDATE, _logicUpdate);
	}

	private void PopulateGamePhysicsEvent()
	{

		_physicsUpdate += Player.PhysicsUpdate;
		_physicsUpdate += AsteroidsManager.PhysicsUpdate;

		EventManager.AddListener(EventType.ON_PHYSICS_UPDATE, _physicsUpdate);
	}

	private void CreatePlayer()
	{
		Player = new Player();
	}

	private void CreateAsteroidSpawner()
	{
		AsteroidsManager = new AsteroidsManager();
	}

	private void SetAndPopulateInput()
	{
		inputManager = new InputManager();
		var shootCommand = new ShootCommand();
		var thrustCommand = new ThrustCommand();
		inputManager.BindInputToCommandWithOrigin(KeyCode.Space, shootCommand, Player.Rb2d.gameObject);
		inputManager.BindInputToCommandWithOriginDown(KeyCode.W, thrustCommand, Player.Rb2d.gameObject);
	}
}
