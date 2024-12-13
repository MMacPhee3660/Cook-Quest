using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public enum SlotTag{Material, Tool, Head, Chest, Legs, Boots}
public enum ActionType{None, Attack, Chop, Mine}

[CreateAssetMenu(menuName = "Scriptable Object/Item")]
public class Item : ScriptableObject
{
   
    
   
    [Header("Only Gameplay")]
    public ActionType actionType;
    public SlotTag itemType;
    public Vector3Int range = new Vector3Int(2,3);

    [Header("Only UI")]
    public bool stackable = true;
    
    
    [Header("Both")]
    public Sprite image;

}
