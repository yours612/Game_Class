using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destoryCircle : MonoBehaviour
{
    public float delayTime;
 
    
        // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BombDetonation());
    }


    IEnumerator BombDetonation()
    {
        // 等待两秒，用于播放引信燃烧效果
        yield return new WaitForSeconds(delayTime);

        destroyGameObject();
    }
    void destroyGameObject()
    {
       Destroy(gameObject);
    }


}
