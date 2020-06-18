using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameControler gameController;

    public GameObject mainUI;
    public GameObject aim;
    public Button startChalengeMode;
    public Button left;
    public Button right;
    public Button startFreeMode;
    public Button showOutwire;
    public Button placeObject;
    public Button restartOnMain;
    public Text timer;
    public GameObject scorePanel;
    public Text score;
    public Button restartOnScore;
    public Button exit;
    public GameObject frame;

    private void Start()
    {
        gameController = FindObjectOfType<GameControler>();
        gameController.trackingHandler.OnTrackFound += OnTrackFound;
        gameController.trackingHandler.OnTrackLost += OnTrackLost;
        SetActionsForButtons();
    }

    public void SetActionsForButtons()
    {   
        startChalengeMode.onClick.AddListener(StartChalengeMode);
        startFreeMode.onClick.AddListener(StartFreeMode);
        placeObject.onClick.AddListener(PlaceObject);
        restartOnScore.onClick.AddListener(RestartGame);
        restartOnMain.onClick.AddListener(RestartGame);
        exit.onClick.AddListener(ExitGame);

        left.onClick.AddListener(() => {
            gameController.IndexForDrop = gameController.IndexForDrop - 1;
        });
        right.onClick.AddListener(() => {
            gameController.IndexForDrop = gameController.IndexForDrop + 1;
        });
    }

    public void StartGame()
    {
        gameController.StartLevel();
        startChalengeMode.gameObject.SetActive(false);
        startFreeMode.gameObject.SetActive(false);
        restartOnMain.gameObject.SetActive(true);
    }

    public void StartFreeMode()
    {
        gameController.gameMode = GameMode.FREE;
        StartGame();
    }

    public void StartChalengeMode()
    {
        gameController.gameMode = GameMode.CHALENGE;
        StartGame();
    }

    public void PlaceObject()
    {
        gameController.shapePickerController.DropObject();
    }

    public void RestartGame()
    {
        gameController.RestartGame();
    }

    public void ExitGame()
    {
        gameController.ExitGame();
    }

    public void OnTrackFound()
    {
        ShowMainUI();
    }

    public void OnTrackLost()
    {
        ShowFrame();
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

    public void ResetUI()
    {
        mainUI.gameObject.SetActive(false);
        scorePanel.gameObject.SetActive(false);
        frame.gameObject.SetActive(false);
    }

    public void ShowMainUI()
    {
        ResetUI();
        mainUI.gameObject.SetActive(true);
        startChalengeMode.gameObject.SetActive(!gameController.isGameStarted);
        startFreeMode.gameObject.SetActive(!gameController.isGameStarted);
        placeObject.gameObject.SetActive(gameController.shapePickerController.CurrentShape != null);
        aim.gameObject.SetActive(false);
        restartOnMain.gameObject.SetActive(gameController.isGameStarted);
    }

    public void ShowScorePanel()
    {
        ResetUI();
        scorePanel.gameObject.SetActive(true);
        score.text = "" + GetScore(gameController.gameTimer);
    }

    public void ShowFrame()
    {
        ResetUI();
        frame.gameObject.SetActive(true);
    }

    public void SetTimer(float time)
    {
        int minutes = (int)time / 60;
        int seconds = (int)time % 60;
        timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public int GetScore(float time)
    {
        int score = Mathf.RoundToInt(gameController.timerValue - time * 1000);
        if (score < 0)
        {
            score = 0;
        }
        return score;
    }
}
