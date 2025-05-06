using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Item))]
public class Item_EditorScript : Editor
{
    public override void OnInspectorGUI()
    {

        Item item = (Item) target;

        item.itemType = (ItemType) EditorGUILayout.EnumPopup("Item Type", item.itemType); // type
        item.itemName = EditorGUILayout.TextField("Item Name", item.itemName); // name
        item.ID = EditorGUILayout.IntField("ID", item.ID); // ID
        item.stackable = EditorGUILayout.Toggle("Stackable", item.stackable); // stackable
        item.price = EditorGUILayout.IntField("Price", item.price); // price
        item.image = (Sprite) EditorGUILayout.ObjectField("Image", item.image, typeof(Sprite), true);

        if(item.itemType == ItemType.Food){
            item.isMenuItem = EditorGUILayout.Toggle("Is Menu Item", item.isMenuItem);
            if(item.isMenuItem){
                            item.servingSize = EditorGUILayout.IntField("Serving Size", item.servingSize);
                        }
        }
        if(item.itemType == ItemType.Tool){
            item.stackable = false;
            item.actionType = (ActionType) EditorGUILayout.EnumPopup("Action Type", item.actionType);
            item.power = EditorGUILayout.IntField("Power", item.power);
        }
        if(item.itemType == ItemType.Material){
            
            
        }
    }
}
