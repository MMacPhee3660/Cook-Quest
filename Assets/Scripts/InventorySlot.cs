using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class InventorySlot : MonoBehaviour, IDropHandler
{
    public Image image;
    public Color selectedColor,notSelectedColor;

    public void Select(){
        image.color = selectedColor;
    }
    public void Deselect(){
        image.color = notSelectedColor;
    }

    void Awake(){
        Deselect();
    }


        public void OnDrop(PointerEventData eventData){
            GameObject dropped = eventData.pointerDrag;
            InventoryItem draggableItem = dropped.GetComponent<InventoryItem>();
            draggableItem.parentAfterDrag = transform;

    }
}
