using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


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

    [Header("Screens")] 
    public GameObject levelUpScreen;
    
    public bool choosingUpgrade = false;

    private PlayerStats playerStats;
    private Health playerHealth;
    private UIPlayerStatus uiPlayerStatus;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        //InitializePlayerStats();
    }   
    
    // Game Management functions
    public void DestroySelf()
    {
        Destroy(gameObject);
        Instance = null;
    }

    public void InitializePlayerStats()
    {
        GetLevelUpScreen();
        GetUIPlayerStats();
        DisableScreens();
        StartTimer();
        GameObject playerGameObject = GameObject.FindWithTag("PlayerContainer");
        playerHealth = playerGameObject.GetComponentInChildren<Health>();
        if (playerGameObject != null)
        {
            playerStats = playerGameObject.GetComponent<PlayerStats>();
            if (playerStats == null)
            {
                Debug.LogError("PlayerStats component not found on Player GameObject");
            }
        }
        else
        {
            Debug.LogError("Player GameObject not found");
        }
        
    }

    private void GetLevelUpScreen()
    {
        levelUpScreen = GameObject.FindWithTag("LevelUpScreen");
        levelUpScreen.SetActive(false);
    }

    private void GetUIPlayerStats()
    {
        uiPlayerStatus = GameObject.FindWithTag("PlayerStatusScreen").GetComponent<UIPlayerStatus>();
    }

    public PlayerStats GetPlayerStats()
    {
        return playerStats;
    }
    
    void DisableScreens()       
    {
        levelUpScreen.SetActive(false);
        uiPlayerStatus.gameObject.SetActive(false);
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

    public TimeSpan GetTime()
    {
        return currentTime;
    }

    public void StartLevelUp()
    {
        uiPlayerStatus.UpdatePlayerStats();
        choosingUpgrade = true;
        PauseGame();
        playerHealth.Heal(15f);
        List<UpgradeManager.UpgradeOption> options = UpgradeManager.Instance.GetRandomUpgrades(4);
        
        LevelUpScreen levelUpScript = levelUpScreen.GetComponent<LevelUpScreen>();

        if (levelUpScript)
        {
            levelUpScript.SetupUpgradeOptions(options);
        }
        
        levelUpScreen.SetActive(true);
        uiPlayerStatus.gameObject.SetActive(true);
    }
    
    public void EndLevelUp()
    {
        choosingUpgrade = false;
        ResumeGame();
        levelUpScreen.SetActive(false);
        uiPlayerStatus.gameObject.SetActive(false);
    }
    
}
