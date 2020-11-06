using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPlayers : MonoBehaviour
{
    int[] raftEntered = new int[2];
    int nbrRaftEntered;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || nbrRaftEntered >= 2) return;

        RaftController raftController = other.GetComponent<RaftController>();
        if (raftController.teamId == raftEntered[0] || raftController.teamId == raftEntered[1]) return;
        
        raftEntered[nbrRaftEntered] = raftController.teamId;
        nbrRaftEntered++;
        raftController.SwitchPlayers();
    }
}
