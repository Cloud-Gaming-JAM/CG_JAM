using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public AnimationCurve travelLUT;
    Collider2D travelTrigger;
    bool travelOnce = false;

    Vector3 startPosition;
    Vector3 endPosition;
    void Start()
    {
        travelTrigger = GetComponent<Collider2D>();

    }

    void FixedUpdate()
    {

    }

    void Update()
    {
        startPosition = transform.position;
    }
    void Travel(Collider2D raft)
    {
        endPosition = transform.position + new Vector3(raft.attachedRigidbody.velocity.x, 0f, 0f);
        if (!travelOnce)
        {
            Debug.Log("Camera Interp Travel");
            travelOnce = true;
            StartCoroutine(TravelRoutine(startPosition, endPosition, 1f));
        }

    }

    void OnTriggerStay2D(Collider2D raft)
    {
        Travel(raft);
    }

    IEnumerator TravelRoutine(Vector3 startPos, Vector3 endPos, float endtime)
    {
        float timeTmp = 0f;
        while (timeTmp <= endtime)
        {
            timeTmp += Time.deltaTime;
            Debug.Log("Camera Interp Iteration");
            transform.position = Vector3.Lerp(startPos, endPos, travelLUT.Evaluate(timeTmp / endtime));
            yield return null;
        }
        travelTrigger.enabled = true;
        travelOnce = false;
    }
}
