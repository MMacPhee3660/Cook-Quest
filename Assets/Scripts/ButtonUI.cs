using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ButtonController : MonoBehaviour
{
    private int itemCount = 0;
    public TMP_Text countText;
    public TMP_Text buyText;
    public GameObject panel;
    public GameObject purchaser;
    public void AddItemButton(){
        itemCount++;
        countText.text = "" + itemCount;
    }
    public void SubtractItemButton(){
        if(itemCount > 0){
            itemCount--;
            countText.text = "" + itemCount;
        }
    }
    public void BuyButton(){
        buyText.text = "Bought!";
    }
    public void firstMenuItem(){
        panel.SetActive(false);
        purchaser.SetActive(true);
        
    }
}
