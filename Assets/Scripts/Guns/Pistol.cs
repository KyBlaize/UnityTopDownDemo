using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : BaseGun
{
    //Based on the Beretta M92F
    public override float DamageOut
    {
        get
        {
            return base.DamageOut;
        }

        set
        {
            base.DamageOut = 20f;
        }
    }

    public override float WeaponRange
    {
        get
        {
            return base.WeaponRange;
        }

        set
        {
            base.WeaponRange = 50f;
        }
    }

    public override void Fire()
    {
        base.Fire();
        Debug.Log("Pistol fired");
    }
}
