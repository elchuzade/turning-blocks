using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainStatus : MonoBehaviour
{
    Navigator navigator;
    Player player;

    [SerializeField] GameObject settingsCanvas;
    [SerializeField] GameObject mainCanvas;
    [SerializeField] GameObject privacyPolicyCanvas;
    [SerializeField] GameObject quitCanvas;
    [SerializeField] GameObject gameModesCanvas;

    [SerializeField] Scoreboard scoreboard;

    void Start()
    {
        navigator = FindObjectOfType<Navigator>();
        player = FindObjectOfType<Player>();

        player.LoadPlayer();
        //player.ResetPlayer();

        if (player.privacyPolicyAccepted)
        {
            //leaderboardButton.GetComponent<Button>().onClick.AddListener(() => ClickLeaderboardButton());

            if (!player.playerCreated)
            {
                //server.CreatePlayer(player);
            }
            else
            {
                //server.SavePlayerData(player);
            }
        }
        else
        {
            //leaderboardButton.GetComponent<Button>().onClick.AddListener(() => ShowPrivacyPolicy());

            if (!player.privacyPolicyDeclined)
            {
                ShowPrivacyPolicyWindow();
            }
        }

        scoreboard.SetCoins(player.coins, false);
        scoreboard.SetDiamonds(player.diamonds, false);
    }

    void Update()
    {
        // Android back button reacts as escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowQuitWindow();
        }
    }

    #region Public Methods
    public void ClickGameModesButton()
    {
        mainCanvas.SetActive(false);
        gameModesCanvas.SetActive(true);
    }

    public void ClickSettingsButton()
    {
        mainCanvas.SetActive(false);
        settingsCanvas.SetActive(true);
    }

    public void ClickPlayButton()
    {
        navigator.LoadGameScene();
    }
    #endregion

    #region Private Methods
    void ShowPrivacyPolicyWindow()
    {
        mainCanvas.SetActive(false);
        privacyPolicyCanvas.SetActive(true);
    }

    void ShowQuitWindow()
    {
        mainCanvas.SetActive(false);
        quitCanvas.SetActive(true);
    }
    #endregion
}
