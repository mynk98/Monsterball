using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Ball;
public class KillerBallPickup : MonoBehaviour
{
    GameObject rollerBall;
    Ball ball;
    AudioSource source;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rollerBall = GameObject.FindWithTag("Player");
        ball = rollerBall.GetComponent<Ball>();
        source = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject == rollerBall)
        {
            ball.SetKillerBall();
            animator.SetBool("isCollecting", true);
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void PlayAudio()
    {
        source.Play();
    }
}
