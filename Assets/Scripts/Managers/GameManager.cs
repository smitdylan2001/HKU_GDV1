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
		
		CollisionManager.Init();
		ProjectileManager.Init();

		PopulateGameStartEvent();

		EventManager.InvokeEvent(EventType.ON_GAME_START);

		SetAndPopulateInput();

		// Populate physics and Logic last because we need all the start objects to be initialized first before we can call their functions.
		// a.k.a. Null pointer when this isn't placed as last on Start.
		PopulateGameLogicUpdateEvent();
		PopulateGamePhysicsEvent();
		AddUIEventListeners();

		UIManager.FindCanvas();
		UIManager.AddTextUIElement(new Vector3(0,460),"0","scoreText", UIManager.Canvas.transform, 30f);
	}
	private void Update()
	{
		EventManager.InvokeEvent(EventType.ON_LOGIC_UPDATE);

		ProjectileManager.Update();

		inputManager.HandleInput();
	}

	void FixedUpdate()
	{
		CollisionManager.Update();
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

		EventManager.AddListener(EventType.ON_LOGIC_UPDATE, _logicUpdate);
	}

	private void PopulateGamePhysicsEvent()
	{
		_physicsUpdate += AsteroidsManager.PhysicsUpdate;

		EventManager.AddListener(EventType.ON_PHYSICS_UPDATE, _physicsUpdate);
	}

	private void CreatePlayer()
	{
		Player = new Player();
		CollisionManager.Collideables.Add(Player);
	}

	private void CreateAsteroidSpawner()
	{
		AsteroidsManager = new AsteroidsManager();
	}

	private void AddUIEventListeners()
	{
		EventManager<int>.AddListener(EventType.ON_UI_UPDATE,ScoreManager.AddScore);
	}

	private void SetAndPopulateInput()
	{
		inputManager = new InputManager();
		ShootCommand shootCommand = new ShootCommand();
		ThrustCommand thrustCommand = new ThrustCommand();
		RotateLeftCommand rotateLeftCommand = new RotateLeftCommand();
		RotateRightCommand rotateRightCommand = new RotateRightCommand();
		inputManager.BindInputToCommandWithOrigin(KeyCode.Space, shootCommand, Player.PlayerGO);
		inputManager.BindInputToCommandWithOriginDown(KeyCode.W, thrustCommand, Player.PlayerGO);
		inputManager.BindInputToCommandWithOriginDown(KeyCode.A, rotateLeftCommand, Player.PlayerGO);
		inputManager.BindInputToCommandWithOriginDown(KeyCode.D, rotateRightCommand, Player.PlayerGO);
	}
}
