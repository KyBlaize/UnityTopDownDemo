using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Overlord : MonoBehaviour
{
    //This script contains the game's timer and score tracking system
    int finalscore = 0;
    public Image Pointer;
    [SerializeField]
    Text targetText;
    [SerializeField]
    Button[] buttons;
    [HideInInspector]
    
    public bool StartTimer = false;
    public float Currenttime = 0; //the player's current time. Timer starts when the player enters the zone and stops when they leave

    private void Update()
    {
        Pointer.transform.position = Input.mousePosition;
        if (StartTimer)
        {
            Currenttime += Time.deltaTime;
            string _minutes = Mathf.Floor(Currenttime / 60).ToString("00"); //get the minutes
            string _seconds = Mathf.Floor(Currenttime % 60).ToString("00"); //get the seconds
            string _milisec = (Mathf.Round(Currenttime * 100f) % 100f).ToString("00"); //milliseconds rounded to 2 digits

            targetText.text = "Time: "+_minutes+":"+_seconds+":"+_milisec;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            for(int i=0;i<buttons.Length;i++)
            {
                buttons[i].interactable = !buttons[i].interactable;
            }
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void CheckForPenalty()
    {
        //TODO: Add target checking and apply penalties based on target type
    }
}
