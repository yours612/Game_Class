using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenMenu : MonoBehaviour
{
    private bool isActive = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void MenuButtonFc()
    {
        gameObject.SetActive(isActive);
        isActive = !isActive;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
