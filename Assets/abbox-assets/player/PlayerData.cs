using System;
using static GlobalVariables;

[Serializable]
public class PlayerData
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

    public PlayerData(Player player)
    {
        coins = player.coins;
        diamonds = player.diamonds;
        playerName = player.playerName;
        playerCreated = player.playerCreated;
        nameChanged = player.nameChanged;
        privacyPolicyAccepted = player.privacyPolicyAccepted;
        privacyPolicyDeclined = player.privacyPolicyDeclined;
        language = player.language;
        gameMode = player.gameMode;
        sounds = player.sounds;
        haptics = player.haptics;
    }
}
