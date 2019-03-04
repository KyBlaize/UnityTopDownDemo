using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRayCastWeapon : MonoBehaviour
{
    [HideInInspector] public float Damage = 1f;
    [HideInInspector] public float Range = 10f;
    [HideInInspector] public BaseActor.ActorType ActorType;
    public Transform GunEnd;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.2f); 

    public void Fire()
    {
        Vector3 _origin = transform.position;
        RaycastHit _hit;
        //---Start the shot effect
        StartCoroutine(FiringEffect());
        //---Something was hit in weapon range
        if (Physics.Raycast(_origin,transform.forward,out _hit,Range))
        {
            BaseActor _target = _hit.collider.GetComponent<BaseActor>();
            if (_target)
                _target.TakeDamage(Damage);
        }
    }

    private IEnumerator FiringEffect()
    {
        //TODO: Add muzzle flash at GunEnd.position.
        yield return shotDuration;
    }
}
