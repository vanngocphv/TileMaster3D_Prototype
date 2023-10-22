using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MenuUI
{
    [SerializeField] private Button MenuButton;
    [SerializeField] private Button ContinueButton;
    [SerializeField] private Button BGMButton;
    [SerializeField] private Button SFXButton;

    [SerializeField] private GameObject OffBGM;
    [SerializeField] private GameObject OffSFX;

    private SettingData settingData;
    private void OnEnable()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        MenuButton.onClick.AddListener(OnClickMenuButton_Trigger);
        ContinueButton.onClick.AddListener(OnClickContinueButton_Trigger);
        BGMButton.onClick.AddListener(OnClickBGMButton);
        SFXButton.onClick.AddListener(OnClickSFXButton);

        // Setting data and active/deactive button
        settingData = GameManager.Instance.GetSettingData();
        OffBGM.SetActive(!settingData.BGM);
        OffSFX.SetActive(!settingData.SFX);
    }



    // Event
    private void OnClickContinueButton_Trigger()
    {
        GameManager.Instance.PlaySFX();
        HideGameObject();
    }

    private void OnClickBGMButton()
    {
        GameManager.Instance.PlaySFX();
        settingData.BGM = !settingData.BGM;
        GameManager.Instance.SetBGM(settingData.BGM);
        GameManager.Instance.UpdateSettingData(settingData);
        OffBGM.SetActive(!settingData.BGM);
    }
    private void OnClickSFXButton()
    {
        settingData.SFX = !settingData.SFX;
        GameManager.Instance.SetSFX(settingData.SFX);
        GameManager.Instance.UpdateSettingData(settingData);
        OffSFX.SetActive(!settingData.SFX);
    }

}
