using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Deer : DefaultEnemy
{
    protected override void PathLoop()
    {
        agent.speed = speed;
        specialTime += Time.deltaTime;
        pos = transform.position;
        targetPos = target.transform.position;
        targetDistance = Vector3.Distance(pos, targetPos);
        
        if (LineOfSight())
        {
            canSeeTarget = true;
        }
        else if (!isSpecial)
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
                animator.SetBool("Running",true);
            }
        }
        agent.SetDestination(dest);
        FindDirection();
    }

    protected override void TrySpecial()
    {
        if (isSpecial)
        {
            agent.speed = speed / 2;
            specialPause += Time.deltaTime;
            if (specialPause >= specialWindup)
            {
                agent.speed = speed + 10f;
            }
            if (agent.velocity == Vector3.zero && specialPause > specialWindup + 0.5f)
                {
                    isSpecial = false;
                    animator.SetBool("Rush",false);
                    specialPause = 0f;
                }
        }
        if ((!isSpecial) && (targetDistance < specialRange && specialTime >= specialCooldown) && LineOfSight())
        {
            isSpecial = true;
            specialTime = 0f;
            animator.SetBool("Rush",true);
            Vector3 perpVector = Vector3.Cross(targetPos - pos, Vector3.up).normalized * 10;
            if (Random.value < 0.5)
            {
                dest = targetPos + perpVector;
            }
            else
            {
                dest = targetPos - perpVector;
            }
            
        }
    }
    protected override void Chase()
    {
        dest = 2 * pos - targetPos;
    }
}