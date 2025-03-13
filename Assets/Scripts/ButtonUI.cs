using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ButtonUI : MonoBehaviour
{
    private int itemCount = 0;
    public TMP_Text countText;
    public TMP_Text buyText;
    public GameObject scroll;
    public GameObject purchaser;

    public void AddItemButton(){
        Debug.Log("Add");
        Debug.Log(itemCount);
        itemCount++;
        Debug.Log(itemCount);
        countText.text = "" + itemCount;
       
    }
    public void SubtractItemButton(){
        if(itemCount > 0){
            itemCount--;
            countText.text = "" + itemCount;
            Debug.Log("minus");
        }
    }
    public void BuyButton(){
        buyText.text = "Bought!";
        Debug.Log("bought");
    }
    public void menuItem(){
        scroll.SetActive(false);
        purchaser.SetActive(true);
        
    }
    public void menuBack(){
        scroll.SetActive(true);
        purchaser.SetActive(false);
        Debug.Log("back");
    }
}
