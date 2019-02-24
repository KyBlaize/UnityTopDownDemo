using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName ="Weapons/Automatic RayCast")]
public class AutoRayCastWeapon : BaseGun
{
    public float FireRate = 1f; //time between shots
    public float Damage;
    public float Range;

    private FireRayCastWeapon fireRayCastWeapon;

    private float coolDownDuration; //the cooldown of the current weapon. This is set from the base cooldown
    private float nextReadyTime; //When we can fire the weapon again
    private float coolDownTimeLeft = 0;

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
        if (Time.time > nextReadyTime)
        {
            fireRayCastWeapon.Fire();
            CurrentMagazineRemainder--;
            Debug.Log("Bullets left: "+CurrentMagazineRemainder); //Replace with UI feedback
            nextReadyTime = coolDownDuration + Time.time;
            coolDownTimeLeft = coolDownDuration;
            Firing = true;
        }
        else
            CoolDown();
    }

    public override void Reload()
    {
        Debug.Log("Weapon reloaded"); //Replace with UI feedback
        CurrentMagazineRemainder = MagazineSize;
    }

    public void CoolDown()
    {
        coolDownTimeLeft -= Time.deltaTime;
    }
}
