using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TimeLeftText;

    private float totalTime;
    private int previousTime;

    private void Start()
    {
        totalTime = GameManager.Instance.totalMaps.AllMap[GameManager.Instance.currentLevel].TotalTime;
        previousTime = (int)totalTime;
        TimeLeftText.text = previousTime.ToString() + "s";
    }

    private void Update()
    {
        if (totalTime >= 0)
        {
            if (Mathf.CeilToInt(totalTime) != previousTime ) 
            {
                previousTime = Mathf.CeilToInt(totalTime);
                TimeLeftText.text = previousTime.ToString() + "s";
            }
        }
        totalTime -= Time.deltaTime;
        if (totalTime <= 0 )
        {
            TimeLeftText.text = "0s";
            // Invoke GameOver;
            GameManager.Instance.GameOver();
        }
    }
}
