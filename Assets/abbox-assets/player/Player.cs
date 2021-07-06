using UnityEngine;
using static GlobalVariables;

public class Player : MonoBehaviour
{
    public string playerName = "";
    public bool playerCreated = false;
    public bool nameChanged = false;
    public bool privacyPolicyAccepted = false;
    public bool privacyPolicyDeclined = false;
    public Languages language = Languages.english;
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
        playerName = "";
        playerCreated = false;
        nameChanged = false;
        privacyPolicyAccepted = false;
        privacyPolicyDeclined = false;
        language = Languages.english;
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

        playerName = data.playerName;
        playerCreated = data.playerCreated;
        nameChanged = data.nameChanged;
        privacyPolicyAccepted = data.privacyPolicyAccepted;
        privacyPolicyDeclined = data.privacyPolicyDeclined;
        language = data.language;
        sounds = data.sounds;
        haptics = data.haptics;
    }
}
