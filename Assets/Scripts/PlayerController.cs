using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//using Input

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    float horizontalInput;
    float speed = 30.0f;

    private float movementX;
    private float movementY;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //disable Y movement, maybe?
        //Vector3 movement = new Vector3(movementX, movementY, 0.0f);
        rb.velocity = Vector2.right * movementX * speed;
    }
}
