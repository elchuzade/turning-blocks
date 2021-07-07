using UnityEngine;
using static GlobalVariables;

public class Player : MonoBehaviour
{
    public int coins = 0;
    public int diamonds = 0;
    public string playerName = "";
    public bool playerCreated = false;
    public bool nameChanged = false;
    public bool privacyPolicyAccepted = false;
    public bool privacyPolicyDeclined = false;
    public Languages language = Languages.english;
    public GameModes gameMode = GameModes.classical;
    public bool sounds = false;
    public bool haptics = false;

    void Awake()
    {
        transform.SetParent(transform.parent.parent);
        // Singleton
        int instances = FindObjectsOfType<Player>().Length;
        if (instances > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void ResetPlayer()
    {
        coins = 0;
        diamonds = 0;
        playerName = "";
        playerCreated = false;
        nameChanged = false;
        privacyPolicyAccepted = false;
        privacyPolicyDeclined = false;
        language = Languages.english;
        gameMode = GameModes.classical;
        sounds = false;
        haptics = false;

        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if (data == null)
        {
            ResetPlayer();
            data = SaveSystem.LoadPlayer();
        }

        coins = data.coins;
        diamonds = data.diamonds;
        playerName = data.playerName;
        playerCreated = data.playerCreated;
        nameChanged = data.nameChanged;
        privacyPolicyAccepted = data.privacyPolicyAccepted;
        privacyPolicyDeclined = data.privacyPolicyDeclined;
        language = data.language;
        gameMode = data.gameMode;
        sounds = data.sounds;
        haptics = data.haptics;
    }
}
