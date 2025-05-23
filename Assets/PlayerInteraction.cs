using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    public GameObject GetClosestInteractable()
    {
        Transform closest = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject E in GameObject.FindGameObjectsWithTag("E"))
        {
            float distance = Vector3.Distance(transform.position, E.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = E.transform;
            }
        }

        if (closest != null)
        {
            return closest.gameObject;
        }
        else
        {
            return null;
        }
    }
}
