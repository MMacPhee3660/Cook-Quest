using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class DefaultEnemy : MonoBehaviour
{
    public Animator animator;
    NavMeshAgent agent;
    [SerializeField] public GameObject target;
    Vector3 origin;
    [SerializeField] public int patrolRadius = 10;
    Vector3 pos;
    Vector3 targetPos;
    Vector3 dest;
    float targetDistance;
    [SerializeField] public float sightRange = 10;
    [SerializeField] public float specialRange = 5;
    NavMeshPath path;
    public LayerMask obstacleLayer;
    float patrolTime = 0f;
    float specialTime = 0f;
    float specialPause = 0f;
    [SerializeField] public float minPause = 3f;
    [SerializeField] public float maxPause = 2f;
    [SerializeField] public float specialCooldown = 5f;
    [SerializeField] public float specialWindup = 0.5f;
    float speed;
    bool canSeeTarget = false;
    bool isSpecial = false;
    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        origin = transform.position;
        dest = origin;
        path = new NavMeshPath();
        speed = agent.speed;
    }

    // Update is called once per frame
    public void Update()
    {
        PathLoop();
    }

    public void PathLoop()
    {
        agent.speed = speed;
        specialTime += Time.deltaTime;
        pos = transform.position;
        targetPos = target.transform.position;
        targetDistance = Vector3.Distance(pos, targetPos);
        //print(targetDistance);
        
        if (isSpecial || LineOfSight())
        {
            canSeeTarget = true;
        }
        else if (targetDistance > sightRange)
        {
            canSeeTarget = false;
        }
        

        if (!canSeeTarget)
        {
            Patrol();
        }
        
        else
        {
            TrySpecial();

            if (!isSpecial)
            {
                Chase();
            }
        }
        agent.SetDestination(dest);
        FindDirection();
    }

    public void Patrol()
    {
        if (agent.remainingDistance < 0.1)
        {
            patrolTime += Time.deltaTime;
            float dir = Random.Range(0, 2 * (float) Math.PI);
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

    public void TrySpecial()
    {
        if (isSpecial)
        {
            agent.speed = 0f;
            specialPause += Time.deltaTime;
            if (specialPause >= specialWindup)
            {
                agent.speed = speed + 100f;
            }
            if (agent.velocity == Vector3.zero && specialPause >= specialWindup + 0.5f)
                {
                    isSpecial = false;
                    animator.SetBool("Rush",false);
                    specialTime = 0f;
                    specialPause = 0f;
                }
        }
        if ((!isSpecial) && (targetDistance > specialRange && specialTime >= specialCooldown) && LineOfSight())
        {
            isSpecial = true;
            animator.SetBool("Rush",true);
            dest = targetPos + (targetPos - pos).normalized * 5f;
        }
    }

    public void Chase()
    {
        dest = targetPos;
    }

    public bool LineOfSight()
    {
        RaycastHit hitInfo;
        Physics.Raycast(pos, (targetPos - pos).normalized, out hitInfo, sightRange, obstacleLayer);
        if (hitInfo.collider != null) {
            return hitInfo.collider.gameObject == target;
        }
        else
        {
        return false;
        }
    }

    public void FindDirection()
    {
        if (agent.velocity.x > 0)
        {

        }
        else 
        {
            
        }
    }
}
