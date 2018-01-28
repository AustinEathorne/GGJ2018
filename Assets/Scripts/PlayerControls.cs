using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public int m_PlayerNumber;
    public float m_Speed = 12f;
    public float m_TurnSpeed = 180f;
    public float m_damping = 10.0f;


    private string m_MovementAxisName;
    private string m_TurnAxisHorizontalName;
    private string m_TurnAxisVerticalName;
    private Rigidbody m_Rigidbody;
    private float m_MovementInputValue;
    private float m_TurnInputXValue;
    private float m_TurnInputYValue;
    private float m_LookAngleInDegrees;


    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // called after awake but before updates
    private void OnEnable()
    {
        m_Rigidbody.isKinematic = false;
    }

    private void OnDisable()
    {
        m_Rigidbody.isKinematic = true;
    }

    private void Start()
    {
        m_MovementAxisName = "Vertical" + m_PlayerNumber;
        m_TurnAxisHorizontalName = "RHorizontal" + m_PlayerNumber;
        m_TurnAxisVerticalName = "RVertical" + m_PlayerNumber;
    }

    private void Update()
    {
        m_MovementInputValue = Input.GetAxis(m_MovementAxisName);
        m_TurnInputXValue = Input.GetAxis(m_TurnAxisHorizontalName);
        m_TurnInputYValue = Input.GetAxis(m_TurnAxisVerticalName);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        // Adjust the position of player
        Vector3 movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;
        m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
        // handling the rotation of the player
        if (m_TurnInputXValue != 0.0f || m_TurnInputYValue != 0.0f)
        {
            m_LookAngleInDegrees = Mathf.Atan2(m_TurnInputYValue, m_TurnInputXValue) * Mathf.Rad2Deg;
            Quaternion eulerAngle = Quaternion.Euler(0.0f, m_LookAngleInDegrees, 0.0f);
            m_Rigidbody.rotation = Quaternion.Lerp(m_Rigidbody.rotation, eulerAngle, Time.deltaTime * m_damping);
        }
    }
}