using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseActor : MonoBehaviour
{
    public float Health { get; set; }
    public virtual BaseGun CurrentGun { get; set; }
    public enum ActorType { Player, Enemy, Civilian, Environment }
    public ActorType MyType { get; set; }

    //public virtual void Attack()
    //{
        //CurrentGun.Fire();
    //}

    public virtual void TakeDamage(float damageIn)
    {
        //Debug.Log("I'm hit!");
        Health -= damageIn;
        if (Health <= 0)
            Die();
    }

    public virtual void Die()
    {
        //PlayAnimation
    }
}
