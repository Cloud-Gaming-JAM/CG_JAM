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
    
    //public RaftController[] rafts = new RaftController[2];
    public PlayerController[] players = new PlayerController[4];
    
    //[Header("Characters")]
    //public GameObject[] characters;
    //[HideInInspector] public List<int> charactersAvailable;

    private void Awake()
    {
        if (instance == null)
            Init();
    }
    
    void Init()
    {
        instance = this;
        // for (int i = 0; i < characters.Length - 1; i++)
        // {
        //     charactersAvailable.Add(i);
        // }
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