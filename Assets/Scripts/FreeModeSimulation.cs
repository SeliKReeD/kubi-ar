using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeModeSimulation : MonoBehaviour
{
    public List<Figure> figures;

    private void Start()
    {
        FindObjectOfType<GameControllerBase>().onStartLevel += StartSimulation;
    }

    public void StartSimulation()
    {
        FindObjectOfType<GameControllerBase>().trackingHandler.OnTrackFound += () => { SetKinematic(false); };
        FindObjectOfType<GameControllerBase>().trackingHandler.OnTrackLost += () => { SetKinematic(true); };
    }

    public void SetKinematic(bool kinematic)
    {
        foreach (var item in figures)
        {
            item.gameObject.SetActive(true);
            item.GetComponent<Rigidbody>().isKinematic = kinematic;
        }
    }
}
