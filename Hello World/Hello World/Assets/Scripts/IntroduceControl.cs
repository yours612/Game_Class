using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroduceControl : MonoBehaviour
{
    private bool isActive = true;
    public void Button_IntroduceFc()
    {
        gameObject.SetActive(isActive);
        isActive = !isActive;
    }

}
