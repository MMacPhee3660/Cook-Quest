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

    void Start()
    {
        Pathfinder pathfinder = new Pathfinder((int)(transform.position.x-destination.x)+1, (int)(transform.position.y-destination.y)+1, 1, 1);
        rb = this.GetComponent<Rigidbody>();
        dests = pathfinder.Pathfind();
    }
    void Update()
    {
       /* currentDest = new Vector3(dests[destIndex].x, 0, dests[destIndex].y);
        if (currentDest != transform.position)
        {
            rb.velocity = (currentDest-transform.position).normalized;
        }
        else
        {
            destIndex++;
        }*/
    }
}