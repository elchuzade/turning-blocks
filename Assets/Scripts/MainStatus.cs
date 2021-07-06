using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainStatus : MonoBehaviour
{
    Navigator navigator;
    Player player;

    [SerializeField] GameObject settingsWindowCanvas;
    [SerializeField] GameObject mainCanvas;

    void Start()
    {
        navigator = FindObjectOfType<Navigator>();
        player = FindObjectOfType<Player>();

        player.LoadPlayer();
        //player.ResetPlayer();
    }

    void Update()
    {
        
    }

    #region Public Methods
    public void ClickModesButton()
    {
        
    }

    public void ClickSettingsButton()
    {
        mainCanvas.SetActive(false);
        settingsWindowCanvas.SetActive(true);
    }

    public void ClickPlayButton()
    {
        navigator.LoadGameScene();
    }
    #endregion

    #region Private Methods

    #endregion
}
