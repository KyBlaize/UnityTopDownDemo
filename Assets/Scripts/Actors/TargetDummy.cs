using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDummy : BaseActor
{
    public delegate void CallDeath(ActorType actorType);// the listener needs to know what type of actor got killed
    public event CallDeath Died;

    private Animator animator;
    void Awake()
    {
        Health = 1;
        animator = GetComponent<Animator>();
    }

    public override void Die() //TODO: We also need to track who killed who
    {
        animator.SetBool("Death", true); //play an animation
        if (Died != null) //first, ensure that died is not null, then call my event
        {
            Died(MyType);//The event listener needs to know what type of object it was
            //Debug.Log(whoKilledMe.ToString());
        }
    }

}
