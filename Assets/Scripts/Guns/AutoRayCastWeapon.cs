using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName ="Weapons/Automatic RayCast Type")]
public class AutoRayCastWeapon : BaseGun
{
    public float FireRate = 1f; //time between shots
    public float Damage;
    public float Range;

    private FireRayCastWeapon fireRayCastWeapon;

    private float coolDownDuration; //the cooldown of the current weapon. This is set from the base cooldown
    private float nextReadyTime; //When we can fire the weapon again
    private float coolDownTimeLeft;

    public override void Initialize(GameObject obj)
    {
        fireRayCastWeapon = obj.GetComponent<FireRayCastWeapon>();

        fireRayCastWeapon.Damage = Damage;
        fireRayCastWeapon.Range = Range;
        CurrentMagazineRemainder = MagazineSize;
        coolDownDuration = FireRate;
    }

    public override void Fire()
    {
        if (Time.time > nextReadyTime && !Firing)
        {
            fireRayCastWeapon.Fire();
            CurrentMagazineRemainder--;
            Debug.Log("Bullets left: "+CurrentMagazineRemainder);
            nextReadyTime = coolDownDuration + Time.time;
            coolDownTimeLeft = coolDownDuration;
        }
        else
            CoolDown();
    }

    public override void Reload()
    {
        Debug.Log("Weapon reloaded");
        CurrentMagazineRemainder = MagazineSize;
    }

    public void CoolDown()
    {
        coolDownTimeLeft -= Time.deltaTime;
    }
}
