using UnityEngine;

public class Cell : MonoBehaviour
{
    // occupied if the shape is snapped to it already
    // At the end of every movement every cell will try to find every block and check the distance if it is 0 than it is snapped
    public bool free = true;
    public BlockPart occupiedBy = null;

    public void CheckFreeStatus()
    {
        BlockPart[] allBlockParts = FindObjectsOfType<BlockPart>();

        float shortestDistance = 100;
        BlockPart nearestBlockPart = null;
        
        // Find the closest blockpart
        for (int i = 0; i < allBlockParts.Length; i++)
        {
            float distance = Vector2.Distance(allBlockParts[i].transform.position, transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestBlockPart = allBlockParts[i];
            }
        }
        // If distance is 0, the blockpart is ontop of the cell 30 is for margin
        if (shortestDistance < 30)
        {
            free = false;
            occupiedBy = nearestBlockPart;
        } else
        {
            free = true;
            occupiedBy = null;
        }
    }
}
