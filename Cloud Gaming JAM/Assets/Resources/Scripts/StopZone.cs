using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopZone : MonoBehaviour
{
    public BoxCollider2D wall;
    
    private Animation anim;
    
    private float time;
    private float activationTime;

    private bool isInZone;
    private bool isActive;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animation>();
        activationTime = LevelManager.instance.stopZoneTimer;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        isInZone = true;
        time = 0;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        isInZone = false;
    }

    private void Update()
    {
        if(isInZone && isActive)
            UpdateTime();
    }

    private void UpdateTime()
    {
        time += Time.deltaTime;
        if (time >= activationTime)
            ActiveWall();
    }

    private void ActiveWall()
    {
        anim.Play("openBarrage");
        wall.enabled = false;
        isActive = false;
        //Destroy(this, anim.clip.length);
    }
}
