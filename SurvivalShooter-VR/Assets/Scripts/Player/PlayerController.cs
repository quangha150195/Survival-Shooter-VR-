using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public bool m_move;
    public float m_speed;
    public GameObject m_head;
    private Rigidbody m_playerRigid;
    private Vector3 m_forward;
    private float m_y;

	void Start ()
    {
        //Get rigidbody of player
        m_playerRigid = this.GetComponent<Rigidbody>();

        //Set pos y original
        m_y = m_head.transform.forward.y;
    }
	
    void Update()
    {
        if(GvrViewer.Instance.Triggered)
        {
            m_move = !m_move;
        }

        if (m_move)
        {
            m_forward = m_head.transform.forward;
            m_forward.y = m_y;

            m_playerRigid.velocity = m_forward * m_speed;
        }
        else
        {
            m_playerRigid.velocity = Vector3.zero;
        }
    }
}
