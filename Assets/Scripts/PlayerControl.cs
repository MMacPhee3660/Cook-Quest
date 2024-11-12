using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public bool inShop = false;
    public bool isDodge = false;
    void Start(){
        animator = GetComponent<Animator>();
   }



    void FixedUpdate()
    {
        //Debug.Log(speed);
        float horzInput = Input.GetAxisRaw("Horizontal");
        float vertInput = Input.GetAxisRaw("Vertical"); 
        Scene currentScene = SceneManager.GetActiveScene ();
        string sceneName = currentScene.name;
        
        if(!isDodge){
            //float horzInput = Input.GetAxisRaw("Horizontal");
            //float vertInput = Input.GetAxisRaw("Vertical"); 
            velocity = 1f;
        
            animator.SetFloat("Xinput",horzInput);
            animator.SetFloat("Yinput",vertInput);

        Vector3 input = new(horzInput, 0, vertInput);
        input.Normalize();


            Debug.Log(vertInput);
            Debug.Log(horzInput);


            if( Input.GetKey(KeyCode.LeftShift) && !inShop){
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
            if(inShop){
                baseSpeed = 2;
            }
            else{
                baseSpeed = 5f;
            }
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

        if(Input.GetKey(KeyCode.Space) && !inShop){
                isDodge = true;
                transform.Translate(10,0,0);
        }
        
        if(sceneName == "MaywensScene"){
            inShop = true; 
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
    
    void Dash(){
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
}
     
   
