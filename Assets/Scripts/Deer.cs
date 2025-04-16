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
    protected override void TrySpecial()
    {
        if (isSpecial)
        {
            agent.speed = 1f;
            specialPause += Time.deltaTime;
            if (specialPause >= specialWindup)
            {
                agent.speed = speed + 10f;
            }
            if (agent.velocity == Vector3.zero && specialPause >= specialWindup + 0.5f)
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
            dest = targetPos + (targetPos - pos).normalized * 10f;
        }
    }
    protected override void Chase()
    {
        dest = 2 * pos - targetPos;
    }
}