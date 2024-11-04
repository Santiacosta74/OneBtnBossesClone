using UnityEngine;
using TMPro;
using System;

public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText; 
    private float elapsedTime = 0f; 
    private bool isRunning = true; 

    void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerDisplay();
        }
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60); 
        int seconds = Mathf.FloorToInt(elapsedTime % 60); 
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds); 
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    internal float GetElapsedTime()
    {
        throw new NotImplementedException();
    }
}
