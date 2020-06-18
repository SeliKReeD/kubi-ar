using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridHL : MonoBehaviour
{
    [ColorUsageAttribute(true, true)] public Color HLColor;

    private Material mat;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
        mat.SetColor("_GridColor", Color.clear);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        mat.SetColor("_GridColor", HLColor);
    }

    private void OnTriggerExit(Collider other)
    {
        mat.SetColor("_GridColor", Color.clear);
    }


}
