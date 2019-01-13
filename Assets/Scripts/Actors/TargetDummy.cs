using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDummy : BaseActor
{
    public ActorType MyActorType;

    void Awake()
    {
        Health = 1;
    }
}
