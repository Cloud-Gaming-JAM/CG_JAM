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

    private float friction;
    private float rawFlowForce;
    #endregion
    
    #region PrivateMethods
    
    void Start()
    {
        raftRigidBody = gameObject.GetComponent<Rigidbody2D>();
        InitValues();
    }

    void InitValues()
    {
        raftSpeedMultiplier = LevelManager.instance.raftSpeedCoef;
        friction = LevelManager.instance.friction;
        rawFlowForce = LevelManager.instance.rawFlowForce;
    }
    
    //Checks if all variables a set up properly
    void PreStartCheck()
    {
        if(playersOnRaft.Count == 0)
        {
            Debug.LogError("Raft has no players on board");
        }
    }

    public void UpdateRaftForce(Vector2 forceToAdd)
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
    
    void ClampRaftSpeed(Vector2 maxSpeed)
    {
        Vector2 newVelocity = new Vector2(raftRigidBody.velocity.x + finalRaftForce.x, raftRigidBody.velocity.y + finalRaftForce.y);

        if (newVelocity.x > maxSpeed.x)
            finalRaftForce.x = maxSpeed.x - raftRigidBody.velocity.x;
        else if (newVelocity.x < -maxSpeed.x)
            finalRaftForce.x = -maxSpeed.x + raftRigidBody.velocity.x;
        
        if (newVelocity.y > maxSpeed.y)
            finalRaftForce.y = maxSpeed.y - raftRigidBody.velocity.y;
        else if (newVelocity.y < -maxSpeed.y)
            finalRaftForce.y = -maxSpeed.y + raftRigidBody.velocity.y;
    }
    
    // Update is called once per frame
    void Update()
    {
        CheckAndApplyPlayersForce();
    }

    void CheckAndApplyPlayersForce()
    {
        Vector2 playersInput = GetRaftPlayersInput();   
        if (playersInput == Vector2.zero) return;
        
        if(playersInput.x != 0)
            playersInput.x *= LevelManager.instance.raftHorizontalSpeedCoef;
        
        Debug.Log("UpdateRaftForce");
        UpdateRaftForce(playersInput);
        finalRaftForce *= raftSpeedMultiplier;
        
        //ClampRaftSpeed(LevelManager.instance.maxNormalSpeed);
    }
    
    private void ApplyFrictionAndFlowForce()
    {
        raftRigidBody.velocity *= friction;
        if(raftRigidBody.velocity.x > -(LevelManager.instance.maxNormalSpeed.x / 2))
            raftRigidBody.AddForce(Vector2.left / rawFlowForce);

        //if (!Mathf.Approximately(0f, raftRigidBody.velocity.x))
    }
    
    private void ApplyNewSpeed()
    {
        //Debug.Log("Before : " + finalRaftForce);
        if (finalRaftForce != Vector2.zero)
        {
            //ClampRaftSpeed(LevelManager.instance.maxBoostSpeed);
            raftRigidBody.AddForce(finalRaftForce);
            finalRaftForce = Vector2.zero;
        }
    }

    void FixedUpdate()
    {
        ApplyNewSpeed();
        ApplyFrictionAndFlowForce();
    }
    
    #endregion
    #region PublicMethods

    public int GetNbrPlayersOnRaft()
    {
        return playersOnRaft.Count;
    }
    
    public void SwitchPlayerStates()
    {
        
    }
    
    #endregion
}


