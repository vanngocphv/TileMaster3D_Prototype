using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class ProgressInfoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI CurrentProgressLevel;

    private void Awake()
    {
        
        string loadPlayerData = File.ReadAllText(Application.persistentDataPath + "/" + "PlayerData.json");
        PlayerData playerData = JsonUtility.FromJson<PlayerData>(loadPlayerData);
        CurrentProgressLevel.text = playerData.Process.ToString();
    }
}
