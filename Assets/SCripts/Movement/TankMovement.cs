using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{

 

    //speed of tank
    public float tankSpeed = 10f;
    //player rigid body
    Rigidbody2D tankRB;
    Vector3 movement;
    Vector3 target;
    float movementSmoothing = 10f;
    Quaternion previousRotation;
  
    // Start is called before the first frame update
    void Awake()
    {
       
        tankRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        move();
    }

    void move()
    {
   
        
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        movement.Set(x, y, 0f);
        movement = movement.normalized * tankSpeed;

        target = transform.position + movement;
        target.x = Mathf.Clamp(target.x, -75 * 2, 75 * 2);
        target.y = Mathf.Clamp(target.y, -45 * 2, 45 * 2);

        transform.position = Vector3.Lerp(transform.position, target, 5 * Time.deltaTime);

        tankRB.MovePosition(transform.position + movement);
        //rotating player into direction it's looking at
        if (movement.x != 0 || movement.y != 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.forward, movement), movementSmoothing * Time.fixedDeltaTime);
            previousRotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.forward, movement), movementSmoothing * Time.fixedDeltaTime);
        }
        else
        {
            transform.rotation = previousRotation;
        }
    }

}
