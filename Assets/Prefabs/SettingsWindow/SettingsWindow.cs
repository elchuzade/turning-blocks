using UnityEngine.UI;
using UnityEngine;
using static GlobalVariables;

public class SettingsWindow : MonoBehaviour
{
    Player player;
    [SerializeField] MainStatus mainStatus;

    [SerializeField] GameObject settingsCanvas;
    [SerializeField] GameObject mainCanvas;

    [SerializeField] GameObject soundsIconDisabled;
    [SerializeField] GameObject hapticsIconDisabled;

    // LANGUAGE
    [SerializeField] GameObject languageLeftArrow;
    [SerializeField] GameObject languageRightArrow;
    [SerializeField] Scrollbar languageScrollbar;
    int totalLanguages = 6;
    int languageIndex;
    Languages currentLanguage;

    // For language buttons
    [SerializeField] TextButton[] allTextButtons;

    void Start()
    {
        player = FindObjectOfType<Player>();
        player.LoadPlayer();

        SetAllLanguageButtons();

        SetInitialSounds();
        SetInitialHaptics();

        currentLanguage = player.language;
        languageIndex = (int)currentLanguage;
        CheckLanguageArrows();

        languageScrollbar.onValueChanged.AddListener(value => SwipeLanguage(value));
        // Unity Bug: Need to set value once in Start and once when modesWindow is opened
        languageScrollbar.value = Mathf.Abs(GetLanguageScrollbarValue() - 0.01f);
        languageScrollbar.size = 1 / totalLanguages;
    }

    #region Public Methods
    public void SetAllLanguageButtons()
    {
        for (int i = 0; i < allTextButtons.Length; i++)
        {
            allTextButtons[i].SetButtonLanguage();
        }
    }

    public void CloseSettingsWindow()
    {
        settingsCanvas.SetActive(false);
        mainCanvas.SetActive(true);
    }

    // LANGUAGES
    public void ClickLeftArrow()
    {
        languageIndex--;
        currentLanguage = (Languages)languageIndex;
        SwitchLanguage();
        languageScrollbar.value = Mathf.Abs(GetLanguageScrollbarValue() - 0.01f);
    }

    public void ClickRightArrow()
    {
        languageIndex++;
        currentLanguage = (Languages)languageIndex;
        SwitchLanguage();
        languageScrollbar.value = Mathf.Abs(GetLanguageScrollbarValue() - 0.01f);
    }

    public void SwipeLanguage(float value)
    {
        languageIndex = Mathf.Clamp((int)(totalLanguages * value), 0, totalLanguages - 1);
        currentLanguage = (Languages)languageIndex;
        SwitchLanguage();
        CheckLanguageArrows();
    }

    // SOUNDS & HAPTICS
    public void ClickSoundsButton()
    {
        player.sounds = !player.sounds;
        player.SavePlayer();
        SetInitialSounds();
    }

    public void ClickHapticsButton()
    {
        player.haptics = !player.haptics;
        player.SavePlayer();
        SetInitialHaptics();
    }

    #endregion

    #region Private Methods
    void SetInitialSounds()
    {
        if (player.sounds)
        {
            soundsIconDisabled.SetActive(false);
        } else
        {
            soundsIconDisabled.SetActive(true);
        }
    }

    void SetInitialHaptics()
    {
        if (player.haptics)
        {
            hapticsIconDisabled.SetActive(false);
        }
        else
        {
            hapticsIconDisabled.SetActive(true);
        }
    }

    void CheckLanguageArrows()
    {
        if (languageIndex == 0)
        {
            SetLeftArrowDisabled();
        }
        else if (languageIndex == totalLanguages - 1)
        {
            SetRightArrowDisabled();
        }
        else
        {
            EnableBothArrows();
        }
    }

    void SwitchLanguage()
    {
        player.language = currentLanguage;
        player.SavePlayer();
        // TODO: Change All Words in this menu to current language
        SetAllLanguageButtons();
    }

    float GetLanguageScrollbarValue()
    {
        return (float)(int)currentLanguage / (totalLanguages - 1);
    }

    void EnableBothArrows()
    {
        languageLeftArrow.SetActive(true);
        languageRightArrow.SetActive(true);
    }

    void SetLeftArrowDisabled()
    {
        languageLeftArrow.SetActive(false);
    }

    void SetRightArrowDisabled()
    {
        languageRightArrow.SetActive(false);
    }
    #endregion
}
