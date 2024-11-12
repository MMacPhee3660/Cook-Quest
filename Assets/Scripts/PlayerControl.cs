using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    public float baseSpeed = 5f;
    public bool isSprinting = false;
    public float sprintingMulti = 1.5f;
    public bool slowWalk;
    private bool stunned;
    public bool isDash = false;
    private bool dashComplete = false;
    private float timeSinceDash = 0.5f;
    void Start(){

    }

    void FixedUpdate()
    {
        //Debug.Log(speed);
        float horzInput = Input.GetAxisRaw("Horizontal");
        float vertInput = Input.GetAxisRaw("Vertical"); 

        Vector3 input = new(horzInput, 0, vertInput);
        input.Normalize();


 

        if (isDash){
            Dash();
            transform.position += speed * Time.deltaTime * transform.forward;
        }
        else if (horzInput != 0 || vertInput != 0){
            transform.forward = new Vector3(input.x, 0, input.z);
            transform.position += speed * Time.fixedDeltaTime * input;
        }
        
        if( Input.GetKey(KeyCode.LeftShift) && !isDash ){
            isSprinting = true;
        }
        else{

            isSprinting = false;
        }

        if(isSprinting) {
            speed = baseSpeed * 1.5f;

        }

        if(!isSprinting && !isDash){
            speed = baseSpeed;
        }

        if(slowWalk){
            baseSpeed = 3;
        }
        else if (timeSinceDash > 0.15){
            baseSpeed = 5f;
        }

        if(Input.GetKey(KeyCode.Space) && timeSinceDash >= 0.15){
            isDash = true;
            dashComplete = false;
        }
        timeSinceDash += Time.deltaTime;
    }
    
    private void Dash(){
        int dashAcceleration = 300;
        int dashSpeed = 35;
        timeSinceDash = 0;
        if (speed < dashSpeed && !dashComplete){
            speed += Time.deltaTime * dashAcceleration;
        }
        else {
            dashComplete = true;
        }
        if (dashComplete){
            if (speed > 0){
                speed -= Time.deltaTime * dashAcceleration;
            }
            else {
                isDash = false;
                baseSpeed = 0;
            }
        }
    }
    
    
}
     
   
