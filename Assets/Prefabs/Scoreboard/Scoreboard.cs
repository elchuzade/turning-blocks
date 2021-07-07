using UnityEngine.UI;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] Text coinsCount;
    [SerializeField] Text diamondsCount;

    [SerializeField] GameObject coinsIcon;
    [SerializeField] GameObject diamondsIcon;

    #region Public Methods
    public void SetCoins(int coins, bool animate = false)
    {
        coinsCount.text = coins.ToString();
        if (animate)
        {
            coinsIcon.GetComponent<AnimationTrigger>().Trigger("Start");
        }
    }

    public void SetDiamonds(int diamonds, bool animate = false)
    {
        diamondsCount.text = diamonds.ToString();
        if (animate)
        {
            diamondsIcon.GetComponent<AnimationTrigger>().Trigger("Start");
        }
    }
    #endregion
}
