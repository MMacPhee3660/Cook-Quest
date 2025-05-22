using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class RestaurantManager : MonoBehaviour
{
    public List<Tuple<Item, int, GameObject>> menuItems = new List<Tuple<Item, int, GameObject>>();
    public List<GameObject> seats = new List<GameObject>();
    public GameObject player;
    public PlayerStats playerStats;
    public int servingsLeft;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = player.GetComponent<PlayerStats>();
        seats.AddRange(GameObject.FindGameObjectsWithTag("Seat"));
        StartCoroutine(GenerateOrder());
    }

    void Update()
    {
        
    }
    public IEnumerator GenerateOrder()
    {
        Debug.Log(menuItems.Count);
        if (menuItems.Count != 0)
        {

            int randomIndex = UnityEngine.Random.Range(0, menuItems.Count);
            Item item = menuItems[randomIndex].Item1;
            GameObject menuPlate = menuItems[randomIndex].Item3;
            MenuPlate menuPlateScript = menuPlate.GetComponent<MenuPlate>();
            foreach (GameObject seat in seats)
            {
                servingsLeft = item.servingSize;
                Restaurantseat restaurantSeat = seat.GetComponent<Restaurantseat>();
                if (!restaurantSeat.isOccupied && item.servingSize > 0)
                {
                    restaurantSeat.orderedItem = item;
                    restaurantSeat.isOccupied = true;
                    restaurantSeat.spriteRenderer.sprite = item.image;
                    playerStats.money += item.price;
                    menuPlateScript.servingsLeft--;
                    menuPlateScript.RefreshServingCount();
                    if (menuPlateScript.servingsLeft <= 0)
                    {
                        menuItems.RemoveAt(randomIndex);
                    }
                    break;
                }
            }
        }
        else
        {
            Debug.Log("no items");
        }
        yield return new WaitForSeconds(UnityEngine.Random.Range(1,3));
        StartCoroutine(GenerateOrder());
    }


}
