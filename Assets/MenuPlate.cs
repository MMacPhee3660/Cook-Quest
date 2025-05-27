using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


public class MenuPlate : MonoBehaviour
{
    private float distance;
    [SerializeField] GameObject E;
    GameObject player;
    GameObject childE;
    public Item displayItem;
    public int servingsLeft;
    public GameObject plateObj;
    public TextMeshPro servingLeftText;


    public InventoryItem selectedItem;
    GameObject inventoryManagerObj;
    InventoryManager inventoryManager;
    SpriteRenderer spriteRenderer; //the sprite that is displayed

    [SerializeField] RestaurantManager restaurantManager;

    void Start()
    {
        plateObj = this.gameObject;
        childE = E;
        inventoryManagerObj = GameObject.FindGameObjectWithTag("InventoryManager");
        inventoryManager = inventoryManagerObj.GetComponent<InventoryManager>();
        player = GameObject.Find("PlayerMove");
       
        childE = Instantiate(E, transform.position, Quaternion.identity);
         E.SetActive(false);
        childE.transform.position += Vector3.up * 1f;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance <= 2f && this.gameObject == player.GetComponent<PlayerInteraction>().GetClosestInteractable())
        {
            E.transform.position = transform.position;
            childE.SetActive(true);
            Debug.DrawLine(transform.position, player.transform.position, Color.green);
        }
        else
        {
            childE.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.E) && distance <= 2f && this.gameObject == player.GetComponent<PlayerInteraction>().GetClosestInteractable())
        {
            SetMenuItem(inventoryManager.GetSelectedItem());
        }
        if (displayItem != null && servingsLeft <= 0  )
        {
            spriteRenderer.sprite = null;
        }
    }

    public void SetMenuItem(Item receivedItem)
    {
        displayItem = receivedItem;
        servingsLeft = displayItem.servingSize;
        servingLeftText.text = servingsLeft.ToString();
        if (receivedItem.itemType == ItemType.Food)
        {
            spriteRenderer.sprite = displayItem.image;
            selectedItem = inventoryManager.GetSelectedItemSlot();
            selectedItem.count--;
            selectedItem.RefreshCount();
            restaurantManager.menuItems.Add(new Tuple<Item, int, GameObject>(displayItem, displayItem.servingSize, plateObj));
            if (selectedItem.count == 0)
            {
                Destroy(selectedItem.gameObject);
            }

        }
    }

    public void RefreshServingCount()
    {
        servingLeftText.text = servingsLeft.ToString();
        bool textActive = servingsLeft > 1;
        servingLeftText.gameObject.SetActive(textActive);
    }
}
