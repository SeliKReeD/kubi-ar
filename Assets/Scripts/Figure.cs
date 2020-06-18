using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Figure : MonoBehaviour
{
    public List<FigurePart> parts;
    private Coroutine forceLocatorCoroutine;
    private Coroutine forceRotatorCoroutine;

    public bool InTower = false;
    public Rigidbody rg;

    private void Awake()
    {
        parts = new List<FigurePart>();
        rg = GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {
        FindObjectOfType<ShapePickerController>().OnObjectClick(this);
    }

    public void OnObjectSelect()
    {
        if (forceLocatorCoroutine != null)
        {
            StopCoroutine(forceLocatorCoroutine);
        }
        if (forceRotatorCoroutine != null)
        {
            StopCoroutine(forceRotatorCoroutine);
        }
    }

    public void ForceLocate(Vector3 targetPosition, Vector3 velocity, float smoothing)
    {
        if (forceLocatorCoroutine != null)
        {
            StopCoroutine(forceLocatorCoroutine);
        }
        forceLocatorCoroutine = StartCoroutine(ForceLocateCoroutine(targetPosition, velocity, smoothing));
    }

    private IEnumerator ForceLocateCoroutine(Vector3 targetPosition, Vector3 velocity, float smoothing)
    {
        while (transform.position != targetPosition)
        {
            yield return null;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothing);
        }
    }

    public void ForceRotate(Vector3 targetRotation, Vector3 velocity, float smoothing)
    {
        if (forceRotatorCoroutine != null)
        {
            StopCoroutine(forceRotatorCoroutine);
        }
        forceRotatorCoroutine = StartCoroutine(ForceRotateCoroutine(targetRotation, velocity, smoothing));
    }

    private IEnumerator ForceRotateCoroutine(Vector3 targetRotation, Vector3 velocity, float smoothing)
    {
        Quaternion startRotationQ = transform.localRotation;
        Quaternion targetRotationQ = Quaternion.Euler(targetRotation);
        float timePassed = 0f;

        while (timePassed < 1f)
        {
            yield return null;
            transform.localRotation = Quaternion.Lerp(startRotationQ, targetRotationQ, Mathf.Sin(timePassed * Mathf.PI / 2f));
            timePassed += Time.deltaTime / smoothing;
        }
        transform.localRotation = targetRotationQ;
        yield break;
    }
}
