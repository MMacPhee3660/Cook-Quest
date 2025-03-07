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
    float targetDistance;
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
    bool isSpecial = false;

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
        pos = transform.position;
        targetPos = target.transform.position;
        targetDistance = Vector3.Distance(pos, targetPos);
        //print(targetDistance);
        

        if ((!isSpecial) && targetDistance > aggroRange)
        {
            Patrol();
            print("patrolling");
        }
        else
        {
            TrySpecial();
            print("trying special");
        }
        agent.SetDestination(dest);
    }
    private void Patrol()
    {
        if (agent.remainingDistance < 0.1)
        {
            print("stop patrol");
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

    private void TrySpecial()
    {
        if (isSpecial)
        {
            if (agent.remainingDistance < 0.1)
            {
                isSpecial = false;
                specialTime = 0f;
                agent.speed = speed;
                print("stop special");
            }
        }
        if ((!isSpecial) && (targetDistance > specialRange && specialTime >= specialCooldown))
        {
            isSpecial = true;
            dest = targetPos;
            agent.speed = speed + 30f;
        }
    }
}