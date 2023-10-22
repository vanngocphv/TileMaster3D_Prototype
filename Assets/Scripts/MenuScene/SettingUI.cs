using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    [SerializeField] private Game GameObj;

    [SerializeField] private GameObject OffBGM;
    [SerializeField] private GameObject OffSFX;

    [SerializeField] private Button BGMButton;
    [SerializeField] private Button SFXButton;
    [SerializeField] private Button CloseButton;

    private SettingData settingData;
    private void OnEnable()
    {
        settingData = GameObj.GetSettingData();

        if (!settingData.BGM) OffBGM.SetActive(true);
        else OffBGM.SetActive(false);

        if (!settingData.SFX) OffSFX.SetActive(true);
        else OffSFX.SetActive(false);
    }

    private void Start()
    {
        CloseButton.onClick.AddListener(OnCloseButtonClicked);
        BGMButton.onClick.AddListener(OnBGMButtonClicked);
        SFXButton.onClick.AddListener(OnSFXButtonClicked);
    }

    private void OnCloseButtonClicked()
    {
        GameObj.PlaySFX();
        this.gameObject.SetActive(false);
    }
    private void OnBGMButtonClicked()
    {
        settingData.BGM = !settingData.BGM;
        if (!settingData.BGM) OffBGM.SetActive(true);
        else OffBGM.SetActive(false);
        GameObj.PlaySFX();                                  // Play Audio
        // Update Game Object data
        GameObj.UpdateBGM(settingData.BGM);
        GameObj.UpdateSettingData(settingData);
    }
    private void OnSFXButtonClicked()
    {
        settingData.SFX = !settingData.SFX;
        if (!settingData.SFX) OffSFX.SetActive(true);
        else OffSFX.SetActive(false);

        // Update Game Object data
        GameObj.UpdateSFX(settingData.SFX);
        GameObj.UpdateSettingData(settingData);
    }
}
