using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : BaseGun
{
    //Based on the FN SCAR-L
    public override float DamageOut
    {
        get
        {
            return base.DamageOut;
        }

        set
        {
            base.DamageOut = 30f;
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
            base.WeaponRange = 500f;
        }
    }
}
