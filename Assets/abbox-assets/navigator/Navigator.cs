using UnityEngine;
using UnityEngine.SceneManagement;

public class Navigator : MonoBehaviour
{
    public void LoadGameScene()
    {

    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void LoadShopScene()
    {
        SceneManager.LoadScene("ShopScene");
    }

    public void LoadChestRoomScene()
    {
        SceneManager.LoadScene("ChestRoomScene");
    }

    public void LoadLeaderboardScene()
    {
        SceneManager.LoadScene("LeaderboardScene");
    }
}
