using UnityEngine;
using System.Collections;

public class Sample : MonoBehaviour
{
    public static bool isShown = false;

    public MeshRenderer[] mrs;

    float time;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isShown)
        {
            if (time < 0f) time = 0f;
            time += Time.deltaTime;
        }
        else
        {
            if (time > 2f) time = 2f;
            time -= Time.deltaTime;
        }
        if (time > 0f)
        {
            for (var i = 0; i < mrs.Length; i++)
            {
                mrs[i].material.SetFloat("_PlayTime", time);
            }
        }
    }
}
