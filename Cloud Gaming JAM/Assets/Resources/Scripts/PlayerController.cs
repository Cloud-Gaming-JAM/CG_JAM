using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Rewired;

public class PlayerController : MonoBehaviour
{
    public int teamId; //0 = no team, 1 = first team
    public int playerId; // playerId in game (team 1 = player 1 + player 2)
    public int controllerId; // controllers in order of plug
    public Player player;
    public PlayerMoveState state;
    public JoyDir lastJoyDir;
    public JoyDir currentJoyDir;
    public Vector2[] joyDirVectors = new Vector2[4];

    private void Awake()
    {
        player = ReInput.players.GetPlayer(controllerId);
        joyDirVectors[0] = Vector2.up;
        joyDirVectors[1] = Vector2.right;
        joyDirVectors[2] = Vector2.down;
        joyDirVectors[3] = Vector2.left;
    }

    private void Update()
    {
        HasToJoinOrQuit();
        if (playerId == 1 && player.GetButtonDown("start") && SceneManager.GetActiveScene().buildIndex == 1)
        {
            PauseMenu.instance.DisplayPauseMenu();
        }
    }

    private void HasToJoinOrQuit()
    {
        if (GameManager.instance.gameState != GameState.inMenu || !MenuController.instance.isInScreenPlayerSelection) return;

        if (teamId == 0 && player.GetButtonDown("select"))
            LevelManager.instance.AddNewPlayer(this);

        else if (teamId > 0 && player.GetButtonDown("back"))
            LevelManager.instance.RemovePlayer(this);
    }

    public Vector2 GetMovePlayerInput()
    {
        Vector2 dir = Vector2.zero;
        if (state == PlayerMoveState.horizontal)
        {
            Vector2 dirBrut = new Vector2(player.GetAxis("moveHorizontal"), player.GetAxis("moveVertical"));
            if ((dirBrut.x < 0.25f && dirBrut.x > -0.25f) || (dirBrut.y < 0.25f && dirBrut.y > -0.25f)) return dir;

            currentJoyDir = (JoyDir)GetJoyDir(dirBrut);
            dir = GetFinalDir();
            //Debug.Log("FinalDir " + dir);
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
    
    public void VibrateController()
    {
        // Set vibration in all Joysticks assigned to the Player
        int motorIndex = 0; // the first motor
        float motorLevel = 1.0f; // full motor speed
        float duration = 0.5f; // 2 seconds

        player.SetVibration(motorIndex, motorLevel, duration);
    }
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
