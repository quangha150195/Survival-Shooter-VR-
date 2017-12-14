using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour {
    public static MenuManager instance;
    [SerializeField]
    private GameObject m_Player;
    [SerializeField]
    private GameObject m_Recticle;
    [SerializeField]
    private GameObject m_BtnGroup;
    [SerializeField]
    private GameObject m_BtnSelectLevel;
    [SerializeField]
    private GameObject checkNormal;
    [SerializeField]
    private GameObject checkGamepad;


    [Header("Audio")]
    [SerializeField]
    private AudioClip m_SoundClick;
    [SerializeField]
    private AudioClip m_SoundRaycast;

    public static bool isGamePad;
    AudioSource m_SoundManager;
    RaycastHit hit;

    private bool _checkOneShot = true;
    // Use this for initialization
    void Start() {
        m_SoundManager = gameObject.GetComponent<AudioSource>();
        instance = this;
        isGamePad = false;
        checkNormal.SetActive(true);
        checkGamepad.SetActive(false);
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
            iTween.ScaleTo(_thisButton, iTween.Hash("x", 1.3f, "y", 1.3f, "time", 0.25f));
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
            iTween.ScaleTo(a, iTween.Hash("x", 1.0f, "y", 0.9f, "time", 0.25f));
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

    public void chooseGamePad()
    {
        checkGamepad.SetActive(true);
        checkGamepad.transform.localScale = Vector3.zero;
        isGamePad = true;
        iTween.ScaleTo(checkGamepad, Vector3.one, 0.25f);
        iTween.ScaleTo(checkNormal, Vector3.zero, 0.25f);
    }

    public void chooseNormal()
    {
        checkNormal.transform.localScale = Vector3.zero;
        isGamePad = false;
        iTween.ScaleTo(checkGamepad, Vector3.zero, 0.25f);
        iTween.ScaleTo(checkNormal, Vector3.one, 0.25f);
    }
}
