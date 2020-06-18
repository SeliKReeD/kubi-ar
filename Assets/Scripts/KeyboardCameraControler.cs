using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardCameraControler : MonoBehaviour
{
    public float moveSens = 10f;
    public float rotSens = 60f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = Vector3.zero;
        Vector3 rot = Vector3.zero;

        if (!Input.GetKey(KeyCode.LeftShift))
        {
            dir += Input.GetAxis("Horizontal") * transform.right;
            dir += Input.GetAxis("Vertical") * transform.forward;
            if (Input.GetKey(KeyCode.Q)) dir += Vector3.down;
            if (Input.GetKey(KeyCode.E)) dir += Vector3.up;
        }
        else
        {
            rot += Input.GetAxis("Horizontal") * Vector3.down;
            rot += Input.GetAxis("Vertical") * Vector3.right;
        }

        transform.position += dir * moveSens * Time.deltaTime;
        transform.eulerAngles += rot * rotSens * Time.deltaTime;
    }
}
