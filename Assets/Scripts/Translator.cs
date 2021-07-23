using System;
using UnityEngine;
using UnityEngine.UI;
using static GlobalVariables;

public class Translator : MonoBehaviour
{
    string youBeatEnglish = "you beat {term_one}%\nof all players";
    string youBeatTurkish = "tüm oyuncularin\n%{term_one} yendin";
    string youBeatSpanish = "ence al {term_one}% de todas\nlas jugadoras";
    string youBeatFrench = "vous battez {term_one}%\nde tous les joueurs";
    string youBeatGerman = "du hast {term_one}% aller\nspieler geschlagen";

    Player player;

    // For language buttons
    [SerializeField] TextButton[] allTextButtons;

    [SerializeField] GameObject classicalBackground;
    [SerializeField] GameObject infiniteBackground;
    [SerializeField] GameObject randomBackground;

    [SerializeField] GameObject classicalMode;
    [SerializeField] GameObject infiniteMode;
    [SerializeField] GameObject randomMode;
    
    [SerializeField] Text youBeatText;
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

    public void SetYouBeatText(int percentage)
    {
        if (player == null)
        {
            player = FindObjectOfType<Player>();
            player.LoadPlayer();
        }
        
        string youBeatModifiedText = "";

        switch (player.language)
        {
            case Languages.english:
                youBeatModifiedText = youBeatEnglish;
                break;
            case Languages.turkish:
                youBeatModifiedText = youBeatTurkish;
                break;
            case Languages.spanish:
                youBeatModifiedText = youBeatSpanish;
                break;
            case Languages.french:
                youBeatModifiedText = youBeatFrench;
                break;
            case Languages.german:
                youBeatModifiedText = youBeatGerman;
                break;
        }

        youBeatModifiedText = youBeatModifiedText.Replace("{term_one}", percentage.ToString());
        youBeatText.text = youBeatModifiedText;
    }
}
