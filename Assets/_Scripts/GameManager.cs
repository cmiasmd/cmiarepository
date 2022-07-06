using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    public GameState State;

    public static event Action<GameState> OnGameStateChanged;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        updateGameState(GameState.INTRO);
    }

    public void Pause(bool paused)
    {
        //if (paused)
        //{
        //    // pause the game/physic
        //    Time.time = 0.0f;
        //}
        //else
        //{
        //    // resume
        //    Time.time = 1.0f;
        //}
    }

    public void updateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.INTRO:
                break;
            case GameState.INVESTIGATE:
                break;
            case GameState.SUCESS:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            case GameState.FAIL:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }

    public enum GameState
    {
        INTRO,
        INVESTIGATE,
        SUCESS,
        FAIL
    }
}
