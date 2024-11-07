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
    [SerializeField] GameObject E;
    GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
        E.SetActive(false);
    }
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if(distance <= 1.25f){
            Debug.DrawLine(transform.position, player.transform.position, Color.green);
            E.SetActive(true);
        }
        else{
            E.SetActive(false);
        }
    }
}
