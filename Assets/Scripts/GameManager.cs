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
    /// <summary> This is the state the game is currently in </summary>
    public static GameState CURRENT_GAME_STATE { get; set; }

    void Start()
    {
        //CURRENT_GAME_STATE = GameState.IS_PLAYING;
        EventManager<GameManager>.AddListener(EventType.ON_GAME_START, OnGameStart);
        EventManager<GameManager>.InvokeEvent(EventType.ON_GAME_START, this);
    }
    
    void Update()
    {
        
    }

    private void OnGameStart(GameManager manager)
    {
        //TODO: Instantiate player (in scene)

    }
}
