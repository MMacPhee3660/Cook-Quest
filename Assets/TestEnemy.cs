using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class TestEnemy : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] GameObject target;
    Vector3 origin;
    [SerializeField] int patrolRadius = 10;
    Vector3 pos;
    Vector3 targetPos;
    Vector3 dest;
    [SerializeField] float aggroRange = 10;
    [SerializeField] float specialRange = 5;
    float pi;
    NavMeshPath path;
    private float patrolTime = 0f;
    private float specialTime = 0f;
    [SerializeField] float minPause = 3f;
    [SerializeField] float maxPause = 2f;
    [SerializeField] float specialCooldown = 5f;
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        origin = transform.position;
        dest = origin;
        pi = (float) Math.PI;
        path = new NavMeshPath();
        speed = agent.speed;
    }

    // Update is called once per frame
    void Update()
    {
        specialTime += Time.deltaTime;
        agent.speed = speed;
        pos = transform.position;
        targetPos = target.transform.position;
        float distance = Vector3.Distance(pos, targetPos);
        print(distance);

        if (distance > aggroRange)
        {
            Patrol();
        }
        else
        {
            if (distance > specialRange && specialTime >= specialCooldown)
            {
                Special();
            }
            dest = targetPos;
        }
        agent.SetDestination(dest);
    }
    private void Patrol()
    {
        if (Vector3.Distance(pos, dest) <= agent.stoppingDistance)
        {
            patrolTime += Time.deltaTime;
            float dir = Random.Range(0, 2 * pi);
            int mag = Random.Range(0, patrolRadius);
            int x = (int)(Math.Cos(dir) * mag);
            int y = (int)(Math.Sin(dir) * mag);
            Vector3 tempDest = new Vector3(origin.x + x, origin.y, origin.z + y);
            float pause = Random.Range(minPause, maxPause);
            
            if (agent.CalculatePath(tempDest, path) && patrolTime >= pause)
            {
                dest = tempDest;
                patrolTime = 0f;
            }
            
        }
    }

    private void Special()
    {
        agent.speed += 10f;
        specialTime = 0f;
    }
}