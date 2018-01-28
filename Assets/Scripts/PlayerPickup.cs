using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    private bool hasWeapon;
    public Transform weaponHoldPoint;

    private WeaponManager weaponManagerScript;
    public GameObject weaponManager;

    private void Awake()
    {
        hasWeapon = false;
    }

    private void Start()
    {
        weaponManagerScript = weaponManager.GetComponent<WeaponManager>();
    }

    private void Update()
    {
        if (Input.GetButton("SwapWeapon" + GetComponent<PlayerControls>().m_PlayerNumber))
        {
            Debug.Log("button pressed");
            // can only transmit if you have a weapon
            if(hasWeapon)
            {
                // checking to see which player pressed transmit
                if (this.gameObject.tag == "Moon")
                {
                    weaponManagerScript.moonWantsToSwitch = true;
                }
                if (this.gameObject.tag == "Sun")
                {
                    weaponManagerScript.sunWantsToSwitch = true;
                }

                if (weaponManagerScript.moonWantsToSwitch && weaponManagerScript.sunWantsToSwitch)
                {
                    // transmit weapon call here
                    TrasmitWeapon();

                }
                // if player presses the transmit button again without the other player pressing theirs then they do not want to switch
                else if (Input.GetButton("SwapWeapon" + GetComponent<PlayerControls>().m_PlayerNumber))
                {
                    Debug.Log("button pressed again");
                    if (GetComponent<PlayerControls>().m_PlayerNumber == 1)
                    {
                        weaponManagerScript.moonWantsToSwitch = false;
                    }
                    if (GetComponent<PlayerControls>().m_PlayerNumber == 2)
                    {
                        weaponManagerScript.sunWantsToSwitch = false;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Weapon")
        {
            if(!hasWeapon)
            {
                GrabWeapon(other.gameObject);
            }          
        }
    }

    private void GrabWeapon(GameObject weapon)
    {
        hasWeapon = true;
        weapon.transform.position = weaponHoldPoint.transform.position;
        weapon.transform.parent = transform;
        weapon.GetComponent<CapsuleCollider>().isTrigger = false;
    }

    // throw weapon
    public void TrasmitWeapon()
    {
        if(hasWeapon)
        {
            hasWeapon = false;          
            // rest of throw physics here

        }
    }
}
