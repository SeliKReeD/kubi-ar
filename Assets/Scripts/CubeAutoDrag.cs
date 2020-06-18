using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CubeAutoDrag : MonoBehaviour
{
    public float dragRadius = 10f;
    public float dragForce = 10f;

    Rigidbody body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.magnitude > dragRadius)
            body.AddForce(-transform.position.normalized * dragForce * Time.fixedDeltaTime * transform.localScale.x);
    }
}
