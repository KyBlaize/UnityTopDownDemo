using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField]
    Overlord overlord;

    private void OnTriggerEnter(Collider other)
    {
        overlord.Currenttime = 0;
        overlord.StartTimer = !overlord.StartTimer;
    }
    private void OnTriggerExit(Collider other)
    {
        //Door open/close
    }
}
