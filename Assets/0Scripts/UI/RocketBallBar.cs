using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Vehicles.Ball;

public class RocketBallBar : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    Ball ballScript;
    Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        ballScript = player.GetComponent<Ball>();
        slider = GetComponent<Slider>();
        slider.maxValue = ballScript.rocketBallTime;
        slider.value = slider.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        float timeLeft = ballScript.rocketBallTime-ballScript.currentRocketBallTime;
        slider.value = timeLeft;
    }
}
