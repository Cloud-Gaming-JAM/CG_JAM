using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [Range(0.2f, 5f)] public float obstacleSpeed = 1f;
    public float obstacleDistance = 1f;
    Vector3 originalPosition;

    private bool isMoveUp;
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateObstaclePosition();
    }

    void UpdateObstaclePosition()
    {
        transform.position = originalPosition + new Vector3(0f, Mathf.Cos(Time.time * obstacleSpeed) * obstacleDistance, 0f);
        if (Mathf.Cos(Time.time * obstacleSpeed) > 0.99f && isMoveUp)
        {
            transform.rotation = new Quaternion(0,0,0,0);
            isMoveUp = false;
        }
        else if (Mathf.Cos(Time.time * obstacleSpeed) < -0.99f && !isMoveUp)
        {
            transform.rotation = new Quaternion(0,0,180,0);
            isMoveUp = true;
        }
    }
}
