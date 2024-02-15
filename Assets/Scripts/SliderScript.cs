using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SliderScript : MonoBehaviour
{
    public AudioSource audioSource;
    public Slider slider;
    public ButtonManager buttonManager;
    public GameObject gameOverPanel;

    [SerializeField] private float _currentTime = 0;
    //private void Start() => slider.maxValue = audioSource.clip.length;

    private void Update()
    {
        if (!buttonManager.isPaused)
        {
            slider.maxValue = audioSource.clip.length;
            audioSource.volume = 0.2f;
            _currentTime = audioSource.time;
            slider.value = _currentTime;

            if (slider.normalizedValue >= 1f)
            {
                gameOverPanel.SetActive(true);
                Time.timeScale = 0f;
                buttonManager.isPaused = true;
            }

        }
        else
        {
            audioSource.volume = 0f;
            audioSource.time = slider.value;
        }
    }
}