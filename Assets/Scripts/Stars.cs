using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stars : MonoBehaviour
{
    public Image Star1;
    public Image Star2;
    public Image Star3;

    void OnEnable()
    {
        Debug.Log(Health.currentHealth);
        Debug.Log(Timer.currentTime);

        if (Timer.currentTime <= 0)
        {
            Star1.color = new Color32(255, 255, 225, 255);
        }
        if (Player.starpoint >= 2)
        {
            Star2.color = new Color32(255, 255, 225, 255);
        }
        if (Player.score >= 500)
        {
            Star3.color = new Color32(255, 255, 225, 255);
        }
    }
}
