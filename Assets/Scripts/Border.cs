using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    public Collider Collider
    {
        get;
        private set;
    }

    // Start is called before the first frame update
    void Start()
    {
        Collider = GetComponent<Collider>();
    }


    public virtual Vector3 GetPosition(Vector3 raycastPoint)
    {
        return new Vector3(Mathf.RoundToInt(raycastPoint.x), raycastPoint.y +( Vector3.up / 2).y, Mathf.RoundToInt(raycastPoint.z));
    }

}
