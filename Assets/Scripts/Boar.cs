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
                animator.SetBool("Windup", false);
                animator.SetBool("Special", true);
                agent.speed = speed + 100f;
            }
            if (agent.velocity == Vector3.zero && specialPause >= specialWindup + 0.5f)
                {
                    isSpecial = false;
                    animator.SetBool("Special", false);
                    specialPause = 0f;
                }
        }
        if ((!isSpecial) && (targetDistance > specialRange && specialTime >= specialCooldown) && LineOfSight())
        {
            isSpecial = true;
            animator.SetBool("Windup", true);
            specialTime = 0f;
            dest = targetPos + (targetPos - pos).normalized * 5f;
        }
    }
}