using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Block : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    ClassicalModeStatus levelStatus;
    public GameObject blockParts;

    // This is to reinstantiate a new block at its place when this is placed on a map
    GameObject blockInstantiator;

    Transform initialParent;
    Vector3 initialPosition;
    Vector3 shrinkedScale;

    // Incase one of the blockParts is on top of the block
    public List<GameObject> conflictBlockParts = new List<GameObject>();
    // Incase one of the blockParts is outside of map limits
    public List<GameObject> outsideMapLimitBlockParts = new List<GameObject>();

    int angle = 0;
    bool dragging = false;

    void Start()
    {
        levelStatus = FindObjectOfType<ClassicalModeStatus>();
    }

    void Update()
    {
        if (blockParts.transform.childCount == 0)
        {
            Destroy(gameObject);
        }
    }

    #region Public Methods
    public void TurnBlock()
    {
        angle -= 90;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void CreateBlock(GameObject _blockInstantiator)
    {
        blockInstantiator = _blockInstantiator;
        // this is done outside of start and awake methods so its scale gets adjasted to canvas scale
        SetInitialValues();
        Shrink();

        Color32 blockColor = new Color32(
            (byte)Random.Range(100, 255),
            (byte)Random.Range(100, 255),
            (byte)Random.Range(100, 255), 255);

        for (int i = 0; i < blockParts.transform.childCount; i++)
        {
            blockParts.transform.GetChild(i).GetComponent<BlockPart>().CreateBlockPart(blockColor);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (levelStatus.swipeUnlocked && dragging && levelStatus.blockDragging)
        {
            // Drag complete, allow dragging new shape
            levelStatus.blockDragging = false;
            dragging = false;
            if (conflictBlockParts.Count == 0 && outsideMapLimitBlockParts.Count == 0)
            {
                levelStatus.swipeUnlocked = false;
                for (int i = 0; i < blockParts.transform.childCount; i++)
                {
                    blockParts.transform.GetChild(i).GetComponent<BlockPart>().DropBlockOnMap();
                }
                levelStatus.GetAllMapBlockParts();
                blockInstantiator.GetComponent<BlockInstantiator>().InstantiateBlock();
            }
            else
            {
                // Drag failed, return to initial position and allow swiping again
                levelStatus.swipeUnlocked = true;

                transform.SetParent(initialParent);
                transform.position = initialPosition;
                Shrink();

                for (int i = 0; i < blockParts.transform.childCount; i++)
                {
                    // Make a block that has just been placed have the same order with other blocks
                    blockParts.transform.GetChild(i).Find("Icon").GetComponent<SpriteRenderer>().sortingOrder = 4;
                    // Make a block that is new appear above all other blocks
                    blockParts.transform.GetChild(i).GetComponent<BlockPart>().dragging = false;
                }
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (levelStatus.swipeUnlocked && !levelStatus.blockDragging)
        {
            // Drag started, block dragging new shape until this one snaps or returns
            levelStatus.blockDragging = true;
            dragging = true;
            transform.SetParent(levelStatus.blocksParent);
            Expand();

            for (int i = 0; i < blockParts.transform.childCount; i++)
            {
                // Make a block that is new appear above all other blocks
                blockParts.transform.GetChild(i).GetComponent<BlockPart>().dragging = true;
                blockParts.transform.GetChild(i).Find("Icon").GetComponent<SpriteRenderer>().sortingOrder = 4;
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (levelStatus.swipeUnlocked && dragging)
        {
            transform.position = new Vector3(
                Camera.main.ScreenToWorldPoint(eventData.position).x,
                Camera.main.ScreenToWorldPoint(eventData.position).y + 100,
                20);
        }
    }

    public void AddOutsideMapLimit(GameObject blockPart)
    {
        if (!outsideMapLimitBlockParts.Contains(blockPart))
        {
            outsideMapLimitBlockParts.Add(blockPart);
        }
    }

    public void RemoveOutsideMapLimit(GameObject blockPart)
    {
        if (outsideMapLimitBlockParts.Contains(blockPart))
        {
            outsideMapLimitBlockParts.Remove(blockPart);
        }
    }

    public void AddConflictBlockPart(GameObject blockPart)
    {
        if (!conflictBlockParts.Contains(blockPart))
        {
            conflictBlockParts.Add(blockPart);
        }
    }

    public void RemoveConflictBlockPart(GameObject blockPart)
    {
        if (conflictBlockParts.Contains(blockPart))
        {
            conflictBlockParts.Remove(blockPart);
        }
    }
    #endregion

    #region Private Methods
    void SetInitialValues()
    {
        initialParent = transform.parent;
        initialPosition = transform.position;
        shrinkedScale = transform.localScale * 0.5f;
    }

    void Shrink()
    {
        transform.localScale = shrinkedScale;
    }

    void Expand()
    {
        transform.localScale = Vector3.one;
    }
    #endregion
}
