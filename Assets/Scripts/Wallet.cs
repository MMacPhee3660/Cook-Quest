using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Wallet : MonoBehaviour
{
    private int money = 100;
    public TMP_Text inWallet;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        inWallet.text = "$: " + money;
        
        if(Input.GetKeyDown(KeyCode.E)){
            money--;
        }
    }
}
