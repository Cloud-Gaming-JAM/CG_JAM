using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        other.attachedRigidbody.velocity /= LevelManager.instance.slowCoef;
        other.GetComponent<RaftController>().raftSpeedMultiplier /= LevelManager.instance.slowCoef;
        Debug.Log("SlowSpeed ");
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        other.GetComponent<RaftController>().raftSpeedMultiplier = LevelManager.instance.raftSpeedCoef;
    }
}
