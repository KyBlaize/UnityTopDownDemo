using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName ="Weapons/RayCast Type")]
public class RayCastWeapon : BaseGun
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
        CurrentMagazineRemainder--;
        fireRayCastWeapon.Fire();
    }

    public override void Reload()
    {
        Debug.Log("Weapon reloaded"); //Debug
        CurrentMagazineRemainder = MagazineSize;
    }
}
