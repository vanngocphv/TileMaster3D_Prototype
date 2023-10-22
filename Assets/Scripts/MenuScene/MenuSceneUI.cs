using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSceneUI : MonoBehaviour
{
    [SerializeField] private Button PlayBtn;
    [SerializeField] private Button QuitBtn;
    [SerializeField] private Button SettingBtn;

    [SerializeField] private GameObject SettingUI;
    [SerializeField] private Game GameObj;

    [SerializeField] private string FileName = "PlayerData.json";

    PlayerData playerData;
    string saveFilePath;

    // Start is called before the first frame update
    void Start()
    {
        SettingUI.SetActive(false);

        playerData = new PlayerData();
        playerData.Process = 0;

        saveFilePath = Application.persistentDataPath + "/" + FileName;

        PlayBtn.onClick.AddListener(OnClickPlayBtn);
        SettingBtn.onClick.AddListener(OnClickSettingBtn);
        QuitBtn.onClick.AddListener(OnClickQuitBtn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnClickPlayBtn()
    {
        // Play, start the game
        GameObj.PlaySFX();
        SceneManager.LoadScene(1);

    }
    private void OnClickSettingBtn()
    {
        GameObj.PlaySFX();
        SettingUI.SetActive(true);
    }
    private void OnClickQuitBtn()
    {
        GameObj.PlaySFX();
        Application.Quit();
    }
}
