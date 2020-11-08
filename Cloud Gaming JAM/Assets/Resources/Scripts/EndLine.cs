using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLine : MonoBehaviour
{
    //List<PlayerController> playersFinished = new List<PlayerController>();

    void OnTriggerEnter2D(Collider2D raft)
    {
        Debug.Log("Raft has finished");
        if (LevelManager.instance.nbrRaftOver == 0)
            LevelManager.instance.winnerTeam = raft.GetComponent<RaftController>().teamId;

        LevelManager.instance.nbrRaftOver++;

        if (LevelManager.instance.nbrRaftInGame == LevelManager.instance.nbrRaftOver)
            EndGame();
    }

    void EndGame()
    {
        EndScreen.instance.EnableEndScreen(LevelManager.instance.winnerTeam);
        //Time.timescale = 0f;
        Debug.Log("The winners are ... team " + LevelManager.instance.winnerTeam);
    }
}
