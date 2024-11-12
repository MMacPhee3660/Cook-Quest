using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    private int money = 100;
    public Text Wallet;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Wallet.text = "$: " + money;
        
        if(Input.GetKeyDown(KeyCode.E)){
            money--;
        }
    }
}
