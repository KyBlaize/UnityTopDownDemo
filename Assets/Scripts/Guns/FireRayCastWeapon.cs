using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRayCastWeapon : MonoBehaviour
{
    [HideInInspector] public float Damage = 1f;
    [HideInInspector] public float Range = 10f;
    
    public void Fire()
    {
        Vector3 _origin = transform.position;
        RaycastHit _hit;
        if (Physics.Raycast(_origin,transform.forward,out _hit,Range))
        {
            Debug.DrawRay(_origin,transform.forward*Range,Color.red,1f);
            BaseActor _target = _hit.collider.GetComponent<BaseActor>();
            if (_target)
                _target.TakeDamage(Damage);
        }
    }
}
