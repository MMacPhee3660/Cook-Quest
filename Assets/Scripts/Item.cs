using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public enum ItemType{Material, Tool, Head, Chest, Legs, Boots}
public enum ActionType{None, Attack, Chop, Mine}

[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class Item : ScriptableObject
{
   
    [Header("Internal")] 
    public int ID;
   
    [Header("Only Gameplay")]
    public ActionType actionType;
    public ItemType itemType;
    public Vector3Int range = new Vector3Int(2,3);

    [Header("Only UI")]
    public bool stackable = true;
    
    
    [Header("Both")]
    public Sprite image;
    public int price;

}
