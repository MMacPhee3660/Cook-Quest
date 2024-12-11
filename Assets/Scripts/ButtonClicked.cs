using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonClicked : MonoBehaviour
{

private Button button; 
public Button add;
    public void OnMouseOver()
    {
        // if(Input.GetButton("add_item")){
        //     itemCount++;
        // }
        button = GetComponent<Button>();
        button.onClick.AddListener(btnClick);
    }

    void btnClick(){
        print("click");
    }
}
