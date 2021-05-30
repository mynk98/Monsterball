using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Ball;
public class CoinPickup : MonoBehaviour
{
    AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
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
