﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BombPickup : MonoBehaviour
{
    private Animator anim;             
    private bool landed = false;

    public AudioClip bombsPickup;
    public AudioMixer mixer;


    void Awake()
    {

        anim = transform.root.GetComponent<Animator>();
    }


    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(bombsPickup, transform.position);
            mixer.SetFloat("props", 0);

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
