using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

    public float acceleration = 100f;
    public float maxSpeed = 50f;
    public float decaleration;
    Rigidbody2D rigidBody;
    


    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
      
        Move();
    }

   

    void Move()
    {
        //X Velocity

        rigidBody.velocity += Vector2.right * (Input.GetAxis("Horizontal") * acceleration) * Time.deltaTime;
        rigidBody.velocity += Vector2.up * (Input.GetAxis("Vertical") * acceleration) * Time.deltaTime;

        //Clamp Max Speed
        if (rigidBody.velocity.magnitude >= maxSpeed)
        {
            rigidBody.velocity = Vector2.ClampMagnitude(rigidBody.velocity, maxSpeed);
        }


        //acceleration decrease 
        if (!Input.GetButton("Horizontal") && rigidBody.velocity.y >= 0f && rigidBody.velocity.x > 0f)
        {
            rigidBody.velocity = rigidBody.velocity * Vector2.right * decaleration * Time.deltaTime;
        }
        else if (!Input.GetButton("Horizontal") && rigidBody.velocity.y >= 0f && rigidBody.velocity.x < 0f)
        {
            rigidBody.velocity = rigidBody.velocity * Vector2.left * -decaleration * Time.deltaTime;
        }

        if (!Input.GetButton("Vertical") && rigidBody.velocity.x >= 0f && rigidBody.velocity.y > 0f)
        {
            rigidBody.velocity = rigidBody.velocity * Vector2.up * decaleration * Time.deltaTime;
        }
        else if (!Input.GetButton("Vertical") && rigidBody.velocity.x >= 0f && rigidBody.velocity.y < 0f)
        {
            rigidBody.velocity = rigidBody.velocity * Vector2.down * -decaleration * Time.deltaTime;
        }



    }
}
