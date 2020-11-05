using System;
using System.Collections.Generic;
using UnityEngine;

public class RaftController : MonoBehaviour
{
    #region PublicVariables
    public Rigidbody2D raftRigidBody;
    public float raftSpeedMultiplier = 1f;
    
    public List<PlayerController> playersOnRaft = new List<PlayerController>();
    [HideInInspector] public int teamId;
    #endregion
    
    #region PrivateVariables
    private Vector2 finalRaftForce;

    private float flowForceCoef;

    private Vector2 lastInput;
    private Vector2 currentInput;
    private float deltaInputs;
    #endregion
    
    #region PrivateMethods
    
    void Start()
    {
        raftRigidBody = gameObject.GetComponent<Rigidbody2D>();
        InitValues();
    }

    void InitValues()
    {
        raftSpeedMultiplier = LevelManager.instance.raftSpeedMultiplier;
    }
    
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
        }
        raftMixedInput *= raftSpeedMultiplier;
        
        if (raftMixedInput != Vector2.zero)
            raftRigidBody.velocity = raftMixedInput;
    }
    
    void UpdateRaftForce(Vector2 forceToAdd)
    {
        finalRaftForce += forceToAdd;
    }
    
    Vector2 GetRaftPlayersInput()
    {
        Vector2 raftMixedInput = new Vector2(0f,0f);
        foreach(PlayerController instance in playersOnRaft)
        {
            raftMixedInput += instance.GetPlayerInput();
        }
        return raftMixedInput;
    }
    
    void ClampRaftSpeed(float maxSpeed)
    {
        if (finalRaftForce.x > maxSpeed)
            finalRaftForce.x = maxSpeed;
        else if (finalRaftForce.x < -maxSpeed)
            finalRaftForce.x = -maxSpeed;
        
        if (finalRaftForce.y > maxSpeed)
            finalRaftForce.y = maxSpeed;
        else if (finalRaftForce.y < -maxSpeed)
            finalRaftForce.y = -maxSpeed;
    }
    
    // Update is called once per frame
    void Update()
    {
        UpdateRaftForce(GetRaftPlayersInput());
        ClampRaftSpeed(LevelManager.instance.maxNormalSpeed);
    }

    private void ApplyFlowForce()
    {
        raftRigidBody.velocity *= flowForceCoef;
        //if (raftRigidBody.velocity.x < 0.005f) raftRigidBody.velocity.x = 0;
    }
    
    private void ApplyNewSpeed()
    {
        ClampRaftSpeed(LevelManager.instance.maxBoostSpeed);
        raftRigidBody.AddForce(finalRaftForce);
    }

    void FixedUpdate()
    {
        ApplyNewSpeed();
        ApplyFlowForce();
    }
    
    #endregion
    #region PublicMethods

    public int GetNbrPlayersOnRaft()
    {
        return playersOnRaft.Count;
    }
    
    #endregion
}


