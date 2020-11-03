using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [Range(0.2f, 5f)] public float obstacleSpeed = 1f;
    Rigidbody2D obstacleRigidBody;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateRaftSpeed();
    }

    void UpdateRaftSpeed()
    {
        Vector2 raftMixedInput = new Vector2(0f, 0f);
        obstacleRigidBody.velocity = new Vector2(-obstacleSpeed, 0f);
    }
}
