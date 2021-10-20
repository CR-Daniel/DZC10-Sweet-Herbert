 using UnityEngine;
 using UnityEngine.UI;
 using System.Collections;
 
 public class ScoreCounter : MonoBehaviour {
 
     public Text txt;
 
     void Start () {
         txt = GetComponent<Text>(); 
         txt.text = "Score : 0";
     }
     
     void Update () {
         txt.text = "Score : " + Player.score;
         Debug.Log(txt.text);
    }
 }
