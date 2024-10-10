using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberF15 : MonoBehaviour
{
    void Update()
    {
        if (transform.position.y < -10)
            Respawn();
    }
    
    private void Respawn()
    {
        transform.position = new Vector3(0, 10);
    }
}
