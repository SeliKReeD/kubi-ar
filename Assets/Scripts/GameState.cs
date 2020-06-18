using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    private Tower tower;
    public Tower Tower
    {
        get
        {
            return tower;
        }
        set
        {
            tower = value;
            grid = tower.grid;
        }
    }
    private Transform grid;

    public System.Action onLevelComplete;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<ShapePickerController>().onDropObject += FinishGameCheck;
    }

    private void FinishGameCheck()
    {
        foreach (var figure in tower.parts)
        {
            bool figureInGrid = true;
            foreach (var part in figure.parts)
            {
                bool partInGrid = false;
                foreach (var gridItem in grid.GetComponentsInChildren<BoxCollider>())
                {
                    if (gridItem.bounds.Contains(part.transform.position))
                    {
                        partInGrid = true;
                        break;
                    }
                }
                if (!partInGrid)
                {
                    figureInGrid = false;
                    break;
                }
            }
            if (!figureInGrid)
            {
                return;
            }
        }
        onLevelComplete?.Invoke();
    }
}
