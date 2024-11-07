using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    
    [SerializeField] float speed = 5f;
    public float baseSpeed = 5f;

    public bool isSprinting = false;
    public float sprintingMulti = 1.5f;
    public bool slowWalk;
    public bool isDodge = false;
    private float dodgeTime = 2;
    private float dodgeCD;
    private float dodgeDist = 10f;
    private Animator animator;
    [SerializeField] float velocity;
    public bool inShop = false;

   void Start(){
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        
   }

    void FixedUpdate()
    {
        Scene currentScene = SceneManager.GetActiveScene ();
        string sceneName = currentScene.name;
        
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
            


            rb.MovePosition(rb.position + movement * Time.fixedDeltaTime * speed);
            transform.forward = new Vector3(movement.x,0,movement.z);
            
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

            
        }

        if(Input.GetKey(KeyCode.Space) && !inShop){
                isDodge = true;
                transform.Translate(10,0,0);
        }
        
        if(sceneName == "MaywensScene"){
            inShop = true; 
        }

    }


    public void DodgeRoll(){
        isDodge = true;
        
    }
    
    



    
}
     
   
