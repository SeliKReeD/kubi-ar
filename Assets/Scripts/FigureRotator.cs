using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureRotator : MonoBehaviour
{
    [SerializeField] float autoRotateSmoothing = 0.5f;
    [SerializeField] float rotationSensative = 1f;

    private Figure figure;
    private Vector3 currentAngular;
    private Vector3 targetAngular;
    private Vector3 currentVelocity;

    private FigureLocator locator;

    private void Start()
    {
        ShapePickerController shapePicker = FindObjectOfType<ShapePickerController>();
        locator = FindObjectOfType<FigureLocator>();
        shapePicker.onPickObject += (newFigure) => { figure = newFigure; };
        shapePicker.onDropObject += () => {
            if (locator.isFocusedonBoard)
            {
                figure.ForceRotate(targetAngular, currentVelocity, autoRotateSmoothing);
            }
            figure = null;
        };
    }

    private void Update()
    {
        if (!figure) return;
        if (Input.touchCount > 0)
        {
            figure.Rotate(Input.GetTouch(0).deltaPosition * rotationSensative);

            currentAngular = Vector3.zero;
        }
        else if (Input.GetMouseButton(0))
        {
            float h = Input.GetAxis("Mouse X");
            float v = Input.GetAxis("Mouse Y");
            figure.Rotate(new Vector2(-h, v) * 5f);

            currentAngular = Vector3.zero;
        }
        else
        {
            StraitRotation();
        }
    }

    private void StraitRotation()
    {
        targetAngular.x = Mathf.Round(figure.transform.localEulerAngles.x / 90f) * 90f;
        targetAngular.y = Mathf.Round(figure.transform.localEulerAngles.y / 90f) * 90f;
        targetAngular.z = Mathf.Round(figure.transform.localEulerAngles.z / 90f) * 90f;
        Vector3 eulers;
        eulers.x = Mathf.SmoothDamp(figure.transform.localEulerAngles.x, targetAngular.x, ref currentAngular.x, autoRotateSmoothing);
        eulers.y = Mathf.SmoothDamp(figure.transform.localEulerAngles.y, targetAngular.y, ref currentAngular.y, autoRotateSmoothing);
        eulers.z = Mathf.SmoothDamp(figure.transform.localEulerAngles.z, targetAngular.z, ref currentAngular.z, autoRotateSmoothing);
        figure.transform.localEulerAngles = eulers;
    }
}

public static class FigureRotatorExtend
{
    private static Transform camera;
    private static Transform Camera
    {
        get
        {
            if (!camera)
            {
                if (Object.FindObjectOfType<KeyboardCameraControler>())
                {
                    camera = Object.FindObjectOfType<KeyboardCameraControler>().transform;
                }
                else
                {
                    camera = Object.FindObjectOfType<Camera>().transform;
                }
            }
            return camera;
        }
    }

    public static void Rotate(this Figure figure, Vector2 value)
    {
        figure.transform.Rotate(Camera.TransformDirection(Vector3.right), value.y, Space.World);
        figure.transform.Rotate(Camera.TransformDirection(Vector3.down), value.x, Space.World);
    }
}
