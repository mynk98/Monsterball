using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Vehicles.Ball;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    Health healthScript;
    Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        healthScript = player.GetComponent<Health>();
        slider = GetComponent<Slider>();
        slider.maxValue = 100;
        slider.value = 100;
    }

    // Update is called once per frame
    void Update()
    {
        float health = healthScript.healthPoints;
        slider.value = health;
    }
}
