using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class craftingRequiredItem : MonoBehaviour
{
    public CraftButton parentScript;
    Item[] parentItemsList;
    void Awake()
    {
        parentScript = GetComponentInParent<CraftButton>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
