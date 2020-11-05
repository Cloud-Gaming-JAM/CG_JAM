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
    public JoyDir lastJoyDir;
    public JoyDir currentJoyDir;
    public Vector2[] joyDirVectors = new Vector2[4];

    public bool debuggMode;
    

    private void Awake()
    {
        player = ReInput.players.GetPlayer(playerId);
        joyDirVectors[0] = Vector2.up;
        joyDirVectors[1] = Vector2.right;
        joyDirVectors[2] = Vector2.down;
        joyDirVectors[3] = Vector2.left;
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
        if (state == PlayerMoveState.horizontal && debuggMode)
        {
            Vector2 dirBrut = new Vector2(player.GetAxis("moveHorizontal"), player.GetAxis("moveVertical"));
            if ((dirBrut.x < 0.25f && dirBrut.x > -0.25f) || (dirBrut.y < 0.25f && dirBrut.y > -0.25f)) return dir;
            
            currentJoyDir = (JoyDir)GetJoyDir(dirBrut);
            dir = GetFinalDir();
            Debug.Log("FinalDir " + dir);
            lastJoyDir = currentJoyDir;
        }
        
        else if (state == PlayerMoveState.vertical)
        {
            dir.y = player.GetAxis("moveVertical");
        }
        return dir;
    }

    int GetJoyDir(Vector2 dir)
    {
        for (int i = 0; i < 4; i++)
        {
            if (Vector2.Angle(dir, joyDirVectors[i]) < 45f)
                return i;
        }
        return 0;
        //int finalJoyDir = 0;
        // float lastDiff = Vector2.Angle(dir, joyDirVectors[0]);
        // for (int i = 1; i < 4; i++)
        // {
        //     float diff = Vector2.Angle(dir, joyDirVectors[i]);
        //     if (diff < lastDiff)
        //         finalJoyDir = i;
        //     lastDiff = diff;
        // }
        //return finalJoyDir;
    }
    
    private Vector2 GetFinalDir()
    {
        Vector2 finalDir = Vector2.zero;
        
        if (currentJoyDir - lastJoyDir == 1 || (currentJoyDir == JoyDir.up && lastJoyDir == JoyDir.left))
        {
            finalDir = Vector2.right;
        }
        else if (currentJoyDir - lastJoyDir == -1 || (lastJoyDir == JoyDir.up && currentJoyDir == JoyDir.left))
        {
            finalDir = Vector2.left;
        }
        return finalDir;
    }

    
    // private float GetMoveDir() // for roll joystick control - previous try
    // {
    //     Vector2 newValue = new Vector2();
    //     newValue = player.GetAxis2D("moveHorizontal", "moveVertical");
    //     lastInputValue = newValue;
    //     return Vector2.Distance(lastInputValue, newValue);
    //
    //     //player.GetAxisDelta("moveHorizontal");
    //     //return lastInputValue + player.GetAxis("moveHorizontal");
    // }
}

public enum PlayerMoveState
{
    horizontal,
    vertical,
    freeze
}

public enum JoyDir
{
    up,
    right,
    down,
    left
}
