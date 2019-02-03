using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDummy : BaseActor
{
    public ActorType MyActorType;
    public delegate void CallDeath();
    public event CallDeath Died;

    private Animator animator;
    void Awake()
    {
        Health = 1;
        animator = GetComponent<Animator>();
    }

    public override void Die()
    {
        animator.SetBool("Death", true); //play an animation
        if (Died != null) //first, ensure that died is not null, then call my event
        {
            Died();
        }
    }

}
