using System;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static Action PauseGame;
    public static Action ResumeGame;

    private void Start()
    {
        PauseGame += OnPauseGame;
        ResumeGame += OnResumeGame;
    }

    void OnPauseGame()
    {
        Time.timeScale = 0f;

        // Pause all audio
        AudioListener.pause = true;
    }

    void OnResumeGame()
    {
        Time.timeScale = 1f;

        // Resume all audio
        AudioListener.pause = false;
    }
}
