using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // singleton for get data
    public static GameManager Instance { get; private set; }

    // fire event
    public event EventHandler OnGameOver;
    public event EventHandler OnGameWinning;

    // for Tile Object
    public TotalMap_SBO totalMaps;
    public int currentLevel;

    // For BGM + SFX
    public BGMManager bgmObj;
    public SFXManager sfxObj;

    // PRIVATE DATA
    private bool isClickEnable = true;

    // Read file from cache
    private string filePath;
    private string PlayerDataFileName = "PlayerData.json";
    private string SettingDataFileName = "SettingData.json";
    private PlayerData playerData;
    private SettingData settingData;


    private void Awake()
    {
        playerData = GetPlayerData();
        settingData = GetSettingData();
        currentLevel = playerData.Process;

        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetBGM(settingData.BGM);
        SetSFX(settingData.SFX);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
    }

    public int CurrentLevelPoint()
    {
        return totalMaps.AllMap[currentLevel].TotalTiles;
    }


    // Get JSON data
    public PlayerData GetPlayerData()
    {
        string loadPlayerData = File.ReadAllText(Application.persistentDataPath + "/" + PlayerDataFileName);
        playerData = JsonUtility.FromJson<PlayerData>(loadPlayerData);

        return playerData;
    }
    public SettingData GetSettingData()
    {
        string loadSettingData = File.ReadAllText(Application.persistentDataPath + "/" + SettingDataFileName);
        settingData = JsonUtility.FromJson<SettingData>(loadSettingData);

        return settingData;
    }

    // Update JSON data
    public void UpdatePlayerData(PlayerData playerData)
    {
        string savePlayerData = JsonUtility.ToJson(playerData);
        File.WriteAllText(Application.persistentDataPath + "/" + PlayerDataFileName, savePlayerData);
    }
    public void UpdateSettingData(SettingData settingData)
    {
        string saveSettingData = JsonUtility.ToJson(settingData);
        File.WriteAllText(Application.persistentDataPath + "/" + SettingDataFileName, saveSettingData);
    }


    // GameWinning
    public void GameWinning()
    {
        // Write/update json data

        playerData.Process = currentLevel + 1 >= totalMaps.AllMap.Count ? currentLevel : currentLevel + 1;

        UpdatePlayerData(playerData);
        OnGameWinning?.Invoke(this, EventArgs.Empty);

    }

    // GameOver
    public void GameOver()
    {
        isClickEnable = false;
        OnGameOver?.Invoke(this, EventArgs.Empty);
    }

    // Stop clickable
    public void StopClick()
    {
        isClickEnable = false;
    }
    public void ContinueClick()
    {
        isClickEnable = true;
    }
    public bool IsClickEnable()
    {
        return isClickEnable;
    }

    // Set BGM
    public void SetBGM(bool isActive)
    {
        bgmObj.SetAudioSrc(isActive);
    }
    public void SetSFX(bool isActive)
    {
        sfxObj.SetAudioSrc(isActive);
    }


    // Play SFX
    public void PlaySFX()
    {
        sfxObj.PlayAudioClip();
    }

}
