using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class RestaurantManager : MonoBehaviour
{
    public List<Tuple<Item, int>> menuItems = new List<Tuple<Item, int>>();
    public List<GameObject> seats = new List<GameObject>();
    public GameObject player;
    public PlayerStats playerStats;
    public int servingsLeft;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = player.GetComponent<PlayerStats>();
        seats.AddRange(GameObject.FindGameObjectsWithTag("Seat"));
    }

    void Update()
    {
        StartCoroutine(GenerateOrder());
    }
    public IEnumerator GenerateOrder()
    {
        int randomIndex = UnityEngine.Random.Range(0, menuItems.Count);
        Item item = menuItems[randomIndex].Item1;
        int amount = menuItems[randomIndex].Item2;
        Debug.Log(item.itemName + " " + amount);
        foreach (GameObject seat in seats)
        {
            servingsLeft = item.servingSize;
            Restaurantseat restaurantSeat = seat.GetComponent<Restaurantseat>();
            if (!restaurantSeat.isOccupied && item.servingSize > 0)
            {
                MenuPlate menuPlate = item.GetComponentInParent<MenuPlate>();
                restaurantSeat.orderedItem = item;
                restaurantSeat.isOccupied = true;
                restaurantSeat.spriteRenderer.sprite = item.image;
                playerStats.money += item.price;
                menuPlate.servingsLeft--;
                break;
            }
        }
        
        yield return new WaitForSeconds(3);
    }


}
