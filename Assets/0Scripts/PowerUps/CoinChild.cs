using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Ball;
public class CoinChild : MonoBehaviour
{
    
    GameObject rollerBall;
    Ball ball;
    [SerializeField]
    GameObject coinPowerup;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rollerBall = GameObject.FindWithTag("Player");
        ball = rollerBall.GetComponent<Ball>();
        animator = coinPowerup.GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject==rollerBall)
        {
            ball.IncreaseScore(10);
            animator.SetBool("isCollecting", true);
            Destroy(gameObject);
        }
    }



    
}
