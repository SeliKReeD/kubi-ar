using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FigureLocator : MonoBehaviour
{
    public float figureCameraDistance;
    public bool isFocusedonBoard = false;
    public LayerMask mask;
    public LayerMask maskForSelectedCube;
    public float pickupSmoothing = 0.3f;

    private Figure figure;
    private Vector3 currentVelocity;
    private Vector3 targetPos;
    private bool needSmoothPos = false;

    private void Start()
    {
        ShapePickerController shapePicker = FindObjectOfType<ShapePickerController>();
        shapePicker.onPickObject += (newFigure) => { figure = newFigure; };
        shapePicker.onDropObject += () => {
            if (needSmoothPos && figure)
            {
                if (figure.transform.position != targetPos)
                {
                    figure.ForceLocate(targetPos, currentVelocity, pickupSmoothing);
                }
            }
            figure = null;
        };
    }

    private void Update()
    {
        if (figure)
        {
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, mask))
            {
                isFocusedonBoard = hit.collider.GetComponent<Border>();
                if (isFocusedonBoard)
                {
                    var startPosition = figure.transform.position;
                    figure.transform.position = hit.collider.GetComponent<Border>().GetPosition(hit.point);
                    targetPos = figure.transform.position + GetOffset();
                    figure.transform.position = Vector3.SmoothDamp(startPosition, targetPos, ref currentVelocity, pickupSmoothing);
                    figureCameraDistance = hit.distance;
                    needSmoothPos = true;
                }
                else
                {
                    Vector3 lastPosition = figure.transform.position;
                    figure.transform.position = hit.point + new Vector3(0, .5f, 0);
                    targetPos = figure.transform.position + GetOffset();
                    figure.transform.position = Vector3.SmoothDamp(lastPosition, targetPos, ref currentVelocity, pickupSmoothing);
                    if ((lastPosition - targetPos).magnitude > figureCameraDistance)
                    {
                        figure.transform.position = GetPositionWithoutPoint();
                    }
                    needSmoothPos = false;
                    
                }
            }
            else
            {
                figure.transform.position = GetPositionWithoutPoint();
                needSmoothPos = false;
            }
        }
    }

    private Vector3 GetPositionWithoutPoint()
    {
        return transform.TransformPoint(Vector3.forward * figureCameraDistance);
    }
    
    private Vector3 GetOffset()
    {
        Vector3 offset = Vector3.zero;
        bool isColision = false;
        float maxDistance = 0;
        foreach (var item in figure.parts)
        {
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(item.transform.position + Vector3.up * 100.5f, Vector3.down, out hit, 100.5f, mask))
            { 
                if (hit.collider.gameObject.layer != (int)Mathf.Log(maskForSelectedCube, 2))
                {
                    if (maxDistance < 101f - hit.distance - 0.5f)
                    {
                        maxDistance = 101f - hit.distance - 0.5f;
                        isColision = true;
                    }
                }
            }
        }
        if (isColision)
        {
            offset += Vector3.up * (maxDistance + .5f);
        }
        return offset;
    }

    public void ForceLocateObject(Figure figure)
    {
        figure.ForceLocate(targetPos, currentVelocity, pickupSmoothing);
    }
}
