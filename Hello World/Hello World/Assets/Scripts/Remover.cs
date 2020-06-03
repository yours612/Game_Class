using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Remover : MonoBehaviour
{
    public GameObject splash;


    private void reStart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        Time.timeScale = 1f;
        Score.x = 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>().enabled = false;
            Instantiate(splash, collision.transform.position, transform.rotation);
            Destroy(collision.gameObject);

            if (Score.x > PlayerPrefs.GetInt("No.5"))
            {
                PlayerPrefs.SetInt("No.5", Score.x);

                for (int i = 1; i <= 5; i++)
                {
                    for (int j = 1; j <= 5 - i; j++)
                    {
                        String rank0 = "No." + j.ToString();
                        String rank1 = "No." + (j + 1).ToString();

                        if (PlayerPrefs.GetInt(rank0) < PlayerPrefs.GetInt(rank1))
                        {
                            int temp = PlayerPrefs.GetInt(rank0);
                            PlayerPrefs.SetInt(rank0, PlayerPrefs.GetInt(rank1));
                            PlayerPrefs.SetInt(rank1, temp);
                        }
                    }
                }


                Invoke("reStart", 1f);
            }
            else
            {
                Instantiate(splash, collision.transform.position, transform.rotation);
                Destroy(collision.gameObject);
            }
        }
    }
}
