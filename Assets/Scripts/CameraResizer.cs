using UnityEngine;
using UnityEngine.UI;

public class CameraResizer : MonoBehaviour
{
    [SerializeField] GameObject canvas;

    void Start()
    {

        //Change the camera zoom based on the screen ratio, for very tall or very wide screens
        if ((float)Screen.height / Screen.width >= 2)
        {
            Camera.main.orthographicSize = 800;
        }
        else
        {
            Camera.main.orthographicSize = 667;

        }
        // Tablet screens
        if ((float)Screen.width / Screen.height > 0.6)
        {
            canvas.GetComponent<CanvasScaler>().matchWidthOrHeight = 1;
        }
        else
        {
            // Phone screens
            canvas.GetComponent<CanvasScaler>().matchWidthOrHeight = 0;
        }
    }
}
