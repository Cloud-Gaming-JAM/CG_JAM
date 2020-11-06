using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrunkCollisionProbe : MonoBehaviour
{
    public TrunkToBreakController instance;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.relativeVelocity.magnitude >= 1)
        {
            instance.CheckDamage(other.rigidbody);
        }

    }
}
