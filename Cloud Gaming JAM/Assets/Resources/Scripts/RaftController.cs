using System.Collections.Generic;
using UnityEngine;

public class RaftController : MonoBehaviour
{
    #region PublicVariables
    //Raft's maximum speed, values below 1 will slow down the raft
    [Range(0.2f, 5f)] public float raftSpeedMultiplier = 1f;
    #endregion
    #region PrivateVariables
    public List<PlayerController> playersOnRaft = new List<PlayerController>();
    
    Rigidbody2D raftRigidBody;
    #endregion
    #region PrivateMethods

    //Checks if all variables a set up properly
    void PreStartCheck()
    {
        if(raftRigidBody == null)
        {
            Debug.LogError("Raft dosen't contain a RigidBody component");
        }
        if(playersOnRaft.Count == 0)
        {
            Debug.LogError("Raft has no players on board");
        }
    }

    ///<summary>
    ///Updates the raft instance's speed, meant to be used in a Update() loop, takes an Input Vector.
    ///<param name="calculatedInput">Mixed input vector.</param>
    ///</summary>
    void UpdateRaftSpeed()
    {
        Vector2 raftMixedInput = new Vector2(0f,0f);
        foreach(PlayerController instance in playersOnRaft)
        {
            raftMixedInput += instance.GetPlayerInput();
            Debug.Log(instance.GetPlayerInput());
        }
        raftMixedInput.Normalize();
        raftRigidBody.velocity = new Vector2(raftMixedInput.x*raftSpeedMultiplier, raftMixedInput.y*raftSpeedMultiplier);
    }

    // Start is called before the first frame update
    void Start()
    {
        raftRigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRaftSpeed();
    }

    void FixedUpdate()
    {
        /* Shows axis values in console
        Debug.Log(Input.GetAxisRaw("Horizontal"));
        Debug.Log(Input.GetAxisRaw("Vertical"));
        */
    }
    #endregion
    #region PublicMethods

    public int GetNbrPlayersOnRaft()
    {
        return playersOnRaft.Count;
    }
    #endregion
}
