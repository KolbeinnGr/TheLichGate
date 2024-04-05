using UnityEngine;
using System;
using System.Collections;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [Header("Game Manager")]
    public bool isGamePaused = false;
    
    // World Timer 
    public event EventHandler<TimeSpan> WorldTimeChanged;
    private TimeSpan currentTime;
    private bool timerActive = false;
    private Coroutine timerCoroutine;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            StartTimer();
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    
    // Game Management functions
    public void DestroySelf()
    {
        Destroy(gameObject);
        Instance = null;
    }
    
    
    public void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0f;
    }
    
    public void ResumeGame()
    {
        isGamePaused = false;
        Time.timeScale = 1f;
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
    
    // World Timer functions
    private IEnumerator TimerCoroutine()
    {
        while (timerActive)
        {
            currentTime = currentTime.Add(TimeSpan.FromSeconds(1));
            WorldTimeChanged?.Invoke(this, currentTime);
            yield return new WaitForSeconds(1);
        }
    }
    
    public void StartTimer()
    {
        timerActive = true;
        
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
        }
        
        timerCoroutine = StartCoroutine(TimerCoroutine());
    }
    public void StopTimer()
    {
        timerActive = false;
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
        }
    }

    public void ResetTimer()
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
        }
        currentTime = TimeSpan.Zero;
        WorldTimeChanged?.Invoke(this, currentTime);
        if (timerActive)
        {
            timerCoroutine = StartCoroutine(TimerCoroutine());
        }
    }
    
}
