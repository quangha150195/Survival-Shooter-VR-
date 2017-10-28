using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject m_player;
    public GameObject m_gun;

    public bool m_isFx;
    public float m_speedShoot;
    public GameObject m_fx;
    public GameObject recticle;

    public GameObject objLight;
    Light gunLight;
    Light faceLight;
    LineRenderer gunLine;

    RaycastHit hit;
    Vector3 hitPoint;

    private bool m_isGun;
    private bool m_isEffect;
    private bool m_isShoot;
    private float m_timer;
    private GameObject enemy;

    public static int score;
    public Text txtScore;

    void Start ()
    {
        score = 0;
        m_isGun = false;
        m_isEffect = false;
        m_isShoot = false;
        m_timer = 0;
        gunLight = objLight.GetComponent<Light>();
        faceLight = objLight.GetComponentInChildren<Light>();
        gunLine = objLight.GetComponent<LineRenderer>();
	}

    void Update()
    {
        //Score
        txtScore.text = score.ToString() + " / ";

        ///////////////////////////////////
        m_fx.SetActive(m_isFx);
        
        Physics.Raycast(recticle.transform.position, recticle.transform.forward, out hit);
        if(hit.collider!=null)
        {
            if (hit.collider.tag == "Gun" && GvrViewer.Instance.Triggered)
            {
                GameObject gun = hit.collider.gameObject;
                float distance = Vector3.Distance(gun.transform.position, m_player.transform.position);
                if (distance < 3.0f)
                {
                    gun.SetActive(false);
                    m_gun.SetActive(true);
                    m_isGun = true;
                }
            }

            if (hit.collider.tag == "Enemy" && m_isGun)
            {
                Shoot();
                enemy = hit.collider.gameObject;
            }

            if (hit.collider.tag == "Untagged")
            {
                m_isEffect = false;
                Effect(m_isEffect);
            }
        }
    }
	
    void Effect(bool b)
    {
        gunLight.enabled = b;
        faceLight.enabled = b;

        gunLine.enabled = b;
        gunLine.SetPosition(0, m_gun.transform.position);
        gunLine.SetPosition(1, hit.point);
    }

    void Shoot()
    {
        m_timer += Time.deltaTime;

        if (m_timer > m_speedShoot)
        {
            m_timer = 0;
            m_isEffect = !m_isEffect;
            //Show light effect of gun
            Effect(m_isEffect);

            //Take damage to enemy
            if (enemy != null)
            {
                enemy.GetComponent<EnemyHealth>().TakeDamage(15, hit.point);
            }
        }
    }
}
