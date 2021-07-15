using UnityEngine;
using UnityEngine.UI;
using static GlobalVariables;

public class LeaderboardStatus : MonoBehaviour
{
    Player player;
    Navigator navigator;

    [SerializeField] GameObject leaderboardCanvas;
    [SerializeField] GameObject changeNameCanvas;

    [SerializeField] GameObject leaderboardItemPrefab;
    [SerializeField] GameObject leaderboardItemTrippleDots;

    // For language buttons
    [SerializeField] TextButton[] allTextButtons;

    /*
        Name in different languages
    */
    string nameEnglish = "name";
    string nameSpanish = "nombre";
    string nameFrench = "nom";
    string nameGerman = "name";
    string nameTurkish = "isim";
    string nameRussian = "name";

    [SerializeField] Text namePlaceholder;

    void Start()
    {
        navigator = FindObjectOfType<Navigator>();
        player = FindObjectOfType<Player>();

        player.LoadPlayer();
        //player.ResetPlayer();

        SetNamePlaceholder();

        SetAllLanguageButtons();
    }

    #region Public Methods
    public void SetAllLanguageButtons()
    {
        for (int i = 0; i < allTextButtons.Length; i++)
        {
            allTextButtons[i].SetButtonLanguage(player.language);
        }
    }

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
    void SetNamePlaceholder()
    {
        switch (player.language)
        {
            case Languages.english:
                namePlaceholder.text = nameEnglish;
                break;
            case Languages.russian:
                namePlaceholder.text = nameRussian;
                break;
            case Languages.turkish:
                namePlaceholder.text = nameTurkish;
                break;
            case Languages.german:
                namePlaceholder.text = nameGerman;
                break;
            case Languages.spanish:
                namePlaceholder.text = nameSpanish;
                break;
            case Languages.french:
                namePlaceholder.text = nameFrench;
                break;
        }
    }
    #endregion
}
