using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerController : MonoBehaviour
{
    public int teamId; //0 = no team, 1 = first team
    public int playerId;
    public Player player;
    public PlayerMoveState state;
    
    private Vector2 lastInputValue;

    private void Awake()
    {
        player = ReInput.players.GetPlayer(playerId);
    }

    private void Update()
    {
        CheckInputToJoin();
    }

    private void CheckInputToJoin()
    {
        if (teamId == 0 && player.GetButtonDown("select"))
        {
            player.GetButton("select");
            LevelManager.instance.AddNewPlayer(playerId);
        }
    }
    
    public void SetTeamID(int newTeam)
    {
        teamId = newTeam;
    }

    public void SetPlayerState(PlayerMoveState state)
    {
        this.state = state;
    }

    public Vector2 GetPlayerInput()
    {
        Vector2 dir = Vector2.zero;
        if (state == PlayerMoveState.horizontal)
        {
            dir.x = player.GetAxis("moveHorizontal");

            //Debug.Log(dir.x);
            //return (new Vector2(0, GetMoveDir()));
        }
        
        else if (state == PlayerMoveState.vertical) // antihoraire pour monter
        {
            dir.y = player.GetAxis("moveVertical");
            //return (new Vector2(GetMoveDir(), 0));
        }
        return dir;
    }
    private float GetMoveDir() // for roll joystick control - WIP
    {
        Vector2 newValue = new Vector2();
        newValue = player.GetAxis2D("moveHorizontal", "moveVertical");
        lastInputValue = newValue;
        return Vector2.Distance(lastInputValue, newValue);

        //player.GetAxisDelta("moveHorizontal");
        //return lastInputValue + player.GetAxis("moveHorizontal");
    }
}

public enum PlayerMoveState
{
    horizontal,
    vertical,
    freeze
}
