using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Constructor for guns
public class BaseGun
{
    private float damageOut;
    public virtual float DamageOut
    {
        get { return damageOut; }
        set { damageOut = value; }
    }

    private float range;
    public virtual float WeaponRange
    {
        get { return range; }
        set { range = value; }
    }

    private float rateoffire;
    public virtual float RateOfFire
    {
        get { return rateoffire; }
        set { rateoffire = value; }
    }

    private float magazinesize;
    public virtual float MagazineSize
    {
        get { return magazinesize; }
        set { magazinesize = value; }
    }

    public virtual void Fire()//override in weapon implementations
    {
        Debug.Log("Weapon fired");
    }
}
