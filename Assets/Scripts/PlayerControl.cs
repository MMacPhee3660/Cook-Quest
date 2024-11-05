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
    [SerializeField] float velocity;
    private Animator animator;

   void Start(){
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
   }
    void FixedUpdate()
    {
        if(!isDodge){
            float horzInput = Input.GetAxisRaw("Horizontal");
            float vertInput = Input.GetAxisRaw("Vertical"); 
            velocity = 1f;
        
            animator.SetFloat("Xinput",horzInput);
            animator.SetFloat("Yinput",vertInput);

            if(horzInput == 0 && vertInput == 0){
                velocity = 0f;
            }

            animator.SetFloat("Velocity",velocity);
            Vector3 movement = new(horzInput, 0, vertInput);
            movement.Normalize();
        if (isDash){
            Dash();
            transform.position += (transform.forward * speed * Time.deltaTime);
        }
        else if (horzInput != 0 || vertInput != 0){
            transform.forward = new Vector3(input.x,0,input.z);
            transform.position += (input * Time.fixedDeltaTime * speed);
        }
        
        if( Input.GetKey(KeyCode.LeftShift)){
            isSprinting = true;
        }
        else{


        if(isSprinting) {
            speed = baseSpeed * 1.5f;


        if(!isSprinting && !isDash){
            speed = baseSpeed;
        }

            
        }

        if(Input.GetKey(KeyCode.Space) && timeSinceDash >= 0.15){
            isDash = true;
            dashComplete = false;
        }
        if (timeSinceDash > 0.15){
            slowWalk = false;
        }
        else{
            timeSinceDash += Time.deltaTime;
        }
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
                slowWalk = true;
            }
        }
    }
    
    



    
}
     
   
