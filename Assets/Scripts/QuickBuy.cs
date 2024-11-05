using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Quickbuy : MonoBehaviour
{

    private bool isNear = false;
    private float distance = 5f;



    [SerializeField] Transform obj1;
    GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
    }
    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, player.transform.position);
        Debug.DrawLine(transform.position, player.transform.position, Color.green);
        
    }
}
