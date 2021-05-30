using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Vehicles.Ball;

public class ScoreText : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    Ball ballScript;

    // Start is called before the first frame update
    void Start()
    {
        ballScript = player.GetComponent<Ball>();

    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = ballScript.score.ToString();
    }
}
