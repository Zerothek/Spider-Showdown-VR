using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionSet : MonoBehaviour
{
    public GameObject cameraHelper;
    // Start is called before the first frame update
    void Start()
    {
        cameraHelper = GameObject.Find("CameraHelper");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = cameraHelper.transform.position;
    }
}
