using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duplicator : MonoBehaviour
{
    [SerializeField]
    int duplicates = 10;
    // Start is called before the first frame update
    void Start()
    {
        if (Time.deltaTime == 0)
        {
            for (int x = 0; x < duplicates; x++)
            {
                Instantiate(gameObject);
            }
        }
    }

}
