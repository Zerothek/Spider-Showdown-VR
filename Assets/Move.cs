using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    float speed = 3;
    float jumpSpeed = 6;
    Rigidbody cameraRigidbody;

    private void Start()
    {
        cameraRigidbody = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.rotation = Camera.main.transform.rotation;

        if (PlayersHealth.health > 0)
        {
            //if (Input.GetKey(KeyCode.S))
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                transform.Translate(new Vector3(0, 0, -speed * Time.deltaTime));
            }

            //if (Input.GetKey(KeyCode.W))
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
            }

            //if (Input.GetKey(KeyCode.D))
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
            }

            //if (Input.GetKey(KeyCode.A))
            if (Input.GetAxisRaw("Vertical") < 0)
            {
                transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
            }

            if (Input.GetButtonDown("Jump"))
            {
                //cameraRigidbody.AddForce(Vector3.up * speed * speed);
                cameraRigidbody.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
                //Debug.Log("I jumped");
            }
        }
    }
}
