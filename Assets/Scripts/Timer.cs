using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text timerText;
    [SerializeField] private float GameLength;
    [SerializeField] private GameObject GameUI;
    [SerializeField] private GameObject GameOverUI;
    public static float currentTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = GameLength;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;

        string minutes = ((int)currentTime / 60).ToString();
        string seconds = (currentTime % 60).ToString("f0");
        int sec = Convert.ToInt32(seconds);
        if (sec < 10)
        {
            seconds = "0" + seconds;
        }
        timerText.text = minutes + ":" + seconds;

        if (currentTime <= 0)
        {
            Time.timeScale = 0f;
            GameUI.SetActive(false);
            GameOverUI.SetActive(true);
        }
    }
}
