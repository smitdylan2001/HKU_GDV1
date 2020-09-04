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
        EventManager.AddListener(EventType.ON_GAME_START, _startGame);
        _startGame += CreatePlayer;
    }

    private void PopulateGamePhysicsEvent()
    {
        Debug.Log("populate physics");
        EventManager.AddListener(EventType.ON_PHYSICS_UPDATE, _physicsUpdate);
        _physicsUpdate += TestMethod;
    }

    private void TestMethod()
    {
        Debug.Log("Physics updated...");

    }

    private void CreatePlayer()
    {
        //TODO: Instantiate player (in scene)
        Debug.Log("Game Started");
    }
}
