using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Ball;

public class Water : MonoBehaviour
{
    float originalJumpPower;
    AudioSource source;
    
    // Start is called before the first frame update
    void Start()
    {
        originalJumpPower = GameObject.FindGameObjectWithTag("Player").GetComponent<Ball>().m_JumpPower;
        source = GetComponent<AudioSource>();
    }


    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().drag = 3;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().angularDrag = 3;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Ball>().m_JumpPower = 1;
            source.Play();
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().drag = 0.1f;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().angularDrag = 1;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Ball>().m_JumpPower = originalJumpPower;
            source.Stop();

        }
    }
    
}
