using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    [SerializeField] Slider healthBar;
    [SerializeField] Health playerHealth;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI timerText;
    
    ScoreKeeper scoreKeeper;
    float elapsedTime;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    void Start()
    {
        healthBar.maxValue = playerHealth.GetHealth();
    }
    void Update()
    {
        Timer();
        HealthBar();
        ScoreUpdate();
    }

    
    void HealthBar()
    {
        healthBar.value = playerHealth.GetHealth();
    }

    void ScoreUpdate()
    {
        scoreText.text= scoreKeeper.GetScore().ToString("000000000");
    }

    void Timer()
    {
        elapsedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
