using System;
using System.Collections;
using System.Collections.Generic;
using Rewired;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameState gameState;
    
    public RaftController[] rafts = new RaftController[2];
    public PlayerController[] players = new PlayerController[4];

    private void Awake()
    {
        if (instance == null)
            Init();
    }
    void Init()
    {
        instance = this;
    }

    public void AddNewPlayer(int playerId, int teamToJoin)
    {
        players[playerId].teamId = teamToJoin;
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