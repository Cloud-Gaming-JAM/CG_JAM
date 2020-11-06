using System;
using System.Collections.Generic;
using UnityEngine;

public class RaftController : MonoBehaviour
{
    public Rigidbody2D raftRigidBody;
    public float raftSpeedMultiplier = 1f;

    public List<PlayerController> playersOnRaft = new List<PlayerController>();
    public int teamId;
    public float oarSoundIntermitence = 2f;

    [SerializeField] private Transform[] characterTransform = new Transform[2];
    public GameObject[] characterPrefab = new GameObject[2];

    private Vector2 finalRaftForce;
    private float timeSinceOarSound;
    private bool oarSound = true;
    private float friction;
    private float rawFlowForce;

    #region OnStartCalls

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

    void PreStartCheck()
    {
        if (playersOnRaft.Count == 0)
        {
            Debug.LogError("Raft has no players on board");
        }
    }
    #endregion

    #region PublicMethods

    public int GetNbrPlayersOnRaft()
    {
        return playersOnRaft.Count;
    }

    public void UpdateRaftForce(Vector2 forceToAdd)
    {
        finalRaftForce += forceToAdd;
    }
    #endregion

    #region UpdateCalls
    void Update()
    {
        if (GameManager.instance.gameState == GameState.inGame)
            CheckAndApplyPlayersForce();
        UpdateSpeedAnimParam();
    }

    void UpdateSpeedAnimParam()
    {
        
        foreach (PlayerController player in playersOnRaft)
        {
            int i = 0;
            if (player.playerId == 2 || player.playerId == 4)
                i = 1;
            
            if (player.state == PlayerMoveState.horizontal)
                characterPrefab[i].GetComponent<Animator>().SetFloat("Speed", (raftRigidBody.velocity.x / LevelManager.instance.maxBoostSpeed.x) * 10);
            else if (player.state == PlayerMoveState.vertical)
                characterPrefab[i].GetComponent<Animator>().SetFloat("Speed", (raftRigidBody.velocity.y / LevelManager.instance.maxBoostSpeed.y) * 10);
        }
    }

    void CheckAndApplyPlayersForce()
    {
        Vector2 playersInput = GetRaftPlayersInput();
        if (playersInput == Vector2.zero) return;

        if (playersInput.x != 0)
            playersInput.x *= LevelManager.instance.raftHorizontalSpeedCoef;

        Debug.Log("UpdateRaftForce");
        UpdateRaftForce(playersInput);
        finalRaftForce *= raftSpeedMultiplier;

        if (timeSinceOarSound >= oarSoundIntermitence)
        {
            oarSound = true;
        }
        if (oarSound)
        {
            AudioManager.instance.Play("oar" + (int)UnityEngine.Random.Range(1, 3));
            oarSound = false;
            oarSoundIntermitence = 0f;
        }
        else
        {
            timeSinceOarSound += Time.deltaTime;
        }


        ClampRaftSpeed(LevelManager.instance.maxNormalSpeed);
    }

    Vector2 GetRaftPlayersInput()
    {
        Vector2 raftMixedInput = new Vector2(0f, 0f);
        foreach (PlayerController instance in playersOnRaft)
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
    #endregion

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Bumper"))
        {
            BumpWithObstacle(other);
        }

    }

    #region Bump

    public void BumpWithObstacle(Collision2D other)
    {
        Vector2 dir = other.relativeVelocity.normalized;
        raftRigidBody.AddForce(dir * LevelManager.instance.BumpForce);
        AudioManager.instance.Play("grunt" + (int)UnityEngine.Random.Range(1, 5));
    }

    #endregion

    #region FixedUpdateCalls

    void FixedUpdate()
    {
        ApplyNewSpeed();
        ApplyFrictionAndFlowForce();
    }

    private void ApplyNewSpeed()
    {
        //Debug.Log("Before : " + finalRaftForce);
        if (finalRaftForce != Vector2.zero)
        {
            ClampRaftSpeed(LevelManager.instance.maxBoostSpeed);
            raftRigidBody.AddForce(finalRaftForce);
            finalRaftForce = Vector2.zero;
        }
    }

    private void ApplyFrictionAndFlowForce()
    {
        raftRigidBody.velocity *= friction;
        if (raftRigidBody.velocity.x > -(LevelManager.instance.maxNormalSpeed.x / 2))
            raftRigidBody.AddForce(Vector2.left / rawFlowForce);
    }
    #endregion

    #region SwitchPlayers

    public void SwitchPlayers()
    {
        Debug.Log("Enter in the fog !");
        foreach (var playerController in playersOnRaft)
        {
            if (playerController.state == PlayerMoveState.horizontal)
                playerController.state = PlayerMoveState.vertical;
            else if (playerController.state == PlayerMoveState.vertical)
                playerController.state = PlayerMoveState.horizontal;
        }
        characterPrefab[0].transform.SetParent(characterTransform[0].transform, false);
        characterPrefab[1].transform.SetParent(characterTransform[1].transform, false);
    }

    #endregion
}


