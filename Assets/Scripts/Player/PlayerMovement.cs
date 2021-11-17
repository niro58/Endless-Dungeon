﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{

    private float moveThrust;
    private Rigidbody2D rb;
    private Animator anim;

    private Vector2 movement;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        moveThrust = GlobalVar.sumStats.Speed;
        // Movement part
        movement = new Vector2(Input.GetAxisRaw("Horizontal") * moveThrust, Input.GetAxisRaw("Vertical") * moveThrust);
        rb.velocity = movement; 
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            anim.SetBool("Moving", true);
        }
        else
        {
            anim.SetBool("Moving",false);
        }
    }
 



}
