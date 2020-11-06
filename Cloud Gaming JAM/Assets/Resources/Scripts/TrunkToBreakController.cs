using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrunkToBreakController : MonoBehaviour
{
    [Range(1, 3)] public int trunkHealth;
    public GameObject normalTrunk;
    public GameObject brokenTrunk;


    public void CheckDamage(Rigidbody2D other)
    {
        if (other.CompareTag("Player")) 
        {
            trunkHealth--;
        }
        if (trunkHealth == 0)
        {
            AudioManager.instance.Play("TrunkBreak");
            normalTrunk.SetActive(false);
            //trunk animation
            brokenTrunk.SetActive(true);
        }
    }

}
