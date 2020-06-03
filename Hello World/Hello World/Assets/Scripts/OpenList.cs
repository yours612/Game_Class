using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenList : MonoBehaviour
{
    private bool isActive = true;

    public void ListFc()
    {
        gameObject.SetActive(isActive);
        isActive = !isActive;
    }
}
