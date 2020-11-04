using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public RaftController[] rafts = new RaftController[2];
    public int nbrRaftInGame;
    [HideInInspector] public int nbrRaftOver;
    [HideInInspector] public int winnerTeam;

    [Header("Tweaks values")]
    [Range(0.2f, 5f)] public float raftSpeedMultiplier = 1f; //Raft's maximum speed, values below 1 will slow down the raft
    public float stopZoneTimer;
    public float speedBoost;
    
    private void Awake()
    {
        if (instance == null)
            Init();
    }

    private void Init()
    {
        instance = this;
        for (int i = 1; i < rafts.Length + 1; i++)
        {
            rafts[i - 1].teamId = i;
        }
    }

    private void Update()
    {
        CheckDebuggingControl();
    }

    private void ResetValues()
    {
        nbrRaftOver = 0;
        winnerTeam = 0;
    }
    
    public void AddNewPlayer(int playerId)
    {
        int teamToJoin = 1;
        if (rafts[0].GetNbrPlayersOnRaft() == 2)
            teamToJoin = 2;
        GameManager.instance.players[playerId].teamId = teamToJoin;
        Debug.Log(GameManager.instance.players[playerId].teamId);
        AddPlayerOnRaft(playerId, teamToJoin-1);
    }
    private void AddPlayerOnRaft(int playerId, int teamId)
    {
        if (rafts[teamId].playersOnRaft.Count < 2)
            rafts[teamId].playersOnRaft.Add(GameManager.instance.players[playerId]);
    }

    #region debugControls

    void CheckDebuggingControl()
    {
        if (GameManager.instance.gameState != GameState.inGame) return;
        
        Vector2 dir = GetKeyboardInput() * raftSpeedMultiplier;
        rafts[0].raftRigidBody.velocity = dir;
    }
    
    Vector2 GetKeyboardInput() // For debugging
    {
        Vector2 dir = Vector2.zero;
        
        if (Input.GetKey(KeyCode.D))
            dir.x = 1;
        else if (Input.GetKey(KeyCode.Q))
            dir.x = -1;
        
        if (Input.GetKey(KeyCode.Z))
            dir.y = 1;
        else if (Input.GetKey(KeyCode.S))
            dir.y = -1;

        return dir;
    }

    #endregion
}
