using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaler : MonoBehaviour
{
    private Transform reparentObject;
    private Transform ReparentObject
    {
        get
        {
            if (!reparentObject)
            {
                reparentObject = new GameObject("ReparentObject").transform;
            }
            return reparentObject;
        }
    }
    public float scaleSpeed = 1f;

    private float scaleValue;

    private void Start()
    {
        scaleValue = transform.localScale.x;
        FindObjectOfType<GameControler>().onStartLevel += () => { Destroy(ReparentObject.gameObject); enabled = false; };
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 2)
        {
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            Scale(deltaMagnitudeDiff);
            Debug.Log(deltaMagnitudeDiff);
        }
        if (Input.GetKey(KeyCode.T))
        {
            Scale(.01f);
        }
        if (Input.GetKey(KeyCode.Y))
        {
            Scale(-.01f);
        }
    }

    private void Scale(float value)
    {
        foreach (Transform item in transform)
        {
            item.SetParent(ReparentObject);
        }
        scaleValue = Mathf.Clamp(scaleValue + value * scaleSpeed, 0.1f, 10f);
        transform.localScale = Vector3.one * scaleValue;
        Debug.Log(scaleValue);
        foreach (Transform item in ReparentObject)
        {
            item.SetParent(transform);
        }
    }
}
