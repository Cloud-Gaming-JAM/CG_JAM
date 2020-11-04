using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [Range(0.2f, 5f)] public float obstacleSpeed = 1f;
    public float obstacleDistance = 1f;
    Vector3 originalPosition;
    Rigidbody2D obstacleRigidBody;
    // Start is called before the first frame update
    void Start()
    {
        obstacleRigidBody = gameObject.GetComponent<Rigidbody2D>();
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateObstaclePosition();
    }

    void UpdateObstaclePosition()
    {
        transform.position = originalPosition + new Vector3(0f, Mathf.Sin(Time.time * obstacleSpeed) * obstacleDistance, 0f);
    }
}
