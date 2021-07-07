using UnityEngine;

public class QuitWindow : MonoBehaviour
{
    [SerializeField] GameObject mainCanvas;
    [SerializeField] GameObject quitCanvas;

    public void ClickQuitGame()
    {
        Application.Quit();
    }

    public void CloseQuitWindow()
    {
        quitCanvas.SetActive(false);
        mainCanvas.SetActive(true);
    }
}
