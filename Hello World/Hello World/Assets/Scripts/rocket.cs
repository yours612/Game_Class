﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket : MonoBehaviour
{
    public GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2);
    }
    void OnExplode()
    {
        Quaternion randRotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
        Instantiate(explosion, transform.position, randRotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnExplode();
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
