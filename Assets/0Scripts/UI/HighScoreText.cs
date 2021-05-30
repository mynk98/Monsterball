using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Ball;
using UnityEngine.UI;


public class HighScoreText : MonoBehaviour
{
    int highScore;
    
    
    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt("highScore", 0);

    }

    // Update is called once per frame
    void Update()
    {
        int sscore = GameObject.FindGameObjectWithTag("Player").GetComponent<Ball>().score;
        if (highScore < sscore)
        {
            highScore = sscore;
            PlayerPrefs.SetInt("highScore", highScore);
            PlayerPrefs.Save();
        }
        GetComponent<Text>().text = "High Score: " + highScore;
    }
}
