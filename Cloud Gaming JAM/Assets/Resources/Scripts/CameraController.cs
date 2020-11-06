using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Range(1f, 10f)] public float displacementLength = 1f;
    [Range(1.5f, 10f)] public float displacementDuration = 1f;
    public AnimationCurve travelLUT;
    Collider2D travelTrigger;
    bool travelOnce = false;

    void Start()
    {
        travelTrigger = GetComponent<Collider2D>();

    }

    void Travel()
    {
        if (!travelOnce)
        {
            travelOnce = true;
            travelTrigger.enabled = false;
            float starttime = Time.time;
            float endtime = Time.time + displacementDuration;
            Vector3 startPosition = transform.position;
            Vector3 endPosition = startPosition + new Vector3(displacementLength, 0f, 0f);
            StartCoroutine(TravelRoutine(startPosition, endPosition, endtime));
        }

    }

    void OnTriggerEnter2D(Collider2D raft)
    {
        Travel();
    }

    IEnumerator TravelRoutine(Vector3 startPos, Vector3 endPos, float endtime)
    {
        while (Time.time != endtime)
        {
            transform.position = Vector3.Lerp(startPos, endPos, travelLUT.Evaluate(Time.time / endtime));
            travelTrigger.enabled = true;
            travelOnce = false;
            yield return new WaitForEndOfFrame();
        }

        yield return null;

    }
}
