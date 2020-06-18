using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public List<Figure> parts;
    public Transform grid;

    public void SetKinematic(bool value)
    {
        foreach(var item in parts)
        {
            item.rg.isKinematic = value || item.InTower;
        }
    }
}
