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
    private bool stunned;
    public bool isDash = false;
    private bool dashComplete = false;
    private float timeSinceDash = 0.5f;
    [SerializeField] float velocity;
    public bool inShop = false;
    public bool isDodge = false;
    void Start(){
   }



    void FixedUpdate()
    {
        //Debug.Log(speed);
        float horzInput = Input.GetAxisRaw("Horizontal");
        float vertInput = Input.GetAxisRaw("Vertical"); 
        Scene currentScene = SceneManager.GetActiveScene ();
        string sceneName = currentScene.name;
        
        if(!isDodge){
            horzInput = Input.GetAxisRaw("Horizontal");
            vertInput = Input.GetAxisRaw("Vertical"); 
            velocity = 1f;

        Vector3 input = new(horzInput, 0, vertInput);
        input.Normalize();
        if(sceneName == "MaywensScene"){
            inShop = true; 
        }

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
        
        if( Input.GetKey(KeyCode.LeftShift) && !isDash ){
            isSprinting = true;
        }
        else{

            isSprinting = false;
        }

        if(Input.GetKey(KeyCode.Space) && !inShop){
                isDodge = true;
                transform.Translate(10,0,0);
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
                baseSpeed = 0;
            }
        }
    }
    
    }
}
     
   
