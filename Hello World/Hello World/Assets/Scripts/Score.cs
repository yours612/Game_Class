using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static Text score;
    public static int x = 0;
    void Start()
    {
        score = GetComponent<Text>();
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
