using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroller : MonoBehaviour
{
    public float horizontalInput;
    public float verticalInput;
    public float speed = 500.0f;
    public float turnspeed = 100.0f;
    public float rotX;
    public float rotY;
    public float rotZ;
    // Update is called once per frame
    void FixedUpdate()
    {
        float translation = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        //float rotation = Input.GetAxis("Horizontal") * turnspeed * Time.deltaTime;

        transform.Translate(0, 0, translation);
        //transform.Rotate(0, rotation, 0);
    }

    void Update()
    {
        rotX -= Input.GetAxis("Mouse Y") * Time.deltaTime * turnspeed;
        rotY += Input.GetAxis("Mouse X") * Time.deltaTime * turnspeed;

        if (rotX < -90)
        {
            rotX = -90;
        }
        else if (rotX > 90)
        {
            rotX = 90;
        }



        transform.rotation = Quaternion.Euler(0, rotY, 0);
        GameObject.FindWithTag("MainCamera").transform.rotation = Quaternion.Euler(rotX, rotY, 0);


    }

}
