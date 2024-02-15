using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;
using TMPro;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public TMP_Dropdown Dropdown;
    public AudioSource audioSource;
    public AudioClip audioClip1;
    public AudioClip audioClip2;
    public GameObject panel;
    public GameObject choicePanel;
    public GameObject exitChoicePanel;
    public bool isPaused = false;
    public bool isSpawn = false;

    public void StartGame()
    {
        Time.timeScale = 0.0f;
        SceneManager.LoadScene(1);
    }

    public void ContinueGame()
    {
        isPaused = false;
        panel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void PauseGame()
    {
        isPaused = true;
        panel.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void MainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }

    public void OpenEditor()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(2);
    }

    public void SongChoise()
    {
        isSpawn = true;
        switch (Dropdown.value)
        {
            case 0:
                audioSource.clip = audioClip1;
                break;
            case 1:
                audioSource.clip = audioClip2;
                break;
            default:
                Debug.Log("Что-то пошло не так, эм, ладно...");
                break;
        }
        audioSource.time = 0.0f;
        audioSource.Play();
        if (choicePanel != null)
        {
            choicePanel.SetActive(false);
        }
    }

    public void OpenRecords()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(3);
    }

    public void Exit()
    {
        exitChoicePanel.SetActive(true);
    }

    public void FinallyQuit()
    {
        Application.Quit();
    }
}