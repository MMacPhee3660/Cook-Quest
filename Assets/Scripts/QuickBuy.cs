using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickBuy : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Capsule
    public GameObject E
    public GameObject "Lower Left Object"
    float dist = Vector3.Distance(Capsule.position, "Lower Left Object".position);

    void Start()
    {
        print(dist)
        // E.disable()
        // if Capsule.trasform position < 10f{
        //     E.enable()
        // }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
    }
}
