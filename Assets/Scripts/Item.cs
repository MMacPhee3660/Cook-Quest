using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SlotTag{None, Head, Chest, Legs, Boots}

[CreateAssetMenu(menuName = "Scriptable Object/Item")]
public class Item : ScriptableObject
{
    public Sprite sprite;
    public SlotTag itemTag;
   
    [Header("If the item can be equipped")]
    public GameObject equipmentPrefab;
}
