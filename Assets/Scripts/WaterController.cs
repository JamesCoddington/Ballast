using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour
{

    public float cracks;
    float t;
    float duration;
    Vector3 startPosition;
    Vector3 target;
    

    void Start()
    {
        startPosition = transform.position;
        target = (transform.position + new Vector3(0f, 2.5f, 0f));
        duration = 60f;
        t = 0f;
    }

    void Update()
    {
        if (cracks > 0)
        {
            float cracksTime = duration / cracks;
            t += Time.deltaTime / cracksTime;
            transform.position = Vector3.Lerp(startPosition, target, t);
        } 
        else
        {
            t = 0f;
            t += Time.deltaTime / duration;
            transform.position = Vector3.Lerp(transform.position, startPosition, t);
        }
        
    }

    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = transform.position;
        while (time < duration && cracks )
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }

}
