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

    
    public GameObject leakParent;
    float cracks;
    Water water = Water.Falling;
    float t;
    // 15 Is good
    public float duration;
    Vector3 startPosition;
    Vector3 target;
    

    void Start()
    {
        startPosition = transform.position;
        target = (transform.position + new Vector3(0f, 2.5f, 0f));
        t = 0f;
    }

    void Update()
    {
        // print(leakParent.GetComponentsInChildren<Transform>().GetLength(0) - 1);
        // GetComponentsInChildren<Transform>().GetLength(0)
        // Gives off 1 when no leaks are there so we subtract by one
        cracks = leakParent.GetComponentsInChildren<Transform>().GetLength(0) - 1;
    }

    void LateUpdate()
    {
        if (cracks > 0)
        {
            if (transform.position == target)
            {
                // Uses ST_GameOver
                ScenesManager.Instance.LoadScene(ScenesManager.Scene.ST_GameOver);
            }
            if (water == Water.Falling)
            {
                water = Water.Rising;
                t = 0f;
            }
            //float cracksTime = duration / cracks;
            //t += Time.deltaTime / cracksTime;
            //transform.position = Vector3.Lerp(transform.position, target, t);
            transform.position = Vector3.MoveTowards(transform.position, target, cracks * (Time.deltaTime / duration));
        }
        else
        {
            if (water == Water.Rising)
            {
                water = Water.Falling;
                t = 0f;
            }
            //t += Time.deltaTime / duration;
            //transform.position = Vector3.Lerp(transform.position, startPosition, t);
            transform.position = Vector3.MoveTowards(transform.position, startPosition, (Time.deltaTime / duration));
        }
    }
}
