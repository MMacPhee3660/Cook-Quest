using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    
    [SerializeField] float speed = 5f;
    public float baseSpeed = 5f;

    public bool isSprinting = false;
    public float sprintingMulti = 1.5f;
    public bool slowWalk;
    public bool isDash = false;


   void Start(){
        rb = GetComponent<Rigidbody>();
   }
    void FixedUpdate()
    {
        float horzInput = Input.GetAxisRaw("Horizontal");
        float vertInput = Input.GetAxisRaw("Vertical"); 

    
         Vector3 movement = new(horzInput, 0, vertInput);
         movement.Normalize();

         rb.MovePosition(rb.position + movement * Time.fixedDeltaTime * speed);
         transform.forward = new Vector3(movement.x,0,movement.z);
         if( Input.GetKey(KeyCode.LeftShift)){
            isSprinting = true;
        }
        else{

            isSprinting = false;
        }

        if(isSprinting == true) {
            speed = baseSpeed * 1.5f;

        }

        if(isSprinting == false){
            speed = baseSpeed;
        }

        if(slowWalk){
            baseSpeed = 3;
        }
        else{
            baseSpeed = 5f;
        }

        if(Input.GetKey(KeyCode.Space)){
            isDash = true;

        }


    }
    
    
    
}
     
   
