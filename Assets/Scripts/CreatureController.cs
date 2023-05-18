
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class CreatureController : MonoBehaviour
{
    public GameObject submarine;
    private SubmarineController submarineController;
    private LeakController leakController;

    public float encounterDist = 30f;
    // public float attackDist = 5f;

    public float moveSpeed = 3f;
    public float rotateSpeed = 3f;
    private Transform[] subPositions;

    public float approachCooldown = 10f;
    public float attackCooldown = 5f;

    public Transform[] pathPositions;

    [Header("Dev Info")]
    public bool inRange = false;
    public float rotationDirection;
    public bool subPowerOff = false;
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
        submarineController = submarine.GetComponent<SubmarineController>();
        leakController = submarine.GetComponent<LeakController>();

        // These could probably be refactored into enums, or handled with a switch case statement
        Transform leftPosition = submarineController.transform.Find("Left Position");
        Transform rightPosition = submarineController.transform.Find("Right Position");
        Transform frontPosition = submarineController.transform.Find("Front Position");
        subPositions = new Transform[] {leftPosition, rightPosition, frontPosition};
        // TODO: set to first position in path
        targetPosition = pathPositions[0];

        SphereCollider encounterRange = GetComponent<SphereCollider>();
        encounterRange.radius = encounterDist / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange && !resetFlag)
        {
            approachSub();
        }

        if (!inRange || subPowerOff || resetFlag)
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

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "HeadCollider")
        {
            inRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "HeadCollider")
        {
            inRange = false;
        }
    }

    // sets next path position, based on if creature has reached previous path position and what position it is at
    void targetPathPosition()
    {
        if (prevPathPosition)
        {
            // go back to what position it was targeting
            targetPosition = prevPathPosition;
        }
        if (transform.position == targetPosition.position)
        {
            // Check to see if we've reset to the previous path position
            // If so, set the resetFlag to true & reset timer
            if (prevPathPosition != null && targetPosition.position == prevPathPosition.position)
            {
                prevPathPosition = null;
                resetFlag = true;
                attackFlag = true;
                timer = 0f;
            }
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
        // toggle to change depending on submarine emergency status
        subPowerOff = checkSubStatus();
        if (!subPowerOff)
        {
            if (Array.Exists(pathPositions, position => position == targetPosition))
            {
                prevPathPosition = targetPosition;
            }
            targetSubPosition();
            if (transform.position == subPositions[2].position)
            {
                attackSub();
            }
        }
    }

    public bool checkSubStatus()
    {
        return submarineController.powerShutOff;
    }
    public void moveCreature()
    {
        float move = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition.transform.position, move);
    }

    // Creature attacks sub (activate leak, causes noise, etc.) then goes on cooldown
    public void attackSub()
    {

        if (attackFlag)
        {
            leakController.randoInt = 5;
            leakController.takeDamage();

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

    public void rotateCreature()
    {
        Vector3 targetDir;
        if (Array.Exists(pathPositions, position => position == targetPosition))
        {
            targetDir = targetPosition.transform.position - transform.position;
        }
        else 
        {
            // adjust for offset by lowering focus point
            targetDir = submarine.transform.position - transform.position - 3 * Vector3.up;
        }
        float step = rotateSpeed * Time.deltaTime;
        Vector3 newRotation = Vector3.RotateTowards(transform.forward, -targetDir, step, 0.0f);
        transform.rotation = Quaternion.LookRotation(newRotation);
    }
}
