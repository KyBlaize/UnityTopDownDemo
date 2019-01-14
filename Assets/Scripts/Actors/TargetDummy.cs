using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDummy : BaseActor
{
    public ActorType MyActorType;
    Animator animator;
    void Awake()
    {
        Health = 1;
        animator = GetComponent<Animator>();
    }

    public override void Die()
    {
        animator.SetBool("Death", true);
    }

}
