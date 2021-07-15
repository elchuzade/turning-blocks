using System.Collections.Generic;
using UnityEngine;

public class BlockCore : MonoBehaviour
{
    List<GameObject> snappingCells = new List<GameObject>();
    Cell snappedCell;

    public void SnapToNearestCell()
    {
        if (snappingCells.Count > 0)
        {
            if (snappedCell != null) {
                // Free the old cell
                snappedCell.occupiedBy = null;
                snappedCell.free = true;
            }

            transform.parent.position = snappingCells[snappingCells.Count - 1].transform.position;
            
            // Occupy the new cell
            snappedCell = snappingCells[snappingCells.Count - 1].GetComponent<Cell>();
            snappedCell.occupiedBy = transform.parent.gameObject.GetComponent<BlockPart>();
            snappedCell.free = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CellCore")
        {
            snappingCells.Add(collision.gameObject);
        }
    }
}
