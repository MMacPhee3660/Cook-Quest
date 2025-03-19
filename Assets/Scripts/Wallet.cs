using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Wallet : MonoBehaviour
{
    Money moneyobj;
    public int money;
    public TMP_Text inWallet;
    // public Quickbuy itemPrice;
    public GameObject[] DisplayObjects;
    // public GameObject nearestObj;
    float distance;
    // float nearestDist = 10000;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        // itemPrice = Quickbuy.price;
        DisplayObjects = GameObject.FindGameObjectsWithTag("DisplayObj");
        player = GameObject.Find("PlayerMove");
        money = moneyobj.moneyint;
    }

    // Update is called once per frame
    void Update()
    {
        
        inWallet.text = "$: " + moneyobj.moneyint;

        // for(int i = 0; i < DisplayObjects.Length; i++){
        //     distance = Vector3.Distance(player.transform.position, DisplayObjects[i].transform.position);

        //     if(distance < nearestDist){
        //         nearestObj = DisplayObjects[i];
        //         nearestDist = distance;
        //     }
        // }
    }
}
