﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public bool debugControlMode;

    public RaftController[] rafts = new RaftController[2];
    public int nbrRaftInGame = 2;
    public int nbrPlayersJoined;
    [HideInInspector] public int nbrRaftOver;
    [HideInInspector] public int winnerTeam;
    
    [Header("Raft values")]
    [Range(0.5f, 3f)] public float raftSpeedCoef = 1f; 
    [Range(3f, 10f)] public float raftHorizontalSpeedCoef = 1f; 
    public Vector2 maxNormalSpeed;
    public Vector2 maxBoostSpeed;
    [Range(0.97f, 1f)] public float friction;
    [Range(5f, 20f)] public float rawFlowForce;
    
    [Header("Objects values")]
    public float stopZoneTimer;
    [Range(50f, 200f)] public int speedBoost = 50;
    [Range(1.1f, 2.5f)] public float slowCoef = 1.5f;
    [Range(50f, 200f)] public float BumpForce = 100f;
    
    private void Awake()
    {
        if (instance == null)
            Init();
    }

    private void Init()
    {
        instance = this;
        // for (int i = 1; i < rafts.Length + 1; i++)
        // {
        //     rafts[i - 1].teamId = i;
        // }
    }

    private void Update()
    {
        CheckDebuggingControl();  //doesn't work since the new controls/move Loop
    }

    private void ResetValues()
    {
        nbrRaftOver = 0;
        winnerTeam = 0;
    }

    #region GamePreparation

    public void AddNewPlayer(PlayerController newPlayer)
    {
        if (nbrPlayersJoined > 3)
        {
            Debug.Log("Max nbr players reach");
            return;
        }
        if (nbrPlayersJoined == 3) 
            MenuController.instance.LaunchGame();

        MenuController.instance.SetActiveJoinPlayerImage(nbrPlayersJoined);
        nbrPlayersJoined++;
        int teamToJoin = 1;
        if (rafts[0].GetNbrPlayersOnRaft() == 2)
            teamToJoin = 2;
        newPlayer.teamId = teamToJoin;
        Debug.Log("Player " + newPlayer.controllerId + " join team : " +  newPlayer.teamId);
        newPlayer.playerId = nbrPlayersJoined;
        AddPlayerOnRaft(newPlayer, teamToJoin - 1);
    }

    public void RemovePlayer(PlayerController playerToRemove)
    {
        int raftIndex = playerToRemove.teamId - 1;
        rafts[raftIndex].playersOnRaft.Remove(playerToRemove);
        MenuController.instance.SetActiveLeavePlayerImage(playerToRemove.playerId);
        Debug.Log("Player " + playerToRemove.controllerId + " leave team : " +  playerToRemove.teamId);
        playerToRemove.teamId = 0;
        playerToRemove.playerId = 0;
        nbrPlayersJoined--;
    }
    
    private void AddPlayerOnRaft(PlayerController newPlayer, int raftIndex)
    {
        if (rafts[raftIndex].playersOnRaft.Count < 2)
        {
            rafts[raftIndex].playersOnRaft.Add(newPlayer);
            if (rafts[raftIndex].playersOnRaft.Count == 1)
                newPlayer.state = PlayerMoveState.vertical;
        }
    }
    #endregion
    

    #region debugControls

    void CheckDebuggingControl()
    {
        if (GameManager.instance.gameState != GameState.inGame || !debugControlMode) return;

        for (int i = 0; i < 2; i++)
        {
            rafts[i].UpdateRaftForce(GetKeyboardInput(i));
        }
    }

    Vector2 GetKeyboardInput(int raftId) // For debugging
    {
        Vector2 dir = Vector2.zero;
        if (raftId == 0)
        {
            if (ReInput.players.GetSystemPlayer().GetButton("right"))
                dir.x = 1;
            else if (ReInput.players.GetSystemPlayer().GetButton("left"))
                dir.x = -1;

            if (ReInput.players.GetSystemPlayer().GetButton("up"))
                dir.y = 1;
            else if (ReInput.players.GetSystemPlayer().GetButton("down"))
                dir.y = -1;
        }

        else if (raftId == 1)
        {
            if (ReInput.players.GetSystemPlayer().GetButton("A_right"))
                dir.x = 1;
            else if (ReInput.players.GetSystemPlayer().GetButton("A_left"))
                dir.x = -1;

            if (ReInput.players.GetSystemPlayer().GetButton("A_up"))
                dir.y = 1;
            else if (ReInput.players.GetSystemPlayer().GetButton("A_down"))
                dir.y = -1;
        }
        return dir;
    }

    #endregion
}
