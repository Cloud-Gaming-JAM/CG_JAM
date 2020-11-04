using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        other.GetComponent<RaftController>().raftSpeedMultiplier /= 2;
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        other.GetComponent<RaftController>().raftSpeedMultiplier = LevelManager.instance.raftSpeedMultiplier;
    }
}
