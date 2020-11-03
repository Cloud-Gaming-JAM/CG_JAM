using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerController
{
    public int playerId;
    public Player player;
    public PlayerMoveState state;
    
    private Vector2 lastInputValue;

    // public PlayerController(int id)
    // {
    //     idPlayer = id;
    // }

    public void SetPlayerID(int id)
    {
        playerId = id;
        player = ReInput.players.GetPlayer(playerId);
    }

    public void SetPlayerState(PlayerMoveState state)
    {
        this.state = state;
    }

    public Vector2 GetPlayerInput()
    {
        Vector2 dir = new Vector2(0,0);
        if (state == PlayerMoveState.horizontal)
        {
            dir.x = player.GetAxis("moveHorizontal");
            //return (new Vector2(0, GetMoveDir()));
        }
        
        else if (state == PlayerMoveState.vertical) // antihoraire pour monter
        {
            dir.y = player.GetAxis("moveVertical");
            //return (new Vector2(GetMoveDir(), 0));
        }
        
        Debug.Log(dir);
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
