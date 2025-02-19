using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CollisionTester : MonoBehaviour
{

    private void OnTriggerEnter(Collider collision)
    {
        ProcessCollision(collision.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        ProcessCollision(collision.gameObject);
    }
    void ProcessCollision(GameObject collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            DoDamageToPlayer();
        }
    }
    
    void DoDamageToPlayer()
    {
        Debug.Log("Hit!");
    }
} 