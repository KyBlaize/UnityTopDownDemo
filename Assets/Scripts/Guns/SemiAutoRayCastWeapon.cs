using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Semi-Automatic weapons are different in that they rely on TriggerReleased() in order to function as expected 
 */
[CreateAssetMenu(menuName = "Weapons/SemiAuto RayCast")]
public class SemiAutoRayCastWeapon : BaseGun
{
    public float Damage;
    public float Range;
    [HideInInspector] public bool Fired;

    private FireRayCastWeapon fireRayCastWeapon;

    public override void Initialize(GameObject obj)
    {
        fireRayCastWeapon = obj.GetComponent<FireRayCastWeapon>();

        fireRayCastWeapon.Damage = Damage;
        fireRayCastWeapon.Range = Range;
        CurrentMagazineRemainder = MagazineSize;
        Fired = false;
    }

    public override void TriggerHeld()
    {
        if (!Fired)
        {
            fireRayCastWeapon.Fire();
            CurrentMagazineRemainder--;
            Debug.Log("Bullets left: " + CurrentMagazineRemainder); //Replace with UI feedback
            Fired = true;
        }
    }

    public override void TriggerReleased()
    {
        Fired = false;
    }

    public override void Reload()
    {
        Debug.Log("Weapon reloaded"); //Replace with UI feedback
        CurrentMagazineRemainder = MagazineSize;
    }
}
