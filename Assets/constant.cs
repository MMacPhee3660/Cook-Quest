using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class constant : MonoBehaviour
{

    public static GameObject instance;
    void Awake()
    {
        if(instance == null){
            instance = this.gameObject;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance !=this){
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
