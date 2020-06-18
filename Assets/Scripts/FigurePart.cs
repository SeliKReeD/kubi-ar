using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FigurePart : Border
{
    public Figure Figure
    {
        get;
        private set;
    }

    public LayerMask collisionDetection;


    private void Start()
    {
        Figure = GetComponentInParent<Figure>();
        Figure.parts.Add(this);
    }

    public override Vector3 GetPosition(Vector3 raycastPoint)
    {
        return new Vector3(Mathf.RoundToInt(raycastPoint.x), + (transform.position + Vector3.up).y, Mathf.RoundToInt(raycastPoint.z));
    }

}
