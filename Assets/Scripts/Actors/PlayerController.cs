﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseActor {

    //---Guns
    Pistol pistol; //just have a pistol for now
    //---Mouse look
    public float RotationSpeed = 10f; //how fast to turn the player
    //---Movement
    public float Speed = 1;
    public float Gravity = 20f;

    CharacterController characterController;
    Vector3 direction;
    Camera camera;
    
	void Awake () {
        characterController = GetComponent<CharacterController>();
        camera = Camera.main;
        Health = 100;//Feed directly from the base actor class
        pistol = new Pistol();
        CurrentGun = pistol;
        Debug.Log(CurrentGun.ToString());
	}
	
	void Update () {
        //Rotate to aim
        Plane _plane = new Plane(Vector3.up, transform.position);
        Ray _ray = camera.ScreenPointToRay(Input.mousePosition);
        float _hitDist = 0f;
        if (_plane.Raycast(_ray, out _hitDist))
        {
            Vector3 _targetPoint = _ray.GetPoint(_hitDist);
            //transform.rotation = Quaternion.LookRotation(_targetPoint - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_targetPoint - transform.position), RotationSpeed * Time.deltaTime);
            //Quaternion _rotation = Quaternion.LookRotation(_targetPoint - transform.position);
            //transform.rotation = _rotation;
        }
        var _v = Input.GetAxis("Vertical"); //Move Forward/Back
        var _h = Input.GetAxis("Horizontal");//Strafe
        direction = new Vector3(_h, 0, _v);
        direction.Normalize(); //normalize vector to prevent diagonal speed up
        if (Input.GetButtonDown("Fire1"))
            Attack(transform.position, transform.forward, CurrentGun.WeaponRange);
    }

    private void FixedUpdate()
    {
        Debug.DrawRay(transform.position, transform.forward * 50, Color.red);//debug the line of fire
        //---Apply Gravity
        direction.y = direction.y - (Gravity * Time.deltaTime);
        //---Move
        characterController.Move(direction*Speed*Time.deltaTime);
    }


    public override void Attack(Vector3 origin, Vector3 direction, float range)
    {
        base.Attack(transform.position, transform.forward, CurrentGun.WeaponRange);
        Vector3 _rayOrigin = origin;
        RaycastHit _hit;
        if (Physics.Raycast(_rayOrigin, direction, out _hit, range))
        {
            TargetDummy target = _hit.collider.GetComponent<TargetDummy>();
            if (target.MyActorType == ActorType.Enemy)
            {
                Debug.Log("Enemy Down");
            }
        }
    }

    //---Prevent gimble lock
    public static float ClampAngle(float angle,float min, float max)
    {
        if (angle < -360f)
            angle += 360f;
        if (angle > 360f)
            angle -= 360f;
        return Mathf.Clamp(angle, min, max);
    }
    /*//Removed mouse look
    //Vector3 _position = Camera.main.WorldToScreenPoint(transform.position);
    //Vector3 _direction = Input.mousePosition - _position;
    //float _angle = Mathf.Atan2(_direction.y,_direction.x)*Mathf.Rad2Deg;
    //transform.rotation = Quaternion.AngleAxis(_angle, Vector3.up);*/
}
