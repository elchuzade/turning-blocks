using UnityEngine;

public class PrivacyPolicyWindow : MonoBehaviour
{
    Player player;

    [SerializeField] GameObject mainCanvas;
    [SerializeField] GameObject privacyPolicyCanvas;

    void Start()
    {
        player = FindObjectOfType<Player>();
        player.LoadPlayer();
        //player.ResetPlayer();
    }

    #region Public Methods
    public void ClickTermsOfUse()
    {
        Application.OpenURL("https://abboxgames.com/terms-of-use");
    }

    public void ClickPrivacyPolicy()
    {
        Application.OpenURL("https://abboxgames.com/privacy-policy");
    }

    public void ClickAcceptPrivacyPolicy()
    {
        //leaderboardButton.transform.Find("Components").Find("Frame").GetComponent<Image>().color = new Color32(255, 197, 158, 255);
        //leaderboardButton.transform.Find("Components").Find("Icon").GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        //leaderboardButton.GetComponent<Button>().onClick.AddListener(() => ClickLeaderboardButton());

        ClosePrivacyPolicyWindow();
        player.privacyPolicyDeclined = false;
        player.privacyPolicyAccepted = true;
        player.SavePlayer();

        //server.CreatePlayer(player);
    }

    public void ClickRejectPrivacyPolicy()
    {
        //leaderboardButton.GetComponent<Button>().onClick.AddListener(() => privacyWindow.SetActive(true));
        ClosePrivacyPolicyWindow();
        player.privacyPolicyDeclined = true;
        player.privacyPolicyAccepted = false;
        player.SavePlayer();
    }
    #endregion

    #region Private Methods
    void ClosePrivacyPolicyWindow()
    {
        privacyPolicyCanvas.SetActive(false);
        mainCanvas.SetActive(true);
    }
    #endregion
}
