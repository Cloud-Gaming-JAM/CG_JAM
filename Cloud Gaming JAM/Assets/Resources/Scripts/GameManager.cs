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

    private void Update()
    {
        if (gameState == GameState.inGame)
            CheckRaftBehindAndResetPos();
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

    void CheckRaftBehindAndResetPos()
    {
        Vector3 cameraPos = Camera.main.transform.position;
        foreach (RaftController raft in LevelManager.instance.rafts)
        {
            Debug.Log("raft pos : " + raft.transform.position + "  camera delta " + (cameraPos.x -11));
            if (raft.transform.position.x < (cameraPos.x - 11))
                raft.transform.position = new Vector3(cameraPos.x - 5, cameraPos.y, 0);
        }
    }
}

public enum GameState
{
    inMenu,
    inGame,
    pause
}