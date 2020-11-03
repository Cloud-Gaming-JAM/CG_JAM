using System.Collections.Generic;
using UnityEngine;

public class RaftController : MonoBehaviour
{
    #region PublicVariables
    //Raft's maximum speed, values below 1 will slow down the raft
    [Range(0.2f, 5f)] public float raftSpeedMultiplier = 1f;
    #endregion
    #region PrivateVariables
    //List<PlayerController> playersOnRaft = new List<PlayerController>(2);
    Rigidbody2D raftRigidBody;
    #endregion
    #region PrivateMethods
    // Start is called before the first frame update
    void Start()
    {
        raftRigidBody = GetComponent<Rigidbody2D>();

        PreStartCheck();
    }
    // Update is called once per frame
    void Update()
    {
        UpdateRaftSpeed();
    }
    void FixedUpdate()
    {
        Debug.Log(Input.GetAxisRaw("Horizontal"));
        Debug.Log(Input.GetAxisRaw("Vertical"));
    }
    //Checks if all variables a set up properly
    void PreStartCheck()
    {
        if(raftRigidBody == null)
        {
            Debug.LogError("Raft dosen't contain a RigidBody component");
        }
    }
    #endregion
    #region PublicMethods
    ///<summary>
    ///Updates the raft instance's speed, meant to be used in a Update() loop, takes an Input Vector.
    ///<param name="calculatedInput">Mixed input vector.</param>
    ///</summary>
    public void UpdateRaftSpeed()
    {
        raftRigidBody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * raftSpeedMultiplier, Input.GetAxisRaw("Vertical") *  raftSpeedMultiplier);
    }
    #endregion
}
