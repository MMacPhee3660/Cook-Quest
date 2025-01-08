using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public float X;
    public float Y;
    public float Z;
    GameObject player;
    public GameObject spawnpoint;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = new Vector3(spawnpoint.transform.position.x,spawnpoint.transform.position.y,spawnpoint.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
