using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed; // Player movement speed

    void Awake()
    {
        speed = 5;
    }
    void Update()
    {
        if(Input.GetKey(KeyCode.W)) { // Controls movements in all four directions
            transform.Translate(Vector3.up * testVert(Vector2.up));
        }
        if(Input.GetKey(KeyCode.A)) {
            transform.Translate(Vector3.left * testHor(Vector2.left));
        }
        if(Input.GetKey(KeyCode.S)) {
            transform.Translate(Vector3.down * testVert(Vector2.down));
        } 
        if(Input.GetKey(KeyCode.D)) {
            transform.Translate(Vector3.right * testHor(Vector2.right));
        }
    }

    private float testHor(Vector2 dir) {
        Vector2 origin = transform.position;
        float offset;
        if (dir.Equals(Vector2.left)) {
            offset = -0.125f; // Offset for left side of character
        } else {
            offset = 0.125f; // Offset for right side of character
        }
            origin.x += offset;
        RaycastHit2D raycast = Physics2D.Raycast(origin, dir, speed * Time.deltaTime);

        if(raycast.collider != null && raycast.collider.gameObject.tag == "Collidable") { // If raycast hits something tagged "Collidable"
            float distance = Math.Abs(raycast.point.x - origin.x);
            return distance;
        }

        return speed * Time.deltaTime;
    }

    private float testVert(Vector2 dir) {
        Vector2 origin = transform.position;
        float offset;
        if (dir.Equals(Vector2.up)) {
            offset = .1f; // Offset for top side of character
        } else {
            offset = -0.5f; // Offset for bottom side of character
        }
        origin.y += offset;
        RaycastHit2D raycast = Physics2D.Raycast(origin, dir, speed * Time.deltaTime);

        if (raycast.collider != null && raycast.collider.gameObject.tag == "Collidable") { // If raycast hits something tagged "Collidable"
            float distance = Math.Abs(raycast.point.y - origin.y);
            return distance;
        }

        return speed * Time.deltaTime;
    }
}
     
   
