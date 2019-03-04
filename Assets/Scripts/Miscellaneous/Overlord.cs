using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Overlord : MonoBehaviour
{
    //---This script contains the game's timer and score tracking system
    int finalscore = 0;
    public Image Pointer;
    [SerializeField]private Text targetText;
    [SerializeField]private Button[] buttons;
    [HideInInspector]public bool StartTimer = false;
    [HideInInspector]public float Currenttime = 0; //---Timer starts when the player enters the zone and stops when they leave
    private List<TargetDummy> targets = new List<TargetDummy>();

    private void OnEnable() //if we are also subscribing to these events, should we use Awake or OnEnable?
    {
        //---Get all potential targets in the level.
        var _objects = FindObjectsOfType<TargetDummy>();
        foreach (TargetDummy actor in _objects)
        {
            //---Add each target to the target list and subscribe to events
            targets.Add(actor);
            actor.Died += CheckForPenalty;
        }
    }

    private void OnDisable()
    {
        var _objects = FindObjectsOfType<TargetDummy>();
        foreach (TargetDummy actor in _objects)
        {
            //---Unsub to target death events
            actor.Died -= CheckForPenalty; 
        }
    }

    private void Update()
    {
        Pointer.transform.position = Input.mousePosition; //TODO: Lock this within the bounds of the screen
        if (StartTimer)
        {
            Currenttime += Time.deltaTime;
            //---Get Minutes, Seconds, and Milliseconds to 2 decimal points
            string _minutes = Mathf.Floor(Currenttime / 60).ToString("00");
            string _seconds = Mathf.Floor(Currenttime % 60).ToString("00");
            string _milisec = (Mathf.Round(Currenttime * 100f) % 100f).ToString("00");

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

    public void Restart() //This is a temporary entry
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame() //This is a temporary entry
    {
        Application.Quit();
    }

    private void CheckForPenalty(BaseActor.ActorType actorType)
    {
        //---Hittting an enemy does nothing, but hitting a civilian will incur a +1sec to your final time
        if (actorType == BaseActor.ActorType.Player)
        {
            Debug.LogWarning("This should not be appearing");
        }
        if (actorType == BaseActor.ActorType.Civilian)
        {
            /*
             * Design point: A warning should be displayed regarding shooting civs.
             *  - However, total time penalties should be applied at the end of the level.
             *  - As a method of testing, we will keep it calculated in the game for now.
             */
            Debug.Log("Don't shoot civvies!");
            Currenttime+=1f;
        }
        if (actorType == BaseActor.ActorType.Enemy)
        {
            Debug.Log("It's ok, you can shoot enemies");
        }
    }
}
