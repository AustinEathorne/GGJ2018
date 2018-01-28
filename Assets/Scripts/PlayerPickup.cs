using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    private bool hasWeapon;
    public Transform weaponHoldPoint;

    private WeaponManager weaponManagerScript;
    public GameObject weaponManager;

    private Transform weaponToThrowTransform;

    [SerializeField]
    private float launchAngle = 45.0f;

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

		this.weaponToThrowTransform = weapon.transform;       
        weapon.GetComponent<CapsuleCollider>().isTrigger = false;
		this.weaponToThrowTransform.GetComponent<Rigidbody>().useGravity = false;
    }

    // throw weapon
    public void TrasmitWeapon()
    {
        if(hasWeapon)
        {
            hasWeapon = false;          
            // rest of throw physics here

            Vector3 launchPos = this.transform.localPosition;
           	Vector3 targetPos = this.weaponManager.transform.parent.transform.tag == "Sun" ?
				GameObject.FindGameObjectWithTag("Sun").transform.localPosition : 
				GameObject.FindGameObjectWithTag("Moon").transform.localPosition;

			targetPos.y = launchPos.y;

			weaponToThrowTransform.LookAt(targetPos);

			float distance = Vector3.Distance(launchPos, targetPos);
			float initialVel = Mathf.Sqrt((distance * -Physics.gravity.y) / (Mathf.Sin(Mathf.Deg2Rad * this.launchAngle * 2)));

			float yVel = initialVel * Mathf.Sin(Mathf.Deg2Rad * this.launchAngle);
			float zVel = initialVel * Mathf.Cos(Mathf.Deg2Rad * this.launchAngle);

			Vector3 yVelocity = yVel * weaponToThrowTransform.transform.up;
			Vector3 zVelocity = zVel * weaponToThrowTransform.transform.forward;

			Vector3 velocity = zVelocity + yVelocity;

			this.weaponToThrowTransform.GetComponent<Rigidbody>().useGravity = true;
			this.weaponToThrowTransform.GetComponent<Rigidbody>().velocity = velocity;
        }
    }
}
