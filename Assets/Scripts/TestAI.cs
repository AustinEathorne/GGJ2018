using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAI : MonoBehaviour {

    [SerializeField] GameObject m_player;

    [HideInInspector] public bool m_isFrozen = false;
    [HideInInspector] public bool m_isDead = false;

    [SerializeField] private Material m_mattFrozen;
    [SerializeField] private Material m_mattNorm;

    private float m_freezeCoolDown = 3f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!m_isDead)
        {
            if (!m_isFrozen)
            {
                m_freezeCoolDown = 3f;
                gameObject.GetComponent<Renderer>().material.color = m_mattNorm.color;
                float step = 5 * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, m_player.transform.position, step);
            }
            else if (m_isFrozen)
            {
                m_freezeCoolDown -= Time.deltaTime;
                gameObject.GetComponent<Renderer>().material.color = m_mattFrozen.color;
                if (m_freezeCoolDown <= 0f)
                {
                    m_isFrozen = false;
                }
            }
        }

        if(m_isDead)
        {
            Destroy(gameObject);
        }

	}
}
