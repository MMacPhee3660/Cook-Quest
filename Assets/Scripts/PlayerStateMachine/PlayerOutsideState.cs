using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;


public class PlayerOutsideState : PlayerBaseState
{
    float speed;
    float dashCooldown = 0;
    float dashDashCooldown = 0;
    bool dashComplete = true;
    bool isDash = false;

    // Start is called before the first frame update
    public override void EnterState(PlayerStateManager player)
    {
        rb = player.GetComponent<Rigidbody>();
        baseSpeed = 5;
    }

    // Update is called once per frame
    public override void UpdateState(PlayerStateManager player)
    {
        float horzInput = Input.GetAxisRaw("Horizontal");
        float vertInput = Input.GetAxisRaw("Vertical");
        Vector3 input = new Vector3 (horzInput, 0, vertInput);
        input.Normalize();

        if (Input.GetKey(KeyCode.Space) && dashDashCooldown <= 0)
        {
            isDash = true;
            dashComplete = false;
        }
        
        if (isDash)
        {
            Dash(player);
        }
        if (dashCooldown <= 0)
        {
            speed = baseSpeed;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed *= 1.5f;
            }
            
            player.MoveCharacter(rb, input, speed);
            if (input != new Vector3 (0,0,0))
            {
                player.transform.forward = input;
            }
        }
        dashCooldown -= Time.deltaTime;
        dashDashCooldown -= Time.deltaTime;
    }

    void Dash(PlayerStateManager player){
        int dashAcceleration = 225;
        int dashSpeed = 25;
        dashCooldown = 0.175f;
        dashDashCooldown = 0.4f;
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
                speed = 0;
            }
        }
        player.MoveCharacter(rb, speed);
    }
    
}
