using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayBombs : MonoBehaviour
{
    public GameObject bomb;
    public int bombCount = 0;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2") && bombCount > 0)
        {
            bombCount--;
            Instantiate(bomb, transform.position, transform.rotation);
        }
    }
}
