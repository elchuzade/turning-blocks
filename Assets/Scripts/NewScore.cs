using UnityEngine;
using UnityEngine.UI;

public class NewScore : MonoBehaviour
{
    [SerializeField] Text score;

    int colorAlpha = 255;

    void Update()
    {
        transform.position += new Vector3(0, 2, 0);
        score.color = new Color32(255, 255, 255, (byte)colorAlpha);
        colorAlpha -= 2;
    }

    public void SetScore(int _score) {
        score.text = _score.ToString();
        Destroy(gameObject, 4);
    }
}
