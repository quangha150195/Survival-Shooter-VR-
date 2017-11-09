using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class MenuManager : MonoBehaviour {

    [SerializeField]
    private GameObject m_Player;
    [SerializeField] 
    private GameObject m_Recticle;
    [SerializeField]
    private GameObject m_BtnGroup;
    [SerializeField]
    private GameObject m_BtnAbout;
    [SerializeField]
    private GameObject m_BtnSelectLevel;
   
    [Header("Audio")]
    [SerializeField]
    private AudioClip m_SoundClick;
    [SerializeField]
    private AudioClip m_SoundRaycast;

    AudioSource m_SoundManager;
    RaycastHit hit;
    private int m_Once;
    private bool _checkOneShot = true;
    // Use this for initialization
    void Start () {
        m_SoundManager = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
       
	    Physics.Raycast(m_Recticle.transform.position, m_Recticle.transform.forward, out hit); 
        GameObject _thisButton = new GameObject();
        if(hit.collider.tag == "VRMenu")
        {
            if(_checkOneShot)
            {
                m_SoundManager.PlayOneShot(m_SoundRaycast);
                _checkOneShot = false;
            }                
            _thisButton = hit.collider.gameObject;
            iTween.ScaleTo(_thisButton, iTween.Hash("x", 1.5f, "y", 1.5f, "time", 0.3f));
            if(GvrViewer.Instance.Triggered)
            {
                
                m_Player.GetComponent<PlayerController>().m_move = true;
                _thisButton.GetComponent<Button>().onClick.Invoke();
            }
        }
        else
        {
            _checkOneShot = true;
            GameObject[] _buttonMenu = GameObject.FindGameObjectsWithTag("VRMenu");
            foreach (GameObject a in _buttonMenu)
            {
                iTween.ScaleTo(a, iTween.Hash("x", 1.0f, "y", 1.0f, "time", 0.3f));
            }
            
        }
      
    }


  public void btnPlay()
  {
        m_SoundManager.PlayOneShot(m_SoundClick);
        m_BtnGroup.SetActive(false);
        m_BtnSelectLevel.SetActive(true);
  }

  public void btnAbout()
  {
        m_SoundManager.PlayOneShot(m_SoundClick);
        m_BtnGroup.SetActive(false);
        m_BtnAbout.SetActive(true);
  }





}
