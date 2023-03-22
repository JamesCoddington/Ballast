using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Water
{
    Rising,
    Falling
}

public class WaterController : MonoBehaviour
{

    public float cracks;
    Water water = Water.Falling;
    float t;
    public float duration;
    Vector3 startPosition;
    Vector3 target;
    

    void Start()
    {
        startPosition = transform.position;
        target = (transform.position + new Vector3(0f, 2.5f, 0f));
        //duration = 60f;
        t = 0f;
        //StartCoroutine(LerpWater());
    }

    void Update()
    {
        if (cracks > 0)
        {
            if (water == Water.Falling)
            {
                water = Water.Rising;
                t = 0f;
            }
            float cracksTime = duration / cracks;
            t += Time.deltaTime / cracksTime;
            transform.position = Vector3.Lerp(transform.position, target, t);
        }
        else
        {
            if (water == Water.Rising)
            {
                water = Water.Falling;
                t = 0f;
            }
            t += Time.deltaTime / duration;
            transform.position = Vector3.Lerp(transform.position, startPosition, t);
        }
        //if (cracks > 0)
        //{
        //    RaiseWater();
        //} else if (cracks < 1)
        //{
        //    LowerWater();
        //}
    }

    void RaiseWater()
    {
        t = 0f;
        float cracksTime = duration / cracks;
        while (cracks > 0)
        {
            t += Time.deltaTime / cracksTime;
            transform.position = Vector3.Lerp(startPosition, target, t);
        }
    }

    void LowerWater()
    {
        t = 0f;
        while (cracks < 1)
        {
            t += Time.deltaTime / duration;
            transform.position = Vector3.Lerp(transform.position, startPosition, t);
        }
    }

    //IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    //{
    //    float time = 0;
    //    Vector3 startPosition = transform.position;
    //    while (time < duration && cracks > 0)
    //    {
    //        transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
    //        time += Time.deltaTime;
    //        yield return null;
    //    }
    //    transform.position = targetPosition;
    //}

    //IEnumerator LerpWater()
    //{
    //    while (true)
    //    {
    //        t = 0f;
    //        while (cracks < 1)
    //        {
    //            t += Time.deltaTime / duration;
    //            transform.position = Vector3.Lerp(transform.position, startPosition, t);
    //        }
    //        t = 0f;
    //        while (cracks > 0 && transform.position == target)
    //        {
    //            float cracksTime = duration / cracks;
    //            t += Time.deltaTime / cracksTime;
    //            transform.position = Vector3.Lerp(startPosition, target, t);
    //        }
    //        if (transform.position == target)
    //        {
    //            print("Game Over");
    //        }
    //    }
    //}

}
