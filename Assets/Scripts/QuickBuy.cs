using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Quickbuy : MonoBehaviour
{

    [Header("Type")]
    public Item item;
    // public Money money;
    public int money;
    private float distance;
    [SerializeField] GameObject E;
    public float price;
    GameObject player;
    GameObject purchase;
    public GameObject testE;
    public GameObject childE;
    private int x = 0;
    PlayerStats moneyScript;
    public InventoryManager inventoryManager;
    


    void Awake(){
        GameObject inventoryManagerObj = GameObject.FindGameObjectWithTag("InventoryManager");
        inventoryManager = inventoryManagerObj.GetComponent<InventoryManager>();
    }
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        moneyScript = player.GetComponentInChildren<PlayerStats>();
        money = moneyScript.money;
        E.SetActive(false);
        childE = Instantiate(testE, transform.position,Quaternion.identity);
        childE.transform.position += Vector3.up * 1f;
        
    }
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if(distance <= 2f){
            E.transform.position = transform.position;
            childE.SetActive(true);
            Debug.DrawLine(transform.position, player.transform.position, Color.green);
        }
        else{
            childE.SetActive(false);
        }
        
        if(Input.GetKeyDown(KeyCode.E) && childE.activeSelf){
            x++;
            Debug.Log("getitem");
            GetItem(item.ID);
            
            
            
        }
    }
    public void GetItem(int id){
        inventoryManager.AddItem(inventoryManager.itemsToPickup[id]);
        moneyScript.money -= item.price;
    }
    


}
