using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Quickbuy : MonoBehaviour
{
    private float distance;
    [SerializeField] GameObject E;
    [SerializeField] float price;
    GameObject player;
    GameObject purchase;
    [SerializeField] GameObject hitE;
    
    void Start()
    {
        player = GameObject.Find("PlayerMove");
        E.SetActive(false);
        hitE.SetActive(false);
    }
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        
        if(distance <= 2f){
            hitE.SetActive(true);
            E.transform.position = transform.position;
            E.transform.position += Vector3.up * 1f;
            E.SetActive(true);
            Debug.DrawLine(transform.position, player.transform.position, Color.green);
        }
        else{
            Debug.Log(E.transform.position);
            E.SetActive(false);
            hitE.SetActive(false);
        }
        
        if(Input.GetKeyDown(KeyCode.E)){
            print("a");
        }
    }
}
