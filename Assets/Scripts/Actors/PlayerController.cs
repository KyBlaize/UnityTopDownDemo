using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseActor
{
    //---Mouse look
    public float RotationSpeed = 10f;
    //---Movement
    public float Speed = 1;
    public float Gravity = 20f;
    //---Misc.
    public Transform RightHand;
    public Transform LeftHand;

    private CharacterController characterController;
    private Vector3 direction; //Move diretion
    private Camera camera;
    [SerializeField] private BaseGun baseGun; //Set with the initialize function
    [SerializeField] private GameObject ourWeapon; //This is the weapon that holds the raycast script
    


    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        camera = Camera.main;
        Health = 100;//Feed directly from the base actor class
        Initialize(baseGun, ourWeapon);
    }

    private void Initialize(BaseGun gun, GameObject weapon)
    {
        baseGun = gun;
        baseGun.Initialize(weapon);
    }

    private void Update()
    {
        #region Aiming
        Plane _plane = new Plane(Vector3.up, transform.position); //create a plane with the normal pointing up at the player's position
        Ray _ray = camera.ScreenPointToRay(Input.mousePosition); //convert mouse position to screen position
        float _hitDist = 0f; //set an initial distance
        if (_plane.Raycast(_ray, out _hitDist))
        {
            Vector3 _targetPoint = _ray.GetPoint(_hitDist); //set a point that the object will rotate towards
            //slerp smoothly rotates to the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_targetPoint - transform.position), RotationSpeed * Time.deltaTime);
        }
        #endregion

        #region Movement
        var _v = Input.GetAxis("Vertical"); //Move Forward/Back
        var _h = Input.GetAxis("Horizontal");//Strafe
        direction = new Vector3(_h, 0, _v);
        direction.Normalize(); //normalize vector to prevent diagonal speed up
        #endregion

        #region Use Equipment
        if (Input.GetButton("Fire1"))
        {
            if (baseGun.CurrentMagazineRemainder > 0)
            {
                UseEquipment();
            }
            else
            {
                StartCoroutine(ReloadEquipment());
            }
        }
        if (Input.GetButtonUp("Fire1"))
        {
            baseGun.Firing = false;
        }
        #endregion
    }

    private void FixedUpdate()
    {
        //---Apply Gravity
        direction.y = direction.y - (Gravity * Time.deltaTime);
        //---Move
        characterController.Move(direction * Speed * Time.deltaTime);
    }

    private void UseEquipment()
    {
        baseGun.Fire();
    }

    IEnumerator ReloadEquipment() //TODO: Replace this current reload system with an animation driven one
    {
        yield return new WaitForSeconds(baseGun.ReloadTime); //This should be replaced with an animation
        baseGun.Reload();
    }

    //---Prevent gimble lock
    public static float ClampAngle(float angle, float min, float max)
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
