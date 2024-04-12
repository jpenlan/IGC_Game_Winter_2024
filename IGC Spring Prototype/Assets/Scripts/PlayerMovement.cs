using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    //The tutorial I used for player & camera movement: https://www.youtube.com/watch?v=f473C43s8nE

    //  movement thingz
    public float moveSpeed;
    public Transform orientation;
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    public Rigidbody playerBody;

    void Start()
    {
        if(playerBody == null)
        {
            playerBody = GetComponent<Rigidbody>();
        }
        playerBody.freezeRotation = true;

        //cursor is locked in the center of screen and invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }



    void Update()
    {
        MyInput();
        SpeedControl();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    //player movement
    //  player input
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    //  move player
    private void MovePlayer()
    {
        //calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //i like to move it move it
        playerBody.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

    }

    //  control player speed to prevent diagonal skating being faster
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(playerBody.velocity.x, 0f, playerBody.velocity.z);

        //limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed; //calculate max velocity
            playerBody.velocity = new Vector3(limitedVel.x, playerBody.velocity.y, limitedVel.z); //apply it
        }
    }

}
