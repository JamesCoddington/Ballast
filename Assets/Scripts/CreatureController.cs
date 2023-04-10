using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class CreatureController : MonoBehaviour
{
    public GameObject submarine;

    public float encounterDist = 30f;
    // public float attackDist = 5f;

    public float moveSpeed = 1f;
    public float rotateSpeed = 1f;

    public Transform leftPosition;
    public Transform rightPosition;
    public Transform frontPosition;
    private Transform[] positions;

    [Header("Dev Info")]
    public float dist;
    public float rotationDirection;
    public bool attackFlag = false;
    public bool resetFlag = false;
    public Transform targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        positions = new Transform[] {leftPosition,rightPosition,frontPosition};
        targetPosition = frontPosition;
    }

    // Update is called once per frame
    void Update()
    {
        dist = checkDist();
        if (dist < encounterDist)
        {
            if (attackFlag && transform.position == targetPosition.position)
            {
                attackSub();
                attackFlag = false;
                resetFlag = true;
            }
        moveCreature();
        rotateCreature();
        }
    }

    public float checkDist()
    {
        if (!attackFlag) { 
            foreach (Transform position in positions)
            {
                float targetPositionDist = Vector3.Distance(transform.position, targetPosition.transform.position);
                float currentDist = Vector3.Distance(transform.position, position.transform.position);
                if (currentDist < targetPositionDist)
                {
                    targetPosition = position;
                }
            }
        }
        if (transform.position == leftPosition.transform.position || transform.position == rightPosition.transform.position)
        {
            targetPosition = frontPosition;
            attackFlag = true;
        }
        float dist = Vector3.Distance(transform.position, targetPosition.transform.position);
        return dist;
    }

    public void moveCreature()
    {
        float move = moveSpeed * Time.deltaTime;
        if (dist < encounterDist)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition.transform.position, move);
        }
    }


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
