using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;


public class PlayerMenuState : PlayerBaseState
{
    float speed;
    // Start is called before the first frame update
    public override void EnterState(PlayerStateManager player)
    {
        rb = player.GetComponent<Rigidbody>();
        baseSpeed = 0;
    }

    // Update is called once per frame
    public override void UpdateState(PlayerStateManager player)
    {
        float horzInput = Input.GetAxisRaw("Horizontal");
        float vertInput = Input.GetAxisRaw("Vertical");
        Vector3 input = new Vector3 (horzInput, 0, vertInput);
        input.Normalize();
    }
}