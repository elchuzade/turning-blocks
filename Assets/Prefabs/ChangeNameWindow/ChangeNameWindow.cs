using UnityEngine.UI;
using UnityEngine;

public class ChangeNameWindow : MonoBehaviour
{
    Player player;

    [SerializeField] GameObject leaderboardCanvas;
    [SerializeField] GameObject changeNameCanvas;

    [SerializeField] InputField nameInput;

    void Start()
    {
        player = FindObjectOfType<Player>();
        player.LoadPlayer();
        //player.ResetPlayer();
    }

    #region Public Methods
    public void ClickSaveNameButton()
    {
        if (nameInput.text.Length > 0)
        {
            //server.ChangePlayerName(nameInput.text);
            player.playerName = nameInput.text;
            player.nameChanged = true;
            player.SavePlayer();

            CloseChangeNameWindow();
        }
    }

    public void CloseChangeNameWindow()
    {
        leaderboardCanvas.SetActive(true);
        changeNameCanvas.SetActive(false);
    }
    #endregion

    #region Private Methods
    #endregion
}
