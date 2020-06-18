using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SampleShower : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Sample.isShown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Sample.isShown = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Sample.isShown = false;
    }
}
