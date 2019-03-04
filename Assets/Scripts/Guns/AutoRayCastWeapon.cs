using System.Collections;
using UnityEngine;
/*
 * Automatic weapons are different in that they require a cool down system
 * They do not necessarily need TriggerReleased, though it is useful in terms of controlling recoil patterns
 */
[CreateAssetMenu(menuName ="Weapons/Automatic RayCast")]
public class AutoRayCastWeapon : BaseGun
{
    public float FireRate = 1f; //---Time between shots
    public float Damage;
    public float Range;

    private FireRayCastWeapon fireRayCastWeapon;
    private float coolDownTimeLeft = 0;

    public override void Initialize(GameObject obj) 
    {
        fireRayCastWeapon = obj.GetComponent<FireRayCastWeapon>();

        fireRayCastWeapon.Damage = Damage;
        fireRayCastWeapon.Range = Range;
        CurrentMagazineRemainder = MagazineSize;
        coolDownTimeLeft = 0;
    }

    public override void TriggerHeld()
    {
        if (coolDownTimeLeft <= 0)
        {
            coolDownTimeLeft = FireRate;
            fireRayCastWeapon.Fire();
            CurrentMagazineRemainder--;
            Debug.Log("Bullets left: " + CurrentMagazineRemainder); //Replace with UI feedback
        }
        else
            CoolDown();
    }

    public override void TriggerReleased()
    {
        coolDownTimeLeft = 0;
    }

    public override void Reload()
    {
        Debug.Log("Weapon reloaded"); //Replace with UI feedback
        CurrentMagazineRemainder = MagazineSize;
    }

    private void CoolDown()
    {
        coolDownTimeLeft -= Time.deltaTime;
    }
}
