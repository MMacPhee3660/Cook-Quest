using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class PlayerStateManager : MonoBehaviour
{
    public PlayerBaseState currentState;
    public PlayerOutsideState outsideState = new PlayerOutsideState();
    public PlayerInsideState insideState = new PlayerInsideState();
    public PlayerVillageState villageState = new PlayerVillageState();
    public PlayerMenuState menuState = new PlayerMenuState();

    // Start is called before the first frame update
    void Start()
    {
        currentState = outsideState;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        //currentState.UpdateState(this);
    }
    
    public void MoveCharacter(Rigidbody rb, Vector3 vector, float speed){
        rb.velocity = vector * speed;
    }
    public void MoveCharacter(Rigidbody rb, float speed){
        rb.velocity = transform.forward * speed;
    }

    public void SwitchState(PlayerBaseState state){
        currentState = state;
        state.EnterState(this);
    }
}
