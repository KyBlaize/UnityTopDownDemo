using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/SemiAuto RayCast Type")]
public class SemiAutoRayCastWeapon : BaseGun
{
    public float Damage;
    public float Range;
    private FireRayCastWeapon fireRayCastWeapon;

    public override void Initialize(GameObject obj)
    {
        fireRayCastWeapon = obj.GetComponent<FireRayCastWeapon>();

        fireRayCastWeapon.Damage = Damage;
        fireRayCastWeapon.Range = Range;
        CurrentMagazineRemainder = MagazineSize;
    }

    public override void Fire()
    {
        if (!Firing)
        {
            fireRayCastWeapon.Fire();
            CurrentMagazineRemainder--;
            Debug.Log("Bullets left: " + CurrentMagazineRemainder);
            Firing = true;
        }
    }

    public override void Reload()
    {
        Debug.Log("Weapon reloaded");
        CurrentMagazineRemainder = MagazineSize;
    }
}
