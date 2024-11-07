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
    public bool isDash = false;
    private bool dashComplete = false;
    private float timeSinceDash = 0.5f;
    public Animator animator;
    [SerializeField] float velocity;
    void Start(){
        animator = GetComponent<Animator>();

    }

    void FixedUpdate()
    {
        //Debug.Log(speed);
        float horzInput = Input.GetAxisRaw("Horizontal");
        float vertInput = Input.GetAxisRaw("Vertical"); 

        if(horzInput == 0 && vertInput == 0){
            velocity = 0;
        }
        else{
            velocity = 1;
        }

        animator.SetFloat("Velocity", velocity);
        animator.SetFloat("Xinput",horzInput);
        animator.SetFloat("Yinput",vertInput);
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
        
        if( Input.GetKey(KeyCode.LeftShift)){
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
        else{
            baseSpeed = 5f;
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
     
   
