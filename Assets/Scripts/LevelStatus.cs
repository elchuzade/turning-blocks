using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GlobalVariables;

public class LevelStatus : MonoBehaviour
{
    Player player;
    Navigator navigator;
    GameOverWindow gameOverWindow;

    // To change the color of header based on the game mode
    [SerializeField] GameObject[] colorfulObjects;

    Color32 infiniteColor = new Color32(189, 59, 160, 255);
    Color32 randomColor = new Color32(59, 160, 189, 255);

    // This is to hide when playing random game mode
    [SerializeField] GameObject swipeArrows;

    Cell[] cells;
    [SerializeField] GameObject scorePrefab;
    [SerializeField] GameObject levelCanvas;
    [SerializeField] Text timeText;
    [SerializeField] Text scoreText;
    [SerializeField] GameObject timerIcon;

    [SerializeField] List<GameObject> aligners = new List<GameObject>();
    List<GameObject> destroyBlockParts = new List<GameObject>();

    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] GameObject palette;

    public Transform blocksParent;

    [SerializeField] GameObject[] allBlockInstantiators;

    public List<GameObject> allMapBlockParts = new List<GameObject>();

    public List<GameObject> idleBlockParts = new List<GameObject>();

    int angle = 0;
    int nextAngle = 0;
    int deltaAngle = 0;
    bool rotating = false;
    public bool swipeUnlocked = true;
    // To stop multiple shapes dragging
    public bool blockDragging = false;

    public Block dragginBlock;

    public int totalScore = 0;
    // Score that we get when aligning blocks
    int score;
    int seconds = 1000; // Seconds
    float timer = 0;
    public bool gameOver;

    // For delayed game over window to compare count of idle block parts with all block parts
    public bool allBlockPartsFell = true;

    // Incase stuck in the swiping while time is up
    bool gameOverPending;

    void Start()
    {
        navigator = FindObjectOfType<Navigator>();

        player = FindObjectOfType<Player>();
        gameOverWindow = FindObjectOfType<GameOverWindow>();

        player.LoadPlayer();

        cells = FindObjectsOfType<Cell>();
        Time.timeScale = 4;
        InstantiateNewBlocks();
        SetTimer();
        SetScore();

        SetGameMode();
    }

    void Update()
    {
        // START DEBUG
        // Android back button reacts as escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            navigator.LoadMainScene();
        }
        // END DEBUG

        if (angle != nextAngle)
        {
            angle += deltaAngle;
            palette.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            if (rotating)
            {
                rotating = false;
                if (allMapBlockParts.Count > 0)
                {
                    allBlockPartsFell = false;
                }
                // Finished rotating make blocks fall
                MoveBlockPartsDown(true);
            }
        }
        if (!gameOverPending)
        {
            // TimeScale is increased by 4. so all time related things should be increased by 4
            if (!gameOver)
            {
                if (timer >= 1 * 4)
                {
                    timer = 0;
                    seconds--;
                    SetTimer();
                }
                else
                {
                    timer += Time.deltaTime;
                }
            }
        }
    }

    #region Public Methods
    public void DropBlockOnMap()
    {
        dragginBlock = null;
        blockDragging = false;
        swipeUnlocked = false;
        allBlockPartsFell = false;
    }

    public void AddSeconds(int _seconds)
    {
        seconds += _seconds;
        gameOver = false;
        SetTimer();
    }

    public void AddIdleBlockPart(GameObject part)
    {
        if (!idleBlockParts.Contains(part))
        {
            idleBlockParts.Add(part);
            allBlockPartsFell = false;
            if (idleBlockParts.Count == allMapBlockParts.Count && idleBlockParts.Count > 0)
            {
                idleBlockParts.Clear();
                allBlockPartsFell = true;
                // Refresh free/occupied status of every cell when all blocks are idle
                CheckCellsFreeStatus();
                ChectAlignedCells();

                // This is incase the time is up but there are still blocks dropping
                // Note: if blocks dropped and they scored some value, time will be added and game over will be cancelled
                if (gameOverPending && seconds == 0)
                {
                    gameOverPending = false;
                    ShowGameOverWindow();
                } else
                {
                    // While falling down you earned extra seconds, so no game over yet
                    gameOverPending = false;
                }
            }
        }
    }

    public void SwipePalette(DraggedDirections direction)
    {
        // Swipe is finished
        if (swipeUnlocked && player.gameMode != GameModes.random)
        {
            if (direction == DraggedDirections.right)
            {
                SwipeRight();
            }
            else if (direction == DraggedDirections.left)
            {
                SwipeLeft();
            }
        }
    }

    public void GetAllMapBlockParts()
    {
        allMapBlockParts.Clear();

        BlockPart[] allBlockParts = FindObjectsOfType<BlockPart>();
        // Adding only parts that are on the map
        for (int i = 0; i < allBlockParts.Length; i++)
        {
            if (allBlockParts[i].onMap && !allMapBlockParts.Contains(allBlockParts[i].gameObject))
            {
                allMapBlockParts.Add(allBlockParts[i].gameObject);
            }
        }
    }

    public void MoveBlockPartsDown(bool allBlockParts)
    {
        if (allMapBlockParts.Count > 0)
        {
            for (int i = 0; i < allMapBlockParts.Count; i++)
            {
                if (allMapBlockParts[i].GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Dynamic || allBlockParts)
                {
                    allMapBlockParts[i].GetComponent<BlockPart>().MoveBlockDown();
                }
                else
                {
                    AddIdleBlockPart(allMapBlockParts[i]);
                }
            }
        }
        else
        {
            // Nothing on the map, no need to wait for block parts to stop moving down before unlokcing swipe
            swipeUnlocked = true;
        }
    }
    #endregion

    #region Private Metods
    void SwipeRight()
    {
        nextAngle += 90;
        deltaAngle = 3;
        rotating = true;
        swipeUnlocked = false;
    }

    void SwipeLeft()
    {
        nextAngle -= 90;
        deltaAngle = -3;
        rotating = true;
        swipeUnlocked = false;
    }

    void SetGameMode()
    {
        // Hide arrows if randomg game mode and set color
        if (player.gameMode == GameModes.random)
        {
            swipeArrows.SetActive(false);
            for (int i = 0; i < colorfulObjects.Length; i++)
            {
                colorfulObjects[i].GetComponent<SpriteRenderer>().color = randomColor;
            }
        } else if (player.gameMode == GameModes.infinite)
        {
            for (int i = 0; i < colorfulObjects.Length; i++)
            {
                colorfulObjects[i].GetComponent<SpriteRenderer>().color = infiniteColor;
            }
        }
    }

    void CheckCellsFreeStatus()
    {
        for (int i = 0; i < cells.Length; i++)
        {
            cells[i].CheckFreeStatus();
        }
    }

    void GiveScore()
    {
        switch (destroyBlockParts.Count)
        {
            case 10:
                totalScore += 10;
                score = 10;
                seconds += 1;
                break;
            case 20:
                totalScore += 30;
                score = 30;
                seconds += 3;
                break;
            case 30:
                totalScore += 60;
                score = 60;
                seconds += 6;
                break;
            case 40:
                totalScore += 100;
                score = 100;
                seconds += 10;
                break;
            case 50:
                totalScore += 150;
                score = 150;
                seconds += 15;
                break;
            case 60:
                totalScore += 200;
                score = 200;
                seconds += 20;
                break;
        }
        GameObject newScore = Instantiate(scorePrefab, palette.transform.position, Quaternion.identity);
        newScore.transform.SetParent(levelCanvas.transform);
        newScore.GetComponent<NewScore>().SetScore(score);
        SetTimer();
        SetScore();
        // Reset Score
        score = 0;
    }

    void SetScore()
    {
        scoreText.text = totalScore.ToString();
    }

    void SetTimer()
    {
        TimeSpan t = TimeSpan.FromSeconds(seconds);

        string second = t.Seconds.ToString();
        if (t.Seconds == 0)
        {
            second = "00";
        }

        timeText.text = t.Minutes + ":" + second;

        if (seconds == 0)
        {
            // Incase some block is being dragged
            if (dragginBlock != null)
            {
                dragginBlock.ReturnToInitialPosition();
            }

            if (!rotating && allBlockPartsFell)
            {
                ShowGameOverWindow();
            } else
            {
                gameOverPending = true;
            }
        }
    }

    void ShowGameOverWindow()
    {
        gameOver = true;
        gameOverCanvas.SetActive(true);
        palette.SetActive(false);
        levelCanvas.SetActive(false);

        if (gameOverWindow == null)
        {
            gameOverWindow = FindObjectOfType<GameOverWindow>();
        }

        gameOverWindow.SetScores();
    }

    void MoveAllBlockPartsDown()
    {
        MoveBlockPartsDown(true);
    }

    // This is so invoke works properly
    void UnlockSwipe()
    {
        swipeUnlocked = true;
    }

    void ChectAlignedCells()
    {
        // Check horizontal alignment. Inside every row check the number of not free cells.
        // If it is 10 then add all those BlockParts into the destroy list to destroy at the end
        CheckHorizontalCells();
        CheckVerticalCells();
        // Destroy all blocks that were aligned
        DestroyAlignedBlocks();
    }

    void CheckVerticalCells()
    {
        // Chek vertical alignment
        for (int i = 0; i < aligners.Count; i++)
        {
            List<GameObject> alignedCols = new List<GameObject>();
            for (int j = 0; j < aligners[i].transform.childCount; j++)
            {
                // swapping i and j we get columns instead of rows
                if (!aligners[j].transform.GetChild(i).GetComponent<Cell>().free)
                {
                    alignedCols.Add(aligners[j].transform.GetChild(i).gameObject);
                }
            }
            if (alignedCols.Count == 10)
            {
                for (int k = 0; k < alignedCols.Count; k++)
                {
                    //if (!destroyBlockParts.Contains(alignedCols[k]))
                    //{
                    destroyBlockParts.Add(alignedCols[k]);
                    //}
                }
            }
        }
    }

    void CheckHorizontalCells()
    {
        // Check each row for aligned items
        for (int i = 0; i < aligners.Count; i++)
        {
            List<GameObject> alignedRows = new List<GameObject>();
            for (int j = 0; j < aligners[i].transform.childCount; j++)
            {
                if (!aligners[i].transform.GetChild(j).GetComponent<Cell>().free)
                {
                    alignedRows.Add(aligners[i].transform.GetChild(j).gameObject);
                }
            }
            if (alignedRows.Count == 10)
            {
                for (int k = 0; k < alignedRows.Count; k++)
                {
                    destroyBlockParts.Add(alignedRows[k]);
                }
            }
        }
    }

    void DestroyAlignedBlocks()
    {
        if (destroyBlockParts.Count > 0)
        {
            GiveScore();
            // This is to remove duplicates from horizontal and vertical aligners
            List<GameObject> uniqueDestroyBlockParts = new List<GameObject>();
            for (int i = 0; i < destroyBlockParts.Count; i++)
            {
                if (!uniqueDestroyBlockParts.Contains(destroyBlockParts[i]))
                {
                    uniqueDestroyBlockParts.Add(destroyBlockParts[i]);
                }
            }

            // Extract BlockParts from those cells that are not free
            for (int i = 0; i < uniqueDestroyBlockParts.Count; i++)
            {
                BlockPart occupiedBy = uniqueDestroyBlockParts[i].GetComponent<Cell>().occupiedBy;
                if (occupiedBy != null)
                {
                    uniqueDestroyBlockParts[i].GetComponent<Cell>().free = true;
                    occupiedBy.RemoveBlock();
                }
            }
            destroyBlockParts.Clear();
            Invoke("GetAllMapBlockParts", 1);
            // Move all map block parts even if they are idle and cant be moved down
            Invoke("MoveAllBlockPartsDown", 1);
        }
        else
        {
            UnlockSwipe();

            if (player.gameMode == GameModes.random)
            {
                // If game mode is random make some swipes randomly every 5 actions make 1 swipe
                int randomlySwipeIndex = UnityEngine.Random.Range(0, 5);

                if (randomlySwipeIndex == 0)
                {
                    // Choose random direction for swiping
                    bool rightSwipe = UnityEngine.Random.Range(0, 2) == 0;

                    if (rightSwipe)
                    {
                        //Invoke("SwipeRight", 1);
                        SwipeRight();
                    }
                    else
                    {
                        //Invoke("SwipeLeft", 1);
                        SwipeLeft();
                    }
                }
            }
        }
    }

    void InstantiateNewBlocks()
    {
        for (int i = 0; i < allBlockInstantiators.Length; i++)
        {
            allBlockInstantiators[i].GetComponent<BlockInstantiator>().InstantiateBlock();
        }
    }
    #endregion
}
