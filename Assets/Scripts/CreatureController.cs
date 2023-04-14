
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class CreatureController : MonoBehaviour
{
    public SubmarineController submarine;

    public float encounterDist = 30f;
    // public float attackDist = 5f;

    public float moveSpeed = 3f;
    public float rotateSpeed = 3f;
    private Transform[] subPositions;

    public float approachCooldown = 10f;
    public float attackCooldown = 10f;

    public Transform[] pathPositions;

    [Header("Dev Info")]
    public float dist;
    public float rotationDirection;
    public bool approachFlag = false;
    public bool attackFlag = true;
    public bool resetFlag = false;
    public bool nextToSub = false;
    public float timer = 0f;
    public int pathPos = 0;
    public int nextPos = 1;
    public Transform targetPosition;
    public Transform prevPathPosition;

    // Start is called before the first frame update
    void Start()
    {
        Transform leftPosition = submarine.transform.Find("Left Position");
        Transform rightPosition = submarine.transform.Find("Right Position");
        Transform frontPosition = submarine.transform.Find("Front Position");
        subPositions = new Transform[] {leftPosition, rightPosition, frontPosition};
        // TODO: set to first position in path
        targetPosition = pathPositions[0];
    }

    // Update is called once per frame
    void Update()
    {
        dist = checkDist();
        if (dist <= encounterDist && !resetFlag)
        {
            approachSub();
        } 

        if (dist > encounterDist)
        {
            targetPathPosition();
        }
        
        if (resetFlag)
        {
            timer += Time.deltaTime;
            if (timer >= approachCooldown)
            {
                timer = 0f;
                resetFlag = false;
            }
        }
        moveCreature();
        rotateCreature();
    }

    // sets next path position, based on if creature has reached previous path position and what position it is at
    void targetPathPosition()
    {
        if (transform.position == targetPosition.position)
        {
            print("reached path position " + pathPos);
            if (pathPos == pathPositions.Length - 1)
            {
                nextPos = -1;
            } else if (pathPos == 0) {
                nextPos = 1;
            }
            pathPos += nextPos;

            targetPosition = pathPositions[pathPos];
        }
    }

    // Determine the target position the creature will move to around the submarine
    void targetSubPosition()
    {
        if (!nextToSub)
        {
            foreach (Transform position in subPositions)
            {
                float targetPositionDist = Vector3.Distance(transform.position, subPositions[0].transform.position);
                float currentDist = Vector3.Distance(transform.position, position.transform.position);
                if (currentDist < targetPositionDist)
                {
                    targetPosition = position;
                }
            }
            if (transform.position == subPositions[0].transform.position || transform.position == subPositions[1].transform.position)
            {
                targetPosition = subPositions[2];
                nextToSub = true;
            }
        }
    }


    void approachSub()
    {
        if (Array.Exists(pathPositions, position => position == targetPosition))
        {
            prevPathPosition = targetPosition;
        }
        // toggle to change depending on submarine emergency status
        approachFlag = true;
        if (approachFlag)
        {
            targetSubPosition();
            if (transform.position == subPositions[2].position)
            {
                if (attackFlag)
                {
                    attackSub();
                    attackFlag = false;
                }
                else
                {
                    timer += Time.deltaTime;
                    if (timer >= attackCooldown)
                    {
                        timer = 0f;
                        attackFlag = true;
                    }
                }
            }
        }
        else
        {
            // add functionality to go back to what position it was targeting
            targetPosition = prevPathPosition;
        }
    }

    public bool checkSubStatus()
    {
        return !submarine.powerShutOff;
    }

    // Check distance to submarine
    public float checkDist()
    {
        float dist = Vector3.Distance(transform.position, submarine.transform.position);
        return dist;
    }
    public void moveCreature()
    {
        float move = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition.transform.position, move);
    }

    // Creature attacks sub (activate leak, causes noise, etc.)
    public void attackSub()
    {
        print("Attack!");
        return;
    }

    public void rotateCreature()
    {
        if (Array.Exists(pathPositions, position => position == targetPosition))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetPosition.rotation, Time.deltaTime * rotateSpeed/10);
        }
        else if (Array.Exists(subPositions, position => position == targetPosition))
        {
            // adjust for offset by lowering focus point
            Vector3 targetDir = targetPosition.transform.position - transform.position - 3 * Vector3.up;
            float step = rotateSpeed * Time.deltaTime;
            Vector3 newRotation = Vector3.RotateTowards(transform.forward, -targetDir, step, 0.0f);
            transform.rotation = Quaternion.LookRotation(newRotation);
        }
    }
}
