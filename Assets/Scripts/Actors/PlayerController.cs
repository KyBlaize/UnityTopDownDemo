using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * Many things in this test/POC (proof-of concept) package are hard-coded into the system (such as player controls).
 * However, to make sure that it is modular, and shows as much of my skill and knowledge of OOP and Unity programming, 
 *  I have made sure to make code as flexible as possible.
 * Many of the public/serialized fields will eventually be made private/hidden in inspector as most things will run
 * Strictly speaking, picking up items should be placed in an Interface. However, considering the nature of this project, I have opted to leave it out for now
 */
public class PlayerController : BaseActor
{
    //---Mouse look
    public float RotationSpeed = 10f;
    //---Movement
    public float Speed = 1;
    public float Gravity = 20f;
    //---Misc.
    public Transform RightHand; //---Use as an anchor for equipment
    public Transform LeftHand; //---Use as an anchor for equipment
    public Text PlayerQuotesText;
    public Vector3 WeaponScale;

    private CharacterController characterController;
    private Vector3 direction; //---Move diretion
    private Camera camera;
    [SerializeField] private GameObject[] MyArsenal = new GameObject[2];
    [SerializeField] private BaseGun baseGun; //---This is the type of weapon that we are using
    [SerializeField] private GameObject ourWeapon; //---This will get passed into the baseGun
    
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        camera = Camera.main;
        Health = 100; //---Feed directly from the base actor class
        PlayerQuotesText.enabled = false;
    }

    private void Update()
    {
        #region Aiming
        Plane _plane = new Plane(Vector3.up, transform.position); //---Create a plane with the normal pointing up at the player's position
        Ray _ray = camera.ScreenPointToRay(Input.mousePosition); //---Convert mouse position to screen position
        float _hitDist = 0f; //---Set an initial distance
        if (_plane.Raycast(_ray, out _hitDist))
        {
            Vector3 _targetPoint = _ray.GetPoint(_hitDist); //---Set a point that the object will rotate towards
            //---Slerp smoothly rotates to the target rotation
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
        if (Input.GetButton("Fire1"))//---Button is held down
        {
            if (baseGun != null)
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
            else
                PlayerQuotesText.enabled = true;
        }
        if (Input.GetButtonUp("Fire1"))//---After it is released
        {
            if (MyArsenal[0] != null)
                baseGun.TriggerReleased();
            PlayerQuotesText.enabled = false; //keep this as a safety net for debugging purposes
        }
        #endregion

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Weapon 1");
            baseGun = null;
            ourWeapon = null;
            MyArsenal[0].SetActive(true);
            MyArsenal[1].SetActive(false);
            EquipWeapon(MyArsenal[0].GetComponent<Firearm>().Gun, MyArsenal[0]);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("Weapon 2");
            baseGun = null;
            ourWeapon = null;
            MyArsenal[0].SetActive(false);
            MyArsenal[1].SetActive(true);
            EquipWeapon(MyArsenal[1].GetComponent<Firearm>().Gun, MyArsenal[1]);
        }
    }

    private void FixedUpdate()
    {
        //---Apply Gravity
        direction.y = direction.y - (Gravity * Time.deltaTime);
        //---Move
        characterController.Move(direction * Speed * Time.deltaTime);
    }

    #region Using Equipment
    private void UseEquipment()
    {
        baseGun.TriggerHeld();
    }

    private IEnumerator ReloadEquipment() //Best to replace this current reload system with an animation driven one
    {
        yield return new WaitForSeconds(baseGun.ReloadTime);
        baseGun.Reload();
    }
    #endregion

    #region Item pick up and switching
    public void OnTriggerEnter(Collider other)
    {
        PutWeaponInHands(other.gameObject);
        other.GetComponent<BoxCollider>().enabled = false;
    }

    private void PutWeaponInHands(GameObject gameobject) //---Put objects into loadout
    {
        //---Make the object a child of the right hand weapon, and scale accordingly
        gameobject.transform.parent = RightHand;
        gameobject.transform.localPosition = Vector3.zero;
        gameobject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        gameobject.transform.localScale = WeaponScale;

        //---Place into the player's weapons arsenal
        var _gun = gameobject.GetComponent<Firearm>().Gun;
        for (int i = 0; i < MyArsenal.Length; i++)
        {
            if (MyArsenal[i] == null)
            {
                MyArsenal[i] = gameobject;
                MyArsenal[i].SetActive(false);
                if (ourWeapon == null)//---If there is no current active weapon, make this one active
                {
                    EquipWeapon(_gun, gameobject);
                    MyArsenal[i].SetActive(true);
                }
                break;
            }
        }
    }

    private void EquipWeapon(BaseGun gun, GameObject gameobject) //---Set the active weapon
    {
        baseGun = gun;
        gun.Initialize(gameobject);
        ourWeapon = gameObject;
    }
    #endregion

    public static float ClampAngle(float angle, float min, float max) //---Prevent gimble lock
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
