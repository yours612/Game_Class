using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rank : MonoBehaviour
{
    private GameObject gameRank;

    public Text[] ranks = new Text[5];

    void Start()
    {
        gameRank = GameObject.Find("GameRank");
        DontDestroyOnLoad(gameRank);
        SetScore();
    }

    public void SetScore()
    {
        ranks[0].text = PlayerPrefs.GetInt("No.1").ToString();
        ranks[1].text = PlayerPrefs.GetInt("No.2").ToString();
        ranks[2].text = PlayerPrefs.GetInt("No.3").ToString();
        ranks[3].text = PlayerPrefs.GetInt("No.4").ToString();
        ranks[4].text = PlayerPrefs.GetInt("No.5").ToString();

    }
}
