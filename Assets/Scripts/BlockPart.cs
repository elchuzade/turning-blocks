using UnityEngine;

public class BlockPart : MonoBehaviour
{
    LevelStatus levelStatus;
    Cell[] allCells;

    // A square that snaps to cell when dragged
    public GameObject snapPlaceholder;

    // To actiavet snap logic while dragging
    public bool dragging;

    // To count as a block part when it is dragged and dropped on the map
    public bool onMap = true;
    // To indicate falling down after drag and drop or after palette swipe
    public bool moving = false;

    Rigidbody2D rb;
    // To detect when velocity changes its direction that is block part has hit some obstacle and has to stop
    float prevVelocity;

    void Start()
    {
        allCells = FindObjectsOfType<Cell>();
        levelStatus = FindObjectOfType<LevelStatus>();

        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (moving)
        {
            // Hit some obstacle, stop moving
            if (rb.velocity.y > prevVelocity)
            {
                SnapToCell();
                rb.bodyType = RigidbodyType2D.Static;
                moving = false;
                levelStatus.AddIdleBlockPart(gameObject);
                prevVelocity = 0;
            }
            else
            {
                // accelerate falling down by 1 unit and reset previous velocity to detect hit
                prevVelocity = rb.velocity.y;
                rb.velocity = new Vector3(0, rb.velocity.y - 1, 0);
            }
        }
    }

    void Update()
    {
        if (dragging)
        {
            GetClosestCell();
        }
    }

    #region Public Methods
    public void CreateBlockPart(Color32 blockColor)
    {
        onMap = false;
        transform.Find("Icon").GetComponent<SpriteRenderer>().color = blockColor;
    }

    public void OccupyNearestCell()
    {
        transform.Find("Core").GetComponent<BlockCore>().SnapToNearestCell();
    }

    public void DropBlockOnMap()
    {
        dragging = false;
        onMap = true;
        snapPlaceholder.SetActive(false);
        rb.bodyType = RigidbodyType2D.Dynamic;

        transform.position = snapPlaceholder.transform.position;
        // Make a block that has just been placed have the same order with other blocks
        transform.Find("Icon").GetComponent<SpriteRenderer>().sortingOrder = 4;
        levelStatus.GetAllMapBlockParts();
        // False to not move already placed block parts but only this one
        levelStatus.MoveBlockPartsDown(false);
    }

    public void MoveBlockDown()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.velocity = new Vector3(0, -1, 0);
        moving = true;
    }

    public void SnapToCell()
    {
        transform.Find("Core").GetComponent<BlockCore>().SnapToNearestCell();
        snapPlaceholder.SetActive(false);
        OccupyNearestCell();
    }

    public void RemoveBlock()
    {
        GetComponent<AnimationTrigger>().Trigger("Start");
        Destroy(gameObject, 1);
    }
    #endregion

    #region Private Methods
    void GetClosestCell()
    {
        // Assume that closest cell is the first cell on the palette
        // Will change as we loop through all cells and get distances
        Cell closestCell = allCells[0];
        // Random distance greater than normally  it should be so that any cell overwrites this value
        float closestCellDistance = 100;

        for (int i = 0; i < allCells.Length; i++)
        {
            float distance = Vector2.Distance(transform.position, allCells[i].transform.position);
            if (distance < closestCellDistance)
            {
                closestCell = allCells[i];
                closestCellDistance = distance;
            }
        }
        // Snap your snapPlaceholder to that cell, diagonal with side 30 is the minimum distance 
        if (closestCellDistance < Mathf.Sqrt((30 * 30) + (30 * 30)) &&
            Mathf.Abs(closestCell.transform.position.x - transform.position.x) < 30 &&
            Mathf.Abs(closestCell.transform.position.y - transform.position.y) < 30)
        {
            // Cell is approached
            if (closestCell.free)
            {
                // Incase at least one block part of a block is outside of map limits
                transform.parent.parent.GetComponent<Block>().RemoveOutsideMapLimit(gameObject);
                // Remove yourself from conflict list incase previously you were conflicting
                transform.parent.parent.GetComponent<Block>().RemoveConflictBlockPart(gameObject);
                // This cell is not conflicting, check neighboring cells
                if (transform.parent.parent.GetComponent<Block>().conflictBlockParts.Count == 0 &&
                    transform.parent.parent.GetComponent<Block>().outsideMapLimitBlockParts.Count == 0)
                {
                    // All good, no conflicts in the whole block
                    snapPlaceholder.SetActive(true);
                    snapPlaceholder.transform.position = closestCell.transform.position;
                }
                else
                {
                    snapPlaceholder.SetActive(false);
                }
            }
            else
            {
                // Conflict created
                transform.parent.parent.GetComponent<Block>().AddConflictBlockPart(gameObject);
                snapPlaceholder.SetActive(false);
            }
        }
        else
        {
            // Block part is outside map limits
            transform.parent.parent.GetComponent<Block>().AddOutsideMapLimit(gameObject);
            snapPlaceholder.SetActive(false);
        }
    }
    #endregion
}
