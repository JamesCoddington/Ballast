using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class CreatureController : MonoBehaviour
{
    public GameObject submarine;

    public float encounterDist = 30f;
    public float attackDist = 5f;

    public float moveSpeed = 1f;
    public float rotateSpeed = 1f;

    [Header("Dev Info")]
    public float dist;
    public float rotationDirection;
    public bool attackFlag = false;

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
            if (dist < attackDist && attackFlag)
            {
                attackSub();
                attackFlag = false;
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
        float move = moveSpeed * Time.deltaTime;
        if (dist < attackDist)
        {
            float rotate = rotateSpeed * Time.deltaTime;
            if (transform.rotation.y < .75)
            {
                rotationDirection = 1;
            } else if (transform.rotation.y > .65)
            {
                rotationDirection = -1;
            } else
            {
                rotateSpeed = 0;
                attackFlag = true;
            }
            transform.RotateAround(submarine.transform.position, submarine.transform.up, rotationDirection * rotate * 10);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, submarine.transform.position, move);
        }
    }


    public void attackSub()
    {
        print("attack!");
        // make sure to reset rotateSpeed after attack
        return;
    }

    public void rotateCreature()
    {
        Vector3 targetDir = submarine.transform.position - transform.position;
        float step = rotateSpeed * Time.deltaTime;
        Vector3 newRotation = Vector3.RotateTowards(transform.forward, -targetDir, step, 0.0f);
        transform.rotation = Quaternion.LookRotation(newRotation);
    }
}
