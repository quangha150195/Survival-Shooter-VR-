using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class GameController : MonoBehaviour {

    public GameObject m_player;
    public GameObject m_gun;
    public GameObject m_icon;
    public GameObject m_iconHealth;
    public GameObject m_slider;

    public bool m_isFx;
    private float m_speedShoot;
    public GameObject m_fx;
    public GameObject recticle;

    public GameObject objLight;
    Light gunLight;
    Light faceLight;
    LineRenderer gunLine;

    RaycastHit hit;
    Vector3 hitPoint;
    Vector3 startPositionPlayer;

    private bool m_isGun;
    private bool m_isEffect;
    private bool m_isShoot;
    private float m_timer;
    private GameObject enemy;

    public static int score;
    public Text txtScore;
    public Text txtScoreOver;

    [SerializeField]
    private GameObject m_FadeforDie;
    [SerializeField]
    private float timeFade = 2;
    [SerializeField]
    private GameObject m_GameOver;


    [Header("Audio")]
    [SerializeField]
    private AudioClip m_SoundClick;
    [SerializeField]
    private AudioClip m_SoundRaycast;

    [Header("Animation")]
    [SerializeField]
    private GameObject m_Wood;

    Animator m_Animatormanager;
    AudioSource m_SoundManager;
    private float timeFadeOver = 0;
    private bool _checkOneShot = true;
    private bool m_isAttack = false;

    void Start ()
    {
        m_SoundManager = gameObject.GetComponent<AudioSource>();
        m_Animatormanager = m_Wood.GetComponent<Animator>();
       
        startPositionPlayer = m_player.transform.position;
        score = 0;
        m_isGun = false;
        m_isEffect = false;
        m_isShoot = false;
        m_timer = 0;
        gunLight = objLight.GetComponent<Light>();
        faceLight = objLight.GetComponentInChildren<Light>();
        gunLine = objLight.GetComponent<LineRenderer>();

        if(PlayerController.is_useGamePad)
        {
            m_speedShoot = 0.1f;
        }
        else
        {
            m_speedShoot = 0.001f;
        }
	}

    void Update()
    {
        //Score
        txtScore.text = score.ToString() + " / ";

        ///////////////////////////////////
        m_fx.SetActive(m_isFx);

        if (Input.GetButton("ShootButton") || GvrViewer.Instance.Triggered)
        {
            m_isAttack = true;
        }
        else
        {
            m_isAttack = false;
            Effect(false);
        }

        if(m_isAttack)
        {
            //Attack
            if (m_isGun)
            {
                Debug.Log("Shoot");
                Shoot();
                m_gun.GetComponent<Animator>().SetBool("Shoot", true);
            }
            else
            {
                m_Animatormanager.SetBool("Fight", true);
            }
        }

        Physics.Raycast(recticle.transform.position, recticle.transform.forward, out hit);
        if(hit.collider!=null)
        {
            if (hit.collider.tag == "Gun" && (GvrViewer.Instance.Triggered || Input.GetButton("PickButton")))
            {
                GameObject gun = hit.collider.gameObject;
                float distance = Vector3.Distance(gun.transform.position, m_player.transform.position);
                if (distance < 3.0f)
                {
                    gun.SetActive(false);
                    m_gun.SetActive(true);
                    m_Wood.SetActive(false);
                    m_isGun = true;
                }
            }

            if (hit.collider.tag == "Wood" && (GvrViewer.Instance.Triggered || Input.GetButton("PickButton")))
            {
                GameObject wood = hit.collider.gameObject;
                float distance = Vector3.Distance(wood.transform.position, m_player.transform.position);
                if (distance < 3.0f)
                {
                    wood.SetActive(false);
                    m_gun.SetActive(false);
                    m_Wood.SetActive(true);
                    m_isGun = false;
                }
            }

            if (m_isAttack && hit.collider.tag == "Enemy")
            {
                enemy = hit.collider.gameObject;
            }
            else 
            {
                m_Animatormanager.SetBool("Fight", false);   
                m_gun.GetComponent<Animator>().SetBool("Shoot", false);      
            }

            if (hit.collider.tag == "VRMenu")
            {
                if (_checkOneShot)
                {
                    m_SoundManager.PlayOneShot(m_SoundRaycast);
                    _checkOneShot = false;
                }
                GameObject _thisButton = hit.collider.gameObject;
                iTween.ScaleTo(_thisButton, iTween.Hash("x", 1.5f, "y", 1.5f, "time", 0.3f));
                if (GvrViewer.Instance.Triggered)
                {
                    m_SoundManager.PlayOneShot(m_SoundClick);
                    hit.collider.gameObject.transform.localScale = new Vector3(1, 1, 1);
                    m_player.GetComponent<PlayerController>().m_speed = 0;
                    _thisButton.GetComponent<Button>().onClick.Invoke();
                }
            }
            else if(hit.collider.tag != "VRMenu")
            {
                _checkOneShot = true;
                Scale_ButtonMenu();
            }
        }

        if(PlayerHealth.instance.isDead)
        {
            CanvasOver(timeFade);
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

    public void CanvasOver(float _timer)
    {   
        timeFadeOver += Time.deltaTime;
        m_player.GetComponent<PlayerController>().m_speed = 0;
        EnemyManager.instance.m_maxEnemy = 0;
        GameObject[] listEnemy = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject _enemy in listEnemy)
        {
            Destroy(_enemy);
        }
        
        if (_timer > timeFadeOver)
        {          
            m_FadeforDie.GetComponent<CanvasGroup>().alpha += Time.deltaTime/ _timer;
        }
        else
        {
            m_gun.SetActive(false);
            m_GameOver.SetActive(true);
            txtScore.gameObject.SetActive(false);
            m_icon.SetActive(false);
            m_iconHealth.SetActive(false);
            m_slider.SetActive(false);
            txtScoreOver.text = score.ToString();

            m_player.transform.position = startPositionPlayer;
            m_FadeforDie.GetComponent<CanvasGroup>().alpha -= Time.deltaTime / (_timer * 1.2f);
           
        }
        if (m_FadeforDie.GetComponent<CanvasGroup>().alpha == 0)
        {
            PlayerHealth.instance.isDead = false;          
            Debug.Log("Exit GameOver Fade");
        }
            
    }

    public void OverButton()
    {
        iTween.ScaleTo(m_GameOver, iTween.Hash("x", 0, "y", 0, "time", 0.3f));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Scale_ButtonMenu()
    {
        GameObject[] _buttonMenu = GameObject.FindGameObjectsWithTag("VRMenu");
        foreach (GameObject a in _buttonMenu)
        {
            iTween.ScaleTo(a, iTween.Hash("x", 1.0f, "y", 1.0f, "time", 0.3f));
        }
    }
}
