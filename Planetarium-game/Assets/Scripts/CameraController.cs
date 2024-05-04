using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float m_sensitivity = 1.5f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            var mx = Input.GetAxis("Mouse X");
            var my = Input.GetAxis("Mouse Y");


            var angles = transform.eulerAngles;

            angles.y += mx;
            angles.x -= my;

            transform.eulerAngles = angles;
        }

        var scroll = Input.GetAxis("Mouse ScrollWheel");

        var scale = transform.localScale;

        scale.z -= scroll;

        transform.localScale = scale;
    }
}
