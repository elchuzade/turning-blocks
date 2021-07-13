using UnityEngine.UI;
using UnityEngine;
using static GlobalVariables;

public class GameModesWindow : MonoBehaviour
{
    Player player;

    [SerializeField] GameObject gameModesCanvas;
    [SerializeField] GameObject mainCanvas;

    [SerializeField] GameObject gameModeLeftArrow;
    [SerializeField] GameObject gameModeRightArrow;
    [SerializeField] Scrollbar gameModeScrollbar;
    int totalGameModes = 3;
    int gameModeIndex;
    GameModes currentGameMode;

    void Start()
    {
        player = FindObjectOfType<Player>();
        player.LoadPlayer();
        //player.ResetPlayer();

        currentGameMode = player.gameMode;
        gameModeIndex = (int)currentGameMode;
        CheckGameModeArrows();

        gameModeScrollbar.onValueChanged.AddListener(value => SwipeGameMode(value));
        // Unity Bug: Need to set value once in Start and once when modesWindow is opened
        gameModeScrollbar.value = Mathf.Abs(GetGameModeScrollbarValue() - 0.01f);
        gameModeScrollbar.size = 1 / totalGameModes;
    }

    #region Public Methods
    public void CloseGameModesWindow()
    {
        gameModesCanvas.SetActive(false);
        mainCanvas.SetActive(true);
    }

    public void ClickLeftArrow()
    {
        gameModeIndex--;
        currentGameMode = (GameModes)gameModeIndex;
        SwitchGameMode();
        gameModeScrollbar.value = Mathf.Abs(GetGameModeScrollbarValue() - 0.01f);
    }

    public void ClickRightArrow()
    {
        gameModeIndex++;
        currentGameMode = (GameModes)gameModeIndex;
        SwitchGameMode();
        gameModeScrollbar.value = Mathf.Abs(GetGameModeScrollbarValue() - 0.01f);
    }

    public void SwipeGameMode(float value)
    {
        gameModeIndex = Mathf.Clamp((int)(totalGameModes * value), 0, totalGameModes - 1);
        currentGameMode = (GameModes)gameModeIndex;
        SwitchGameMode();
        CheckGameModeArrows();
    }
    #endregion

    #region Private Methods
    void CheckGameModeArrows()
    {
        if (gameModeIndex == 0)
        {
            SetLeftArrowDisabled();
        }
        else if (gameModeIndex == totalGameModes - 1)
        {
            SetRightArrowDisabled();
        }
        else
        {
            EnableBothArrows();
        }
    }

    void SwitchGameMode()
    {
        player.gameMode = currentGameMode;
        player.SavePlayer();
        // TODO: Change All Words in this menu to current language
    }

    float GetGameModeScrollbarValue()
    {
        return (float)(int)currentGameMode / (totalGameModes - 1);
    }

    void EnableBothArrows()
    {
        gameModeLeftArrow.SetActive(true);
        gameModeRightArrow.SetActive(true);
    }

    void SetLeftArrowDisabled()
    {
        gameModeLeftArrow.SetActive(false);
    }

    void SetRightArrowDisabled()
    {
        gameModeRightArrow.SetActive(false);
    }
    #endregion
}
