using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundParallax : MonoBehaviour
{
    public Transform[] backgrounds;
    //背景随相机移动的比例
    public float parallaxScale = 0.5f;
    //层间运动比例
    public float layerScale = 0.5f;
    public float smooth = 5f;

    private Transform CamTransform;
    private Vector3 previousCamPos;

    private void Awake()
    {
        CamTransform = Camera.main.transform; //获取摄像机位置

    }
    // Start is called before the first frame update
    void Start()
    {
        previousCamPos = CamTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float parallax = (previousCamPos.x - CamTransform.position.x) * parallaxScale;
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float targetX = backgrounds[i].position.x + parallax * (1 + i * layerScale);
            Vector3 targetPos = new Vector3(targetX, backgrounds[i].position.y, backgrounds[i].position.z);

            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, targetPos, smooth * Time.deltaTime);
        }
        previousCamPos = CamTransform.position;
    }
}
