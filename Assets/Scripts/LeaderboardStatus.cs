using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardStatus : MonoBehaviour
{
    Player player;
    Navigator navigator;

    [SerializeField] GameObject leaderboardCanvas;
    [SerializeField] GameObject changeNameCanvas;

    [SerializeField] GameObject leaderboardItemPrefab;
    [SerializeField] GameObject leaderboardItemTrippleDots;

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
    public void ClickBackButton()
    {
        navigator.LoadMainScene();
    }

    public void ClickChangeNameButton()
    {
        leaderboardCanvas.SetActive(false);
        changeNameCanvas.SetActive(true);
    }
    #endregion

    #region Private Methods
    #endregion
}
