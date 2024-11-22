using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;


public class PlayerOutsideState : PlayerBaseState
{
    float speed;
    float dashCooldown = 0;
    bool dashComplete = true;
    bool isDash = false;
    // Start is called before the first frame update
    public override void EnterState(PlayerStateManager player)
    {
        baseSpeed = 5;
    }

    // Update is called once per frame
    public override void UpdateState(PlayerStateManager player)
    {
        speed = baseSpeed;
    }

    void Dash(PlayerStateManager player){
        int dashAcceleration = 300;
        int dashSpeed = 35;
        dashCooldown = 5;
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
        player.MoveCharacter(rb, speed);
    }
}
