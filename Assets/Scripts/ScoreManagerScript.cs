using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManagerScript : MonoBehaviour
{
    public int _score = 0;
    public int _combo = 0;
    public float _comboTimer = 2f;
    public float _currentComboTime = 0f;
    public TextMeshProUGUI[] scoreText;
    public TextMeshProUGUI comboText;
    public TextMeshProUGUI[] bestScoreText;
    public TextMeshProUGUI rateText;
    public TMP_Dropdown musicChoice;

    public void Start()
    {
        GetBestScore();

        foreach (TextMeshProUGUI textS in scoreText)
        {
            textS.text = "Score: " + _score;
        }
        comboText.text = "Combo: " + _combo;
    }

    public void Update()
    {
        foreach (TextMeshProUGUI textS in scoreText)
        {
            textS.text = "Score: " + _score;
        }
        comboText.text = "Combo: " + _combo;

        if (_combo > 0)
        {
            _currentComboTime -= Time.deltaTime;
            if (_currentComboTime <= 0)
            {
                _combo--;
                comboText.text = "Combo: " + _combo;
                _currentComboTime = _comboTimer;
            }
        }

        if (musicChoice.value == 0)
        {
            if (PlayerPrefs.GetInt("TFR_Unity") < _score)
            {
                PlayerPrefs.SetInt("TFR_Unity", _score);
                PlayerPrefs.Save();
            }
            bestScoreText[0].text = "Best score: " + PlayerPrefs.GetInt("TFR_Unity");
        }
        else
        {
            if (PlayerPrefs.GetInt("BeatSaber") < _score)
            {
                PlayerPrefs.SetInt("BeatSaber", _score);
                PlayerPrefs.Save();
            }
            bestScoreText[0].text = "Best score: " + PlayerPrefs.GetInt("BeatSaber");
        }

        rateText.text = "Rate: " + GetRate(_score);
    }

    public void AddScore(int amount)
    {
        foreach (TextMeshProUGUI textS in scoreText)
        {
            _score += amount * (_combo + 1);
            textS.text = "Score: " + _score;
        }
    }

    public void IncreaseCombo()
    {
        _combo++;
        comboText.text = "Combo: " + _combo;
        _currentComboTime = _comboTimer;
    }

    public void RemoveCombo()
    {
        _combo = 0;
        _currentComboTime = 0f;
    }

    public int GetRate(int score)
    {
        int rate = 0;

        if (score >= 0 && score < 500)
        {
            rate = 1;
        }
        else if (score >= 500 && score < 1000)
        {
            rate = 2;
        }
        else if (score >= 1000 && score < 2000)
        {
            rate = 3;
        }
        else if (score >= 2000 && score < 3000)
        {
            rate = 4;
        }
        else
        {
            rate = 5;
        }

        return rate;
    }

    public void GetBestScore()
    {
        Debug.Log(PlayerPrefs.GetInt("TFR_Unity") + " è " + PlayerPrefs.GetInt("BeatSaber"));
        bestScoreText[0].text = "Best score: " + PlayerPrefs.GetInt("TFR_Unity");
        bestScoreText[1].text = "Best score: " + PlayerPrefs.GetInt("BeatSaber");
    }
}
