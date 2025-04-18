using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Boar : DefaultEnemy
{
    protected override void PathLoop()
    {
        agent.speed = speed;
        specialTime += Time.deltaTime;
        pos = transform.position;
        targetPos = target.transform.position;
        targetDistance = Vector3.Distance(pos, targetPos);
        
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

    protected override void Chase()
    {
        dest = targetPos;
    }
    protected override void TrySpecial()
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
                    specialPause = 0f;
                }
        }
        if ((!isSpecial) && (targetDistance > specialRange && specialTime >= specialCooldown) && LineOfSight())
        {
            isSpecial = true;
            specialTime = 0f;
            animator.SetBool("Rush",true);
            dest = targetPos + (targetPos - pos).normalized * 5f;
        }
    }
}