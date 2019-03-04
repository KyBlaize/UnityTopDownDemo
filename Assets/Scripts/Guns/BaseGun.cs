using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  
 *  This script is the basis of all weapons (whether they are raycast or not)
 *      - Take note of TriggerHeld and TriggerReleased
 *          - TriggerHeld is mainly useful for automatic weapons
 *          - TriggerReleased is useful for creating semi-auto and burst-type weapons
 *              - This prevents having to create ridiculously long cool down times to simulate semi-auto functionality
 *              - This also allows for automatic weapons to fully reset their cool down time and recoil (though recoil is not implemented in this project)
 *  Callbacks available for Scriptable Objects
 *      - OnEnable
 *      - OnDisable
 *      - OnDestroy
 */
public abstract class BaseGun : ScriptableObject
{
    public string Name = "New Gun";
    public float ReloadTime = 1f;
    public int MagazineSize;
    [HideInInspector] public int CurrentMagazineRemainder;

    public abstract void Initialize(GameObject obj); //---obj is the object that has FireRayCastWeapon script
    public abstract void TriggerHeld();
    public abstract void TriggerReleased(); //---Make sure all guns are ready to fire again as soon as the trigger is released
    public abstract void Reload();
}
