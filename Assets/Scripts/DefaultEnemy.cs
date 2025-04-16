using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Unity.AI.Navigation;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public abstract class DefaultEnemy : MonoBehaviour
{
    public Animator animator;
    protected NavMeshAgent agent;
    protected GameObject target;
    protected Vector3 origin;
    [SerializeField] public int patrolRadius = 10;
    protected Vector3 pos;
    protected Vector3 targetPos;
    protected Vector3 dest;
    protected float targetDistance;
    [SerializeField] public float sightRange = 10;
    [SerializeField] public float specialRange = 5;
    protected NavMeshPath path;
    protected NavMeshSurface surface;
    public LayerMask obstacleLayer;
    protected float patrolTime = 0f;
    protected float specialTime = 0f;
    protected float specialPause = 0f;
    [SerializeField] public float minPause = 3f;
    [SerializeField] public float maxPause = 2f;
    [SerializeField] public float specialCooldown = 5f;
    [SerializeField] public float specialWindup = 0.5f;
    protected float speed;
    protected bool canSeeTarget = false;
    protected bool isSpecial = false;
    protected float patrolDir = 0f;

    protected void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        origin = transform.position;
        dest = origin;
        path = new NavMeshPath();
        speed = agent.speed;
        target = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    protected void Update()
    {
        PathLoop();
    }

    protected void PathLoop()
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
            print("patrollin");
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

    protected void Patrol()
    {
        if (agent.remainingDistance < 0.1)
        {
            patrolTime += Time.deltaTime;
            patrolDir += (float) Math.PI + 0.1f;
            float mag = Random.Range(patrolRadius / 4, patrolRadius);
            float x = (float)(Math.Cos(patrolDir) * mag);
            float y = (float)(Math.Sin(patrolDir) * mag);
            Vector3 tempDest = new Vector3(origin.x + x, origin.y, origin.z + y);
            float pause = Random.Range(minPause, maxPause);
            
            if (agent.CalculatePath(tempDest, path) && patrolTime >= pause)
            {
                dest = tempDest;
                patrolTime = 0f;
            }
        }
    }

    protected abstract void TrySpecial();

    protected abstract void Chase();

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
