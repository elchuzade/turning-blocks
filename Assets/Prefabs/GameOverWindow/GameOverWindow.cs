using UnityEngine;
using UnityEngine.UI;
using static GlobalVariables;

public class GameOverWindow : MonoBehaviour
{
    Translator translator;
    Player player;
    Navigator navigator;

    LevelStatus levelStatus;

    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] GameObject levelCanvas;
    [SerializeField] GameObject palette;

    [SerializeField] Text scoreText;
    [SerializeField] Text bestText;

    void Start()
    {
        translator = FindObjectOfType<Translator>();
        player = FindObjectOfType<Player>();
        player.LoadPlayer();
        navigator = FindObjectOfType<Navigator>();

        levelStatus = FindObjectOfType<LevelStatus>();

        translator.SetYouBeatText(Random.Range(0, 100));

        SetScores();
    }

    #region Public Methods
    public void ClickHomeButton()
    {
        // Save all data and Load main scene
        if (player.gameMode == GameModes.classical)
        {
            if (player.classicalHighScore < levelStatus.totalScore)
            {
                player.classicalHighScore = levelStatus.totalScore;
                player.SavePlayer();
            }
        } else if (player.gameMode == GameModes.random)
        {
            if(player.randomHighScore < levelStatus.totalScore)
            {
                player.randomHighScore = levelStatus.totalScore;
                player.SavePlayer();
            }
        }

        navigator.LoadMainScene();
    }

    public void ClickPlayAgainButton()
    {
        navigator.LoadGameScene();
    }

    public void ClickExtraTime()
    {
        // Add extra time and close game over canvas
        levelStatus.AddSeconds(5);
        CloseGameOverWindow();
    }

    public void ClickShareButton()
    {
        // Open share options
    }

    public void ClickRateButton()
    {
        // Open rate the game page
    }
    #endregion

    #region Private Methods
    void CloseGameOverWindow()
    {
        gameOverCanvas.SetActive(false);
        levelCanvas.SetActive(true);
        palette.SetActive(true);
    }

    public void SetScores()
    {
        if (player == null)
        {
            player = FindObjectOfType<Player>();
            player.LoadPlayer();
        }

        if (levelStatus == null)
        {
            levelStatus = FindObjectOfType<LevelStatus>();
        }

        scoreText.text = levelStatus.totalScore.ToString();

        if (player.gameMode == GameModes.classical)
        {
            if (levelStatus.totalScore > player.classicalHighScore)
            {
                bestText.text = levelStatus.totalScore.ToString();
            } else
            {
                bestText.text = player.classicalHighScore.ToString();
            }
        } else if (player.gameMode == GameModes.random)
        {
            if (levelStatus.totalScore > player.randomHighScore)
            {
                bestText.text = levelStatus.totalScore.ToString();
            }
            else
            {
                bestText.text = player.randomHighScore.ToString();
            }
        }
    }
    #endregion
}
