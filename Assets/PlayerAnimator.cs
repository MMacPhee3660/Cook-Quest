using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
public Animator animator;
    [SerializeField] float velocity;
    void Start()
    {
         animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
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
    }
}
