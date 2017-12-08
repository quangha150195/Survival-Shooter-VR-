using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


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

    private bool _checkOneShot = true;
    // Use this for initialization
    void Start() {
        m_SoundManager = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {

        Physics.Raycast(m_Recticle.transform.position, m_Recticle.transform.forward, out hit);

        if (hit.collider.tag == "VRMenu")
        {
            if (_checkOneShot)
            {
                m_SoundManager.PlayOneShot(m_SoundRaycast);
                _checkOneShot = false;
            }
            GameObject _thisButton = hit.collider.gameObject;
            iTween.ScaleTo(_thisButton, iTween.Hash("x", 1.5f, "y", 1.5f, "time", 0.3f));
            if (GvrViewer.Instance.Triggered || Input.GetButton("ShootButton"))
            {
                m_SoundManager.PlayOneShot(m_SoundClick);
                hit.collider.gameObject.transform.localScale = new Vector3(1, 1, 1);
                _thisButton.GetComponent<Button>().onClick.Invoke();
            }
        }
        else
        {
            _checkOneShot = true;
            Scale_ButtonMenu();
        }

    }

    public void btnMenu(GameObject _popup)
    {
        Scale_ButtonMenu();
        m_BtnGroup.SetActive(false);
        _popup.SetActive(true);
        iTween.ScaleFrom(_popup, iTween.Hash("x", 0.7f, "y", 0.7f, "time", 0.5f));
    }

    public void btnExit(GameObject _popup)
    {
        Scale_ButtonMenu();                
        _popup.SetActive(false);
        m_BtnGroup.SetActive(true);
    }

    private void Scale_ButtonMenu()
    {
        GameObject[] _buttonMenu = GameObject.FindGameObjectsWithTag("VRMenu");
        foreach (GameObject a in _buttonMenu)
        {
            iTween.ScaleTo(a, iTween.Hash("x", 1.0f, "y", 1.0f, "time", 0.3f));
        }
    }

    public void Play(string _nameScene)
    {
        SceneManager.LoadScene(_nameScene, LoadSceneMode.Single);
    }
    
    IEnumerator loadScene(string _nameScene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_nameScene);
        while (!asyncLoad.isDone)
        {          
            yield return null;
        }
    }
}
