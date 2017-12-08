using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public static PlayerController instance;
    public bool m_move;
    public float m_speed;
    public GameObject m_head;
    private Rigidbody m_playerRigid;
    private Vector3 m_forward;
    private float m_y;
    public static bool is_useGamePad = true;

	void Start ()
    {
        instance = this;
        //Get rigidbody of player
        m_playerRigid = this.GetComponent<Rigidbody>();

        //Set pos y original
        m_y = m_head.transform.forward.y;
        m_move = true;
    }
	
    void Update()
    {
        if (m_move)
        {
            if (!is_useGamePad)
            {
                m_forward = m_head.transform.forward;
                m_forward.y = m_y;

                m_playerRigid.velocity = m_forward * m_speed;
            }
            else
            {
                //Move fast
                if (Input.GetButton("MoveFastButton"))
                {
                    m_speed = 3.0f;
                }
                else
                {
                    m_speed = 1.5f;
                }

                if (Input.GetButton("YButton"))
                {
                    m_forward = m_head.transform.forward;
                    m_forward.y = m_y;
                    m_playerRigid.velocity = m_forward * m_speed;
                }
                else
                if (Input.GetButton("AButton"))
                {
                    m_forward = m_head.transform.forward;
                    m_forward.y = m_y;
                    m_playerRigid.velocity = -m_forward * m_speed;
                }
                else
                if (Input.GetButton("XButton"))
                {
                    m_forward = m_head.transform.right;
                    m_forward.y = m_y;
                    m_playerRigid.velocity = -m_forward * m_speed;
                }
                else
                if (Input.GetButton("BButton"))
                {
                    m_forward = m_head.transform.right;
                    m_forward.y = m_y;
                    m_playerRigid.velocity = m_forward * m_speed;
                }
                else
                {
                    m_playerRigid.velocity = Vector3.zero;
                }
            }
        }
        else
        {
            m_playerRigid.velocity = Vector3.zero;
        }
    }
}
