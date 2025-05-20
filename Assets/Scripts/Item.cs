using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public enum ItemType{Material, Tool, Head, Chest, Legs, Boots, Food}
public enum ActionType{None, Attack, Chop, Mine}

[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class Item : ScriptableObject
{
    [Header("Internal")] 
    public int ID;
   
    [Header("Only Gameplay")]
    public bool isMenuItem;
    public ActionType actionType;
    public ItemType itemType;
    public int power;

    [Header("Only UI")]
    public bool stackable = true;
    public string itemName;

    
    
    [Header("Both")]
    public int servingSize;
    public Sprite image;
    public int price;

}
