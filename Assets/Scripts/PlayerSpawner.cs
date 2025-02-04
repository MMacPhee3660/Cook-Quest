using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    float X;
    float Y;
    float Z;
    GameObject player;
    public GameObject spawnpoint;
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
        spawnpoint = GameObject.FindGameObjectWithTag("spawnPoint");
        player = GameObject.FindGameObjectWithTag("Player");
        TeleportTo(spawnpoint);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TeleportTo(GameObject spawnpoint){
        player.transform.position = new Vector3(spawnpoint.transform.position.x,spawnpoint.transform.position.y,spawnpoint.transform.position.z);
    }
}
