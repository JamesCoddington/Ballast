using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureController : MonoBehaviour
{
    public GameObject submarine;

    public float encounterDist = 15f;
    public float attackDist = 5f;

    public float movementSpeed = 1f;
    public float rotateSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float dist = checkDist();
        if (dist < encounterDist)
        {
            if (dist > attackDist)
            {
                moveCreature();
            } else
            {
                attackSub();
            }
        }

        rotateCreature();
    }

    public float checkDist()
    {
        float dist = Vector3.Distance(submarine.transform.position, transform.position);
        return dist;
    }

    public void moveCreature()
    {
        float move = movementSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, submarine.transform.position, move);
    }

    public void attackSub()
    {
        return;
    }

    public void rotateCreature()
    {
        Vector3 targetDir = submarine.transform.position - transform.position;
        float step = rotateSpeed * Time.deltaTime;
        Vector3 newRotation = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
        transform.rotation = Quaternion.LookRotation(newRotation);
    }
}
