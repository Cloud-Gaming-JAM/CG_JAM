using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrunkCollisionProbe : MonoBehaviour
{
    public TrunkToBreakController instance;

    void OnCollisionEnter2D(Collision2D other)
    {
        instance.CheckDamage(other.rigidbody);
    }
}
