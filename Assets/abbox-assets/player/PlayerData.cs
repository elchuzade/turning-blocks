using System;
using static GlobalVariables;

[Serializable]
public class PlayerData
{
    public string playerName = "";
    public bool playerCreated = false;
    public bool nameChanged = false;
    public bool privacyPolicyAccepted = false;
    public bool privacyPolicyDeclined = false;
    public Languages language = Languages.english;
    public bool sounds = false;
    public bool haptics = false;

    public PlayerData(Player player)
    {
        playerName = player.playerName;
        playerCreated = player.playerCreated;
        nameChanged = player.nameChanged;
        privacyPolicyAccepted = player.privacyPolicyAccepted;
        privacyPolicyDeclined = player.privacyPolicyDeclined;
        language = player.language;
        sounds = player.sounds;
        haptics = player.haptics;
    }
}
