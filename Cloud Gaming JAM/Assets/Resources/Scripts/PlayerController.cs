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
        if (state == PlayerMoveState.horizontal)
        {
            return (new Vector2(0, GetMoveDir()));
        }
            
        else if (state == PlayerMoveState.vertical)
        {
            return (new Vector2(GetMoveDir(), 0));
        }
        return Vector2.zero;
    }

    private float GetMoveDir()
    {
        Vector2 newValue = new Vector2();
        newValue = player.GetAxis2D("moveHorizontal", "moveVertical");
        lastInputValue = newValue;
        return Vector2.Distance(lastInputValue, newValue);

        //player.GetAxisDelta("moveHorizontal")
        //return lastInputValue + player.GetAxis("moveHorizontal");
    }
}

public enum PlayerMoveState
{
    horizontal,
    vertical,
    freeze
}
