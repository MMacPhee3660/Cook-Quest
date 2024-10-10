using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private void OnEnable()
    {
        Respawn();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -10)
            Respawn();
    }
    private void Respawn()
    {
        float randomX = UnityEngine.Random.Range(-5,5);
        float randomY = UnityEngine.Random.Range(10,30);
        transform.position += new Vector3(randomX, randomY);
        var rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = Vector3.zero;
    }
}
