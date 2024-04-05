using UnityEngine;
using System;
using TMPro;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Replace with 'public TextMeshProUGUI timerText;' for TextMeshPro
    // ...

    void Start()
    {
        GameManager.Instance.WorldTimeChanged += OnWorldTimeChanged;
        GameManager.Instance.ResetTimer();
    }

    private void OnWorldTimeChanged(object sender, TimeSpan time)
    {
        timerText.text = string.Format("{0:00}:{1:00}", time.Minutes, time.Seconds);
    }

    void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.WorldTimeChanged -= OnWorldTimeChanged;
        }
    }
}