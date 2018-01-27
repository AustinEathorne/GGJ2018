using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    private bool hasWeapon;

    public Transform weaponHoldPoint;

    private void Awake()
    {
        hasWeapon = false;
    }

    private void Update()
    {
        if (Input.GetButton("SwapWeapon" + GetComponent<PlayerControls>().m_PlayerNumber))
        {
            if(hasWeapon)
            {
                // display icon to allow throwing
                Debug.Log("buttonPressed");
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
    public void ThrowWeapon()
    {
        if(hasWeapon)
        {
            hasWeapon = false;
            
            // rest of throw physics here
        }
    }
}
