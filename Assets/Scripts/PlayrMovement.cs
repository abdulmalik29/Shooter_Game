﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayrMovement : MonoBehaviour
{
    public float startMoveSpeed = 5f;

    public Rigidbody2D rb;
    public Camera cam;

    public static Vector2 Position;
    public static float playerAngle;

    private Vector2 movement;
    private Vector2 mousePos;

    private float movementSpeed;


    // Update is called once per frame
    void Update()
    {
        // gets the inputs from the game
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);


        transform.localScale = Vector3.one * Progression.Growth;
        movementSpeed = startMoveSpeed * Progression.Growth;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime);


        // rotate the player
        Vector2 lookDirection = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;

        Position = rb.position;
        playerAngle = angle;
    }

}
