using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform Player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Player.position;
    }
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
}
