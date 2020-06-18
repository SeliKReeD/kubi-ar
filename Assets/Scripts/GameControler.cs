using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class GameControler : GameControllerBase
{
    public Tower currentTower;
    public List<Tower> towersPrefabs;
    public int indexForDrop = 0;
    public int IndexForDrop
    {
        get
        {
            return indexForDrop;
        }
        set
        {
            if (value < 0)
                value = towersPrefabs.Count - 1;
            else if (value > towersPrefabs.Count - 1)
                value = 0;
            indexForDrop = value;
            currentTower = DropTower(indexForDrop);
        }
    }

    public GameState gameState;
    public UIController uiController;
    public Rigidbody wreckingBall;
    public float wreckingBallElevation = 5f;
    
    public float timerValue;
    public float gameTimer;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        gameTimer = timerValue;
        gameState = GetComponent<GameState>();
        uiController = FindObjectOfType<UIController>();
        Subscribe();
    }

    public void Subscribe()
    {
        shapePickerController.onDropObject += OnDropObject;
        shapePickerController.onPickObject += OnPickObject;
        trackingHandler.OnTrackFound += TrackingFound;
        trackingHandler.OnTrackLost += TrackingLost;
        gameState.onLevelComplete += FinishGame;
    }

    private void Update()
    {
        if(isGameStarted)
        {
            if(gameTimer > 0)
            {
                gameTimer -= Time.deltaTime;
                uiController.SetTimer(gameTimer);
            }
            else
            {
                FinishGame();
            }
        }
    }

    public void StartLevel()
    {
        isGameStarted = true;
        if(gameMode == GameMode.CHALENGE)
        {
            currentTower.SetKinematic(false);
            StartCoroutine(Wreck());
            onStartLevel?.Invoke();
        }
        else if(gameMode == GameMode.FREE)
        {
            SceneManager.LoadScene(1);
        }
    }

    public void FinishGame()
    {
        uiController.ShowScorePanel();
        isGameStarted = false;
        gameTimer = timerValue;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void TrackingFound()
    {
        if(currentTower == null)
            currentTower = DropTower(indexForDrop);
        if (isGameStarted && currentTower != null)
            currentTower.SetKinematic(false);
    }

    public void TrackingLost()
    {
        if(currentTower)
            currentTower.SetKinematic(true);
    }

    IEnumerator Wreck()
    {
        wreckingBall.transform.position = Vector3.down * 2f * transform.lossyScale.x;
        while (wreckingBall.transform.position.y < 10f * transform.lossyScale.x)
        {
            wreckingBall.transform.Translate(Vector3.up * wreckingBallElevation * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
        Destroy(wreckingBall.gameObject);
    }

    public void OnPickObject(Figure f)
    {
        uiController.OnItemPicked();
    }

    public void OnDropObject()
    {
        uiController.OnItemDropped();
    }

    public Tower DropTower(int index)
    {
        if(currentTower != null)
        {
            Destroy(currentTower.gameObject);
            currentTower = null;
        }
        Tower tower = Instantiate(towersPrefabs[index]);
        tower.gameObject.transform.SetParent(trackingHandler.gameObject.transform);
        tower.gameObject.transform.localPosition = new Vector3(0f, 0.125f, 0f);
        gameState.Tower = tower;
        return tower;
    }
}

