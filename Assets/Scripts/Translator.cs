using UnityEngine;
using UnityEngine.UI;
using static GlobalVariables;

public class Translator : MonoBehaviour
{
    Player player;

    // For language buttons
    [SerializeField] TextButton[] allTextButtons;

    [SerializeField] GameObject classicalBackground;
    [SerializeField] GameObject infiniteBackground;
    [SerializeField] GameObject randomBackground;

    [SerializeField] GameObject classicalMode;
    [SerializeField] GameObject infiniteMode;
    [SerializeField] GameObject randomMode;

    [SerializeField] Text highScore;

    void Start()
    {
        player = FindObjectOfType<Player>();
        player.LoadPlayer();

        SetLanguage(player.language);
        SetGameMode(player.gameMode);
    }

    public void SetGameMode(GameModes gameMode)
    {
        classicalMode.SetActive(false);
        infiniteMode.SetActive(false);
        randomMode.SetActive(false);

        classicalBackground.SetActive(false);
        infiniteBackground.SetActive(false);
        randomBackground.SetActive(false);

        switch (gameMode)
        {
            case GameModes.classical:
                classicalBackground.SetActive(true);
                classicalMode.SetActive(true);
                // Game Scene does not hold high score
                if (highScore != null)
                {
                    highScore.text = player.classicalHighScore.ToString();
                }
                break;
            case GameModes.infinite:
                infiniteBackground.SetActive(true);
                infiniteMode.SetActive(true);
                if (highScore != null)
                {
                    highScore.text = player.infiniteHighScore.ToString();
                }
                break;
            case GameModes.random:
                randomBackground.SetActive(true);
                randomMode.SetActive(true);
                if (highScore != null)
                {
                    highScore.text = player.randomHighScore.ToString();
                }
                break;
        }
    }

    public void SetLanguage(Languages language)
    {
        for (int i = 0; i < allTextButtons.Length; i++)
        {
            allTextButtons[i].SetButtonLanguage(language);
        }
    }
}
