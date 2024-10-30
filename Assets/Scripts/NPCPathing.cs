using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

public class NPCPathing : MonoBehaviour
{
    [SerializeField] UnityEngine.Vector3 destination1 = new UnityEngine.Vector3(0,0,0);
    [SerializeField] UnityEngine.Vector3 destination2 = new UnityEngine.Vector3(0,0,0);
    [SerializeField] UnityEngine.Vector3 destination3 = new UnityEngine.Vector3(0,0,0);
    List<UnityEngine.Vector3> nodes = new List<UnityEngine.Vector3>();
    bool nodeReached = false;
    int frameCount = 0;
    int currentNode = 0;
    float timeReached;
    UnityEngine.Vector3 pos;

    void Update()
    {
        if (frameCount == 0)
        {
            nodes.AddRange(new UnityEngine.Vector3[]{destination1,destination2,destination3});
            pos = transform.position;
        }
        if (!nodeReached)
        {
            if (transform.position.Equals(nodes[currentNode]))
            {
                nodeReached = true;
                timeReached = Time.time;
            }
            else
            {
                transform.position = UnityEngine.Vector3.MoveTowards(pos,nodes[currentNode],Time.deltaTime);
            }
        }
        else if (Time.time >= timeReached + 2)
        {
            nodeReached = false;
            pos = transform.position;
            currentNode++;
        }
        frameCount++;
    }

       

}
