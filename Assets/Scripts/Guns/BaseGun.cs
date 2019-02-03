using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Base class for all of our guns
/*Callbacks available for Scriptable Objects
 * OnEnable
 * OnDisable
 * OnDestroy
 */
public abstract class BaseGun : ScriptableObject
{
    public float FireRate = 1f; //Cooldown
    public float ReloadTime = 1f; //How long it takes to reload the weapon
    public int MagazineSize;
    [HideInInspector] public int CurrentMagazineRemainder;

    public abstract void Initialize(GameObject obj);
    public abstract void Fire();
    public abstract void Reload();
}
