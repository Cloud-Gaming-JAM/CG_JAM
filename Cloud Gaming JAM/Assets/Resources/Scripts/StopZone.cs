using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopZone : MonoBehaviour
{
    [SerializeField] BoxCollider2D wall;
    
    [SerializeField] Animation anim;
    
    [SerializeField] float time;
    [SerializeField] float activationTime = 1;

    [SerializeField] bool isInZone;
    bool isActive = true;
    
    // Start is called before the first frame update
    void Start()
    {
        activationTime = LevelManager.instance.stopZoneTimer;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Debug.Log("in park zone !");
        isInZone = true;
        time = 0;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        Debug.Log("Exit park zone !");
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
        Debug.Log("active wall");
        wall.enabled = false;
        anim.Play("Beavers-GoThrough");
        isActive = false;
        //Destroy(this, anim.clip.length);
    }
}
