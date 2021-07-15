using UnityEngine;
using UnityEngine.EventSystems;
using static GlobalVariables;

// This class is used to manage the grid UI meaning position the tails, resize them, and create the grid seen by the user
public class PaletteSwiper : MonoBehaviour, IDragHandler, IEndDragHandler
{
    ClassicalModeStatus levelStatus;

    void Start()
    {
        levelStatus = FindObjectOfType<ClassicalModeStatus>();
    }

    #region  IDragHandler - IEndDragHandler
    public void OnEndDrag(PointerEventData eventData)
    {
        Vector3 dragVectorDirection = (eventData.position - eventData.pressPosition).normalized;

        levelStatus.SwipePalette(GetDragDirection(dragVectorDirection));
    }

    // It must be implemented otherwise IEndDragHandler won't work 
    public void OnDrag(PointerEventData eventData) { }

    DraggedDirections GetDragDirection(Vector3 dragVector)
    {
        float positiveX = Mathf.Abs(dragVector.x);
        float positiveY = Mathf.Abs(dragVector.y);
        DraggedDirections draggedDir;
        if (positiveX > positiveY)
        {
            draggedDir = (dragVector.x > 0) ? DraggedDirections.right : DraggedDirections.left;
        }
        else
        {
            draggedDir = (dragVector.y > 0) ? DraggedDirections.up : DraggedDirections.down;
        }

        return draggedDir;
    }
    #endregion
}