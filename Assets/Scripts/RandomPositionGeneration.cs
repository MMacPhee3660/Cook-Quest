using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPositionGeneration : MonoBehaviour
{
    [SerializeField]
    private float x = 50;
    [SerializeField]
    private float y = 30;
    // Start is called before the first frame update
    void Start()
    {
        float randX = UnityEngine.Random.Range(-x,x);
        float randY = UnityEngine.Random.Range(-y,y);
        transform.position = new Vector3(randX,0,randY);
    }
}
