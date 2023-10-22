using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private string PlayerDataFileName = "PlayerData.json";
    [SerializeField] private string SettingDataFileName = "SettingData.json";
    [SerializeField] private BGMManager bgm;
    [SerializeField] private SFXManager sfx;

    PlayerData playerData;
    SettingData settingData;
    string savePlayerDataFilePath;
    string saveSettingDataFilePath;


    private void OnEnable()
    {
        savePlayerDataFilePath = Application.persistentDataPath + "/" + PlayerDataFileName;
        saveSettingDataFilePath = Application.persistentDataPath + "/" + SettingDataFileName;

        if (!File.Exists(savePlayerDataFilePath))
        {
            playerData = new PlayerData();
            playerData.Process = 0;

            // When the file doesn't exist, create new
            string savePlayerData = JsonUtility.ToJson(playerData);
            File.WriteAllText(savePlayerDataFilePath, savePlayerData);
        }

        if (!File.Exists(saveSettingDataFilePath))
        {
            settingData = new SettingData();
            settingData.BGM = true;
            settingData.SFX = true;

            // When the file doesn't exist, create new
            string saveSettingData = JsonUtility.ToJson(settingData);
            File.WriteAllText(saveSettingDataFilePath, saveSettingData);
        }
    }

    private void Start()
    {
        UpdateBGM(GetSettingData().BGM);
        UpdateSFX(GetSettingData().SFX);
    }

    // Player Data
    public PlayerData GetPlayerData()
    {
        string loadPlayerData = File.ReadAllText(savePlayerDataFilePath);
        playerData = JsonUtility.FromJson<PlayerData>(loadPlayerData);

        return playerData;
    }
    public void UpdatePlayerData(PlayerData playerData)
    {
        string savePlayerData = JsonUtility.ToJson(playerData);
        File.WriteAllText(savePlayerDataFilePath, savePlayerData);
    }

    // Setting Data
    public SettingData GetSettingData()
    {
        string loadSettingData = File.ReadAllText(saveSettingDataFilePath);
        settingData = JsonUtility.FromJson<SettingData>(loadSettingData);

        return settingData;
    }
    public void UpdateSettingData(SettingData settingData)
    {
        string saveSettingData = JsonUtility.ToJson(settingData);
        File.WriteAllText(saveSettingDataFilePath, saveSettingData);
    }

    // Update BGM + SFX
    public void UpdateBGM(bool isActive)
    {
        bgm.SetAudioSrc(isActive);
    }
    public void UpdateSFX(bool isActive)
    {
        sfx.SetAudioSrc(isActive);
    }

    // Play SFX
    public void PlaySFX()
    {
        sfx.PlayAudioClip();
    }

}
