using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject GameOverUI;
    [SerializeField] private GameObject GamePauseUI;
    [SerializeField] private GameObject WinningUI;
    [SerializeField] private Button PauseButton;

    // Text UI
    [SerializeField] private TextMeshProUGUI CurrentLevel;

    // Start is called before the first frame update
    void Start()
    {
        // Set level
        CurrentLevel.text = GameManager.Instance.currentLevel.ToString();

        GameManager.Instance.OnGameOver += OnGameOver_Trigger;
        GameManager.Instance.OnGameWinning += OnGameWinning_Trigger;
        PauseButton.onClick.AddListener(OnPauseGame_Trigger);
        InitialSetting();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameOver -= OnGameOver_Trigger;
        GameManager.Instance.OnGameWinning -= OnGameWinning_Trigger;
        Time.timeScale = 1;
    }

    // function declare
    private void InitialSetting()
    {
        GameOverUI.SetActive(false);
        WinningUI.SetActive(false);
        GamePauseUI.SetActive(false);
    }

    // Event declare
    private void OnGameOver_Trigger(object sender, System.EventArgs e)
    {
        Time.timeScale = 0;
        GameOverUI.SetActive(true);
    }
    private void OnGameWinning_Trigger(object sender, System.EventArgs e)
    {
        Time.timeScale = 0;
        WinningUI.SetActive(true);
    }
    private void OnPauseGame_Trigger()
    {
        GameManager.Instance.PlaySFX();
        Time.timeScale = 0;
        GamePauseUI.SetActive(true);
    }
}
