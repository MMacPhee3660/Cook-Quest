using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Quickbuy : MonoBehaviour
{

    private bool isNear = false;
    GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
    }
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if(distance <= 5f){
            Debug.DrawLine(transform.position, player.transform.position, Color.green);
        }
        print(distance);
    }
}
