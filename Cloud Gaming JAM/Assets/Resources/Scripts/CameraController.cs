using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Range(1f, 10f)] public float displacementLength = 1f;
    [Range(2f, 10f)] public float displacementDuration = 1f;
    public AnimationCurve travelLUT;
    Collider2D travelTrigger;
    bool travelOnce = false;

    Vector3 startPosition;
    Vector3 endPosition;
    float starttime;
    float endtime;
    void Start()
    {
        travelTrigger = GetComponent<Collider2D>();

    }

    void FixedUpdate()
    {
        startPosition = transform.position;
        endPosition = startPosition + new Vector3(displacementLength, 0f, 0f);
        starttime = Time.time;
        endtime = Time.time + displacementDuration;
    }
    void Travel()
    {

        if (!travelOnce)
        {
            travelOnce = true;
            travelTrigger.enabled = false;
            StartCoroutine(TravelRoutine(startPosition, endPosition, starttime, endtime));
        }

    }

    void OnTriggerEnter2D(Collider2D raft)
    {
        Travel();
    }

    IEnumerator TravelRoutine(Vector3 startPos, Vector3 endPos, float starttime, float endtime)
    {
        float timeTmp = 0f;
        while (starttime + timeTmp <= endtime)
        {
            timeTmp += Time.deltaTime;
            Debug.Log("Camera Interp Iteration");
            transform.position = Vector3.Lerp(startPos, endPos, travelLUT.Evaluate((starttime + timeTmp) / endtime));
            yield return null;
        }
        travelTrigger.enabled = true;
        travelOnce = false;

    }
}
