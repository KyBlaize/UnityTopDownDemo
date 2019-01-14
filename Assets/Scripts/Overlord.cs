using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Overlord : MonoBehaviour
{
    //This script contains the game's timer and score tracking system
    int finalscore = 0;
    
    [SerializeField]
    Text targetText;
    [HideInInspector]
    public bool StartTimer = false;
    public float Currenttime = 0; //the player's current time. Timer starts when the player enters the zone and stops when they leave

    private void Update()
    {
        if (StartTimer)
        {
            Currenttime += Time.deltaTime;
            targetText.text = "Current Time: "+Currenttime.ToString();
        }
    }
}
