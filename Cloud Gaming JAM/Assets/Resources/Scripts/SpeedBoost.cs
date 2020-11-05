using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    private float speedBoost;
    
    // Start is called before the first frame update
    void Start()
    {
        speedBoost = LevelManager.instance.speedBoost;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        other.attachedRigidbody.AddForce(new Vector3(speedBoost, 0));
        Debug.Log("SPEEDBOOST");
    }
}
