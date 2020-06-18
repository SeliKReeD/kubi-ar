using UnityEngine;
using System.Collections;

public class FigureColorer : MonoBehaviour
{
    public Color color;
    // Use this for initialization
    void Start()
    {
        var mrs = GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer mr in mrs)
        {
            mr.material.SetColor("_CubeColor", color);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
