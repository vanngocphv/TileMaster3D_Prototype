using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    public void HideGameObject()
    {
        Time.timeScale = 1;
        GameManager.Instance.ContinueClick();
        this.gameObject.SetActive(false);
    }
    public void ShowGameObject()
    {
        Time.timeScale = 0;
        GameManager.Instance.StopClick();
        this.gameObject.SetActive(true);
    }

    public void OnClickMenuButton_Trigger()
    {
        GameManager.Instance.PlaySFX();
        SceneManager.LoadScene(0);
    }
}
