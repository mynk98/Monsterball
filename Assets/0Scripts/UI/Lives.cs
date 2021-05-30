using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lives : MonoBehaviour
{
    [SerializeField]
    GameObject life1;
    [SerializeField]
    GameObject life2;
    [SerializeField]
    GameObject life3;
    [SerializeField]
    GameObject life4;
    [SerializeField]
    GameObject life5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int lives=GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().numberOfLives;
        if (lives == 4)
        {
            life4.SetActive(true);
            life5.SetActive(false);
        }

        else if (lives == 5)
        {
            life5.SetActive(true);
        }

        else if (lives == 3)
        {
            life4.SetActive(false);
            life3.SetActive(true);
        }
        else if (lives == 2)
        {
            life2.SetActive(true);
            life3.SetActive(false);
        }
        else if (lives == 1)
        {
            life1.SetActive(true);
            life2.SetActive(false);
        }
        else if (lives == 0)
        {
            life1.SetActive(false);
        }
    }
}
