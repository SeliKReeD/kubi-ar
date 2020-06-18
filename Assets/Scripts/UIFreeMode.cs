using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFreeMode : MonoBehaviour
{
    public GameControllerBase gameController;
    public GameObject mainUI;
    public GameObject aim;
    public Button placeObject;
    public Button restartOnMain;
    public GameObject frame;

    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameControllerBase>();
        gameController.trackingHandler.OnTrackFound += OnTrackFound;
        gameController.trackingHandler.OnTrackLost += OnTrackLost;


        placeObject.onClick.AddListener(PlaceObject);
        restartOnMain.onClick.AddListener(RestartGame);
    }
    
    public void ResetUI()
    {
        mainUI.gameObject.SetActive(false);
        frame.gameObject.SetActive(false);
    }

    public void ShowMainUI()
    {
        ResetUI();
        mainUI.gameObject.SetActive(true);
        aim.gameObject.SetActive(false);
    }

    public void OnItemPicked()
    {
        aim.gameObject.SetActive(true);
        placeObject.gameObject.SetActive(true);
    }

    public void OnItemDropped()
    {
        aim.gameObject.SetActive(false);
        placeObject.gameObject.SetActive(false);
    }

    public void ShowFrame()
    {
        ResetUI();
        frame.gameObject.SetActive(true);
    }

    public void OnTrackFound()
    {
        ShowMainUI();
    }

    public void OnTrackLost()
    {
        ShowFrame();
    }


    public void PlaceObject()
    {
        gameController.shapePickerController.DropObject();
    }


    public void RestartGame()
    {
        gameController.RestartGame();
    }
}