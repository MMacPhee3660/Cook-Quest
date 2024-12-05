using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolAnimator : MonoBehaviour
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

        if(Input.GetKeyDown(KeyCode.F)){
            animator.SetTrigger("Swing");
        }
    }

}
