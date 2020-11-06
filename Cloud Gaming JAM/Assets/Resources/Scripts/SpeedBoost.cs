using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    private int speedBoost;
    
    // Start is called before the first frame update
    void Start()
    {
        speedBoost = LevelManager.instance.speedBoost;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        other.GetComponent<RaftController>().UpdateRaftForce(Vector2.right * speedBoost);
        Debug.Log("SPEEDBOOST : " + other.name);
    }
}
