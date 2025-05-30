using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Wallet : MonoBehaviour
{
    Money moneyobj;
    int money;
    public TMP_Text inWallet;
    // public Quickbuy itemPrice;
    public GameObject[] DisplayObjects;
    // public GameObject nearestObj;
    float distance;
    // float nearestDist = 10000;
    public GameObject player;
    PlayerStats moneyScript;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        moneyScript = player.GetComponentInChildren<PlayerStats>();
        money = moneyScript.money;
        // itemPrice = Quickbuy.price;
        DisplayObjects = GameObject.FindGameObjectsWithTag("DisplayObj");
        // player = GameObject.Find("PlayerMove");
    }

    // Update is called once per frame
    void Update()
    {
        money = moneyScript.money;
        inWallet.text = "$: " + money;

        // for(int i = 0; i < DisplayObjects.Length; i++){
        //     distance = Vector3.Distance(player.transform.position, DisplayObjects[i].transform.position);

        //     if(distance < nearestDist){
        //         nearestObj = DisplayObjects[i];
        //         nearestDist = distance;
        //     }
        // }
    }
}
