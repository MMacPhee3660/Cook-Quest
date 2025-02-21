
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
  [SerializeField] bool freezeXZAxis = true;

    private void Update()
    {
        if (Camera.main != null)
        {
            if(freezeXZAxis){
    
            transform.rotation = Quaternion.Euler(0f, Camera.main.transform.rotation.eulerAngles.y, 0f );
    
        }
        else{
            transform.rotation = Camera.main.transform.rotation;
        }
        }
    }
}
