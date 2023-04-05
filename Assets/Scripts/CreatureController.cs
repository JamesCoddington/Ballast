using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class CreatureController : MonoBehaviour
{
    public GameObject submarine;

    public float encounterDist = 30f;
    public float attackDist = 5f;

    public float movementSpeed = 1f;
    public float rotateSpeed = 1f;

    [Header("Dev Info")]
    public float dist;
    public GameObject focalPoint;
    public float rotationDirection;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        dist = checkDist();
        if (dist < encounterDist)
        {
            if (dist < attackDist)
            {
                attackSub();
            }
        moveCreature();
        rotateCreature();
        }
    }

    public float checkDist()
    {
        float dist = Vector3.Distance(submarine.transform.position, transform.position);
        return dist;
    }

    public void moveCreature()
    {
        float move = movementSpeed * Time.deltaTime;
        if (dist < attackDist && transform.rotation.y != .7)
        {
            rotationDirection = 1;
            if (transform.rotation.y > .7)
            {
                rotationDirection = -1;
            }
            print(transform.rotation.y);
            transform.RotateAround(submarine.transform.position, submarine.transform.up, rotationDirection * move * 10);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, submarine.transform.position, move);

        }
    }
    public void attackSub()
    {
        return;
    }

    public void rotateCreature()
    {
        Vector3 targetDir = submarine.transform.position - transform.position + 2*(Vector3.down);
        float step = rotateSpeed * Time.deltaTime;
        Vector3 newRotation = Vector3.RotateTowards(transform.forward, -targetDir, step, 0.0f);
        transform.rotation = Quaternion.LookRotation(newRotation);
    }
}
