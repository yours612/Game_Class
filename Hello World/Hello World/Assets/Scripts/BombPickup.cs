﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPickup : MonoBehaviour
{
    private Animator anim;             
    private bool landed = false;       


    void Awake()
    {

        anim = transform.root.GetComponent<Animator>();
    }


    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            other.GetComponent<LayBombs>().bombCount++;

            Destroy(transform.root.gameObject);
        }
   
        else if (other.tag == "Ground" && !landed)
        {
       
            anim.SetTrigger("Land");
            transform.parent = null;
            gameObject.AddComponent<Rigidbody2D>();
            landed = true;
        }
    }
}
