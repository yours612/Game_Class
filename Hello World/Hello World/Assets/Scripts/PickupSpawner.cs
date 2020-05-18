﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    public GameObject[] pickups;                // 道具数组：炸弹、医疗包
    public float pickupDeliveryTime = 5f;       // 间隔时间
    public float dropRangeLeft = -15;           // 最左侧
    public float dropRangeRight = 15;           // 最右侧
    public float highHealthThreshold = 75f;     // 血量大于多少时只产生炸弹
    public float lowHealthThreshold = 25f;      // 血量低于多少时只产生医疗包

    private LayBombs layBombs;
    private PlayerHealth playerHealth;          // 脚本


    void Awake()
    {
        // 获取脚本
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        layBombs = GameObject.FindGameObjectWithTag("Player").GetComponent<LayBombs>();
    }


    void Start()
    {
        // 启动第一次道具产生
        if (layBombs.bombCount < 3)
        {
            for (int i = layBombs.bombCount; i < 2; i++)
            {              
                StartCoroutine(DeliverPickup());
            }
        }
    }


    public IEnumerator DeliverPickup()
    {
        // 第一次时间间隔
        yield return new WaitForSeconds(pickupDeliveryTime);

        // 在最左和最右之间产生随机x值
        float dropPosX = Random.Range(dropRangeLeft, dropRangeRight);
        Vector3 dropPos = new Vector3(dropPosX, 15f, 1f);

        // 只产生炸弹
        if (playerHealth.health >= highHealthThreshold)
            Instantiate(pickups[0], dropPos, Quaternion.identity);
        else if (playerHealth.health <= lowHealthThreshold)
            // 只产生医疗包
            Instantiate(pickups[1], dropPos, Quaternion.identity);
        else
        {
            // 随机产生炸弹或医疗包
            int pickupIndex = Random.Range(0, pickups.Length);
            Instantiate(pickups[pickupIndex], dropPos, Quaternion.identity);
        }
    }
}
