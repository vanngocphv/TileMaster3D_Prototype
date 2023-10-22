using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MenuUI
{
    [SerializeField] private Button MenuButton;
    [SerializeField] private Button RetryButton;
    // Start is called before the first frame update
    void Start()
    {
        MenuButton.onClick.AddListener(OnClickMenuButton_Trigger);
        RetryButton.onClick.AddListener(OnClickRetryButton_Trigger);
    }

    // Update is called once per frame
    void Update()
    {

    }


    // Event
    private void OnClickRetryButton_Trigger()
    {
        GameManager.Instance.PlaySFX();
        SceneManager.LoadScene(1);
    }
}
