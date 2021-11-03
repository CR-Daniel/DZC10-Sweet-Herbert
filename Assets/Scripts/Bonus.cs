 using UnityEngine;
 using UnityEngine.UI;
 using System.Collections;
 
 public class Bonus : MonoBehaviour {
 
     public Text txt;
 
     void Start () {
         txt = GetComponent<Text>(); 
     }
     
     void Update () {
         txt.text = "Bonus: Sell Ice Cream to a " + Player.objective + "!";
    }
 }
