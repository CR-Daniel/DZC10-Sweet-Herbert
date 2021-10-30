using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stars_Hard : MonoBehaviour
{
    public Image Star1_H;
    public Image Star2_H;
    public Image Star3_H;

    void OnEnable()
    {
        if (Health.currentHealth == 3)
        {
            Star1_H.color = new Color32(255, 255, 225, 255);
        }
        if (Player.starpoint_hard >= 2)
        {
            Star2_H.color = new Color32(255, 255, 225, 255);
        }
        if (Player.score >= 800)
        {
            Star3_H.color = new Color32(255, 255, 225, 255);
        }
    }
}
