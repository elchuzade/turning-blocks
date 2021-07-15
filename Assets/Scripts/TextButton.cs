using UnityEngine.UI;
using UnityEngine;
using static GlobalVariables;

public class TextButton : MonoBehaviour
{
    [SerializeField] Sprite buttonEnglish;
    [SerializeField] Sprite buttonTurkish;
    [SerializeField] Sprite buttonSpanish;
    [SerializeField] Sprite buttonFrench;
    [SerializeField] Sprite buttonGerman;

    Image buttonImage;

    public void SetButtonLanguage(Languages language)
    {
        buttonImage = GetComponent<Image>();

        switch (language)
        {
            case Languages.english:
                buttonImage.sprite = buttonEnglish;
                break;
            case Languages.turkish:
                buttonImage.sprite = buttonTurkish;
                break;
            case Languages.spanish:
                buttonImage.sprite = buttonSpanish;
                break;
            case Languages.french:
                buttonImage.sprite = buttonFrench;
                break;
            case Languages.german:
                buttonImage.sprite = buttonGerman;
                break;
        }
    }
}
