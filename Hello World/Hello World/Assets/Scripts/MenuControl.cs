using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuControl : MonoBehaviour
{
    private float isTimeStop = 0;

    public void OnPauseFc()
    {
        Time.timeScale = isTimeStop;
        //isTimeStop == 0 ? isTimeStop = 1 : isTimeStop = 0;
        if (isTimeStop == 0)
            isTimeStop = 1;
        else
            isTimeStop = 0;
    }

    public void OnRestartFc()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        Time.timeScale = 1f;


        /*
        PlayerPrefs.SetInt("No.5", 0);
        PlayerPrefs.SetInt("No.4", 0);
        PlayerPrefs.SetInt("No.3", 0);
        PlayerPrefs.SetInt("No.2", 0);
        PlayerPrefs.SetInt("No.1", 0);
    */


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
        }

        Score.x = 0;
    }

}
