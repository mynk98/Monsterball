using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Ball;
public class HealthPickup : MonoBehaviour
{
    
    [SerializeField]
    float healAmount = 40;
    [SerializeField]
    AudioSource source;
    Animator animator;
    bool picking;
    // Start is called before the first frame update
    void Start()
    {
        
        source = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        picking = true;
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            if (picking)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().ApplyHeal(healAmount);
                animator.SetBool("isCollecting", true);
                picking = false;
            }
        }
    }

    void Die()
    {
        Destroy(gameObject);
        picking = true;
    }

    void PlayAudio()
    {
        source.Play();
    }
}
