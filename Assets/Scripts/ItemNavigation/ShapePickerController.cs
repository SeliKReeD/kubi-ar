using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapePickerController : MonoBehaviour
{
    FigureLocator locator;
    private Figure currentShape;
    public Figure CurrentShape
    {
        get
        {
            return currentShape;
        }
        set
        {
            if (currentShape)
            {
                Array.ForEach(currentShape.GetComponentsInChildren<Collider>(), (Collider collider) => {
                    collider.gameObject.layer = (int)Mathf.Log(locator.isFocusedonBoard ? maskActiveCube : maskDefaultCube, 2);
                });
                currentShape.GetComponent<Rigidbody>().useGravity = !locator.isFocusedonBoard;
                currentShape.GetComponent<Rigidbody>().isKinematic = locator.isFocusedonBoard;
                onDropObject?.Invoke();
            }
            currentShape = value;
            if(currentShape)
            {
                Array.ForEach(currentShape.GetComponentsInChildren<Collider>(), (Collider collider) => { collider.gameObject.layer = (int)Mathf.Log(maskSelectedCube, 2); });

                currentShape.GetComponent<Rigidbody>().useGravity = false;
                currentShape.GetComponent<Rigidbody>().isKinematic = true;
                onPickObject?.Invoke(currentShape);
            }
        }
    }

    public LayerMask maskDefaultCube;
    public LayerMask maskActiveCube;
    public LayerMask maskSelectedCube;

    public Action<Figure> onPickObject;
    public Action onDropObject;

    private bool isObjectPickable = false;

    private void Start()
    {
        locator = FindObjectOfType<FigureLocator>();
        FindObjectOfType<GameControllerBase>().onStartLevel += () => { isObjectPickable = true; };
    }

    public void OnObjectClick(Figure figure)
    {
        if (CurrentShape)
        {
            DropObject();
            PickObject(figure);
        }
        else
        {
            PickObject(figure);
        }
    }

    public void PickObject(Figure obj)
    {
        if (isObjectPickable)
        {
            obj.OnObjectSelect();
            Debug.Log("Pick object");
            CurrentShape = obj;
        }
    }

    public void DropObject()
    {
        Debug.Log("Drop object");
        CurrentShape.InTower = locator.isFocusedonBoard;
        CurrentShape = null;
    }
}
