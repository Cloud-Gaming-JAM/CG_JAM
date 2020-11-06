using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLine : MonoBehaviour
{
    //List<PlayerController> playersFinished = new List<PlayerController>();

    void OnTrigger2DEnter(Collider2D raft)
    {
        if (LevelManager.instance.nbrRaftOver == 0)
            LevelManager.instance.winnerTeam = raft.GetComponent<RaftController>().teamId;

        LevelManager.instance.nbrRaftOver++;

        if (LevelManager.instance.nbrRaftInGame == LevelManager.instance.nbrRaftOver)
            EndGame();
    }

    void EndGame(/*int WinnerID*/)
    {
        GetComponent<EndScreen>().EnableEndScreen(LevelManager.instance.winnerTeam);
        //Time.timescale = 0f;
        Debug.Log("The winners are ... team " + LevelManager.instance.winnerTeam);
    }
}
