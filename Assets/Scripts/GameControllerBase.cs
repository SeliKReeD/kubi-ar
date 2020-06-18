using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum GameMode { CHALENGE, FREE };

public class GameControllerBase : MonoBehaviour
{

    public GameMode gameMode;
    public DefaultTrackableEventHandler trackingHandler;
    public ShapePickerController shapePickerController;

    public bool isGameStarted = false;
    public System.Action onStartLevel;

    private void Awake()
    {
        trackingHandler = FindObjectOfType<DefaultTrackableEventHandler>();

    }

    public virtual void Start()
    {
        shapePickerController = FindObjectOfType<ShapePickerController>();
        if (gameMode == GameMode.FREE)
        {
            StartCoroutine(StartLevel());
        }
    }

    private IEnumerator StartLevel()
    {
        yield return null;

        onStartLevel?.Invoke();
        isGameStarted = true;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

}
