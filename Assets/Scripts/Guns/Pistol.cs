using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : BaseGun
{
    //Based on the Beretta M92F
    //This class does not need to override the base weapon's Fire() method
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
}
