using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class inventor : MonoBehaviour
{
   [Header("UI")]
   public Image image;

   [HideInInspector] public Transform parentAfterDrag;

    public void OnBeginDrag(PointerEventData eventData){
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
    }


    public void OnDrag(PointerEventData eventData){
        transform.position = Input.mousePosition;
    }


    public void OnEndDrag(){
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
    }
}

