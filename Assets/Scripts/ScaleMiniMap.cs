using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleMiniMap : MonoBehaviour
{
    public static bool MiniMapBig = false; //Can be used for sound change
    public Image MiniMapBorder;
    public RawImage MiniMap;
    public Camera MiniMapCamera;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (MiniMapBig)
            {
                MakeMapSmall();
            }
            else
            {
                MakeMapBig();
            }
        }
    }
    void MakeMapSmall()
    {
        MiniMapBorder.rectTransform.sizeDelta = new Vector2(100, 100);
        MiniMap.rectTransform.sizeDelta = new Vector2(98, 98);
        MiniMapCamera.orthographicSize = 11;
        MiniMapBig = false;
    }

    void MakeMapBig()
    {
        MiniMapBorder.rectTransform.sizeDelta = new Vector2(300, 300);
        MiniMap.rectTransform.sizeDelta = new Vector2(298, 298);
        MiniMapCamera.orthographicSize = 30;
        MiniMapBig = true;
    }
}
