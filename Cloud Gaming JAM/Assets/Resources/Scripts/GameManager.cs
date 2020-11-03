using System;
using System.Collections;
using System.Collections.Generic;
using Rewired;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameState gameState;

    public PlayerController[] player = new PlayerController[4];

    private void Awake()
    {
        if (instance == null)
            Init();
    }
    void Init()
    {
        instance = this;
    }

    void AddNewPlayer()
    {
        
    }

    public int GetNbrController()
    {
        Debug.Log("there is " + ReInput.controllers.joystickCount + " joy controllers");
        return ReInput.controllers.joystickCount;
    }

    public void SetGameState(GameState newState)
    {
        gameState = newState;
    }
}

public enum GameState
{
    inMenu,
    inGame,
    pause
}