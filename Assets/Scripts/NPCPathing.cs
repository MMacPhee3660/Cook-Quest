using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class NPCPathing : MonoBehaviour
{
    
    List<Vector2> dests;
    Rigidbody rb;
    [SerializeField] Vector2 destination = new Vector2(0,0);
    int destIndex = 0;
    Vector3 currentDest;
    Boolean isMoving = false;

    void Start()
    {
        Vector2 startPos = new Vector2((int)Math.Abs(transform.position.x), (int)Math.Abs(transform.position.z));
        Vector2 endPos = new Vector2(0,0);
        Pathfinder pathfinder = new Pathfinder();
        rb = this.GetComponent<Rigidbody>();
        dests = pathfinder.Pathfind(startPos,endPos);
    }
    void Update()
    {
        currentDest = new Vector3(dests[destIndex].x, 0, dests[destIndex].y);
        if (currentDest != transform.position && !isMoving)
        {
            rb.velocity = (currentDest-transform.position).normalized;
            isMoving = true;
        }
        else
        {
            destIndex++;
            isMoving = false;
        }
    }
}