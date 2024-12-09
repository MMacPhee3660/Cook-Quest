using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using TMPro;

public class Menubuy : MonoBehaviour
{
    private float distance;
    [SerializeField] GameObject E;
    GameObject player;
    GameObject purchase;
    public GameObject testE;
    public GameObject childE;
    public GameObject menu;
    public GameObject menuObject;
    private int itemCount = 0;
    public TMP_Text countText;
    
    void Start()
    {
        player = GameObject.Find("PlayerMove");
        E.SetActive(false);
        menu.SetActive(false);
        menuObject.SetActive(false);
        childE = Instantiate(testE, transform.position,Quaternion.identity);
        childE.transform.position += Vector3.up * 1f;
    }
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        countText.text = itemCount + "";


        if(distance <= 2f){
            E.transform.position = transform.position;
            childE.SetActive(true);
            Debug.DrawLine(transform.position, player.transform.position, Color.green);
        }
        else{
            // Debug.Log(E.transform.position);
            childE.SetActive(false);
        }
        
        if(Input.GetKeyDown(KeyCode.E)){
            print("a");
            if(menu.activeSelf == false && childE.activeSelf == true){
                menu.SetActive(true);
                menuObject.SetActive(true);
            }
            else{
                menu.SetActive(false);
                menuObject.SetActive(false);
            }
        }
    }
}
