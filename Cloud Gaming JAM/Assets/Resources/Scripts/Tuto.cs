using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuto : MonoBehaviour
{
    public float[] raftsPos = new float[LevelManager.instance.nbrRaftInGame];

    private bool over;
    void Update()
    {
        if(!over)
            UpdateRaftPos();
    }

    void UpdateRaftPos()
    {
        int raftCrossed = 0;
        for (int i = 0; i < raftsPos.Length; i++)
        {
            raftsPos[i] = LevelManager.instance.rafts[i].transform.position.x;
            if (raftsPos[i] > transform.position.x)
                raftCrossed++;
        }
        if(raftCrossed == raftsPos.Length)
            DisableTuto();
    }

    void DisableTuto()
    {
        over = true;
        Debug.Log("Disable tuto");
        //this.gameObject.SetActive(false);
    }
}
