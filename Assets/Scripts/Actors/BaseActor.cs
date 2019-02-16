using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseActor : MonoBehaviour
{
    public float Health { get; set; }
    public enum ActorType { Player, Enemy, Civilian, Environment }
    public ActorType MyType;

    public virtual void Attack(/*Vector3 origin, Vector3 direction, float weaponrange, float damage*/)
    {
        //CurrentGun.Fire();
        /*RaycastHit _hit;
        if (Physics.Raycast(origin, direction, out _hit, weaponrange))
        {
            BaseActor _target = _hit.collider.GetComponent<BaseActor>();
            _target.TakeDamage(damage);
        }*/
    }

    public virtual void TakeDamage(float damageIn)//we need to know how much damage to deal
    {
        Health -= damageIn;
        if (Health <= 0)
            Die();
    }

    public virtual void Die()//needs to know what killed this object
    {
        //Will have code in the implementation
    }
}
