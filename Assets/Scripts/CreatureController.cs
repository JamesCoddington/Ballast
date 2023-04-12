using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class CreatureController : MonoBehaviour
{
    public GameObject submarine;

    public float encounterDist = 30f;
    // public float attackDist = 5f;

    public float moveSpeed = 3f;
    public float rotateSpeed = 3f;
    private Transform[] positions;

    public float attackCooldown = 10f;

    public Transform[] pathPositions;

    [Header("Dev Info")]
    public float dist;
    public float rotationDirection;
    public bool attackFlag = false;
    public bool resetFlag = false;
    public bool nextToSub = false;
    public float timer = 0f;
    public Transform targetPosition;
    public Transform prevPathPosition;

    // Start is called before the first frame update
    void Start()
    {
        Transform leftPosition = submarine.transform.Find("Left Position");
        Transform rightPosition = submarine.transform.Find("Right Position");
        Transform frontPosition = submarine.transform.Find("Front Position");
        positions = new Transform[] {leftPosition, rightPosition, frontPosition};
        // TODO: set to first position in path
        targetPosition = pathPositions[0];
    }

    // Update is called once per frame
    void Update()
    {
        dist = checkDist();
        if (dist < encounterDist && !resetFlag)
        {
            prevPathPosition = targetPosition;
            // toggle to change depending on submarine emergency status
            attackFlag = true;
            if (attackFlag)
            {
                targetSubPosition();
                if (transform.position == targetPosition.position)
                {
                    attackSub();
                    // TODO: Determine where these flags get set
                    attackFlag = false;
                    resetFlag = true;
                    // add functionality to go back to what position it was targeting
                    targetPosition = prevPathPosition;
                }
            } else
            {
                // add functionality to go back to what position it was targeting
                targetPosition = prevPathPosition;
            }
            
        }
        moveCreature();
        rotateCreature();

        if (resetFlag)
        {
            timer += Time.deltaTime;
            if (timer >= attackCooldown)
            {
                timer = 0f;
                resetFlag = false;
            }
        }
    }

    // Check distance to submarine
    public float checkDist()
    {
        float dist = Vector3.Distance(transform.position, submarine.transform.position);
        return dist;
    }

    // Determine the target position the creature will move to around the submarine
    void targetSubPosition()
    {
        if (!nextToSub)
        {
            foreach (Transform position in positions)
            {
                float targetPositionDist = Vector3.Distance(transform.position, targetPosition.transform.position);
                float currentDist = Vector3.Distance(transform.position, position.transform.position);
                if (currentDist < targetPositionDist)
                {
                    targetPosition = position;
                }
            }
            if (transform.position == positions[0].transform.position || transform.position == positions[1].transform.position)
            {
                targetPosition = positions[2];
                nextToSub = true;
            }
        }
    }

    public void moveCreature()
    {
        float move = moveSpeed * Time.deltaTime;
        if (dist < encounterDist)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition.transform.position, move);
        }
    }

    // Creature attacks sub (activate leak, causes noise, etc.)
    public void attackSub()
    {
        
        return;
    }

    public void rotateCreature()
    {
        // adjust for offset by lowering focus point
        Vector3 targetDir = submarine.transform.position - transform.position - 3*Vector3.up;
        float step = rotateSpeed * Time.deltaTime;
        Vector3 newRotation = Vector3.RotateTowards(transform.forward, -targetDir, step, 0.0f);
        transform.rotation = Quaternion.LookRotation(newRotation);
    }
}
