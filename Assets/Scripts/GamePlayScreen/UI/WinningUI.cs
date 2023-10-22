using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinningUI : MenuUI
{
    [SerializeField] private Button MenuButton;
    [SerializeField] private Button NextButton;
    // Start is called before the first frame update
    void Start()
    {
        MenuButton.onClick.AddListener(OnClickMenuButton_Trigger);
        NextButton.onClick.AddListener(OnClickNextButton_Trigger);
    }

    // Update is called once per frame
    void Update()
    {

    }


    // Event
    private void OnClickNextButton_Trigger()
    {
        GameManager.Instance.PlaySFX();
        SceneManager.LoadScene(1);
    }

}
