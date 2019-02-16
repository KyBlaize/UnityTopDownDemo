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
    public float ReloadTime = 1f; //How long it takes to reload the weapon
    public int MagazineSize;
    [HideInInspector] public int CurrentMagazineRemainder;
    [HideInInspector] public bool Firing = false; //Check if the weapon is firing (useful for semi-auto and burst)

    public abstract void Initialize(GameObject obj);
    public abstract void Fire();
    public abstract void Reload();
}
